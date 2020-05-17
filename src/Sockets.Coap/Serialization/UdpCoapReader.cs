using Sockets.Core.Conversion;
using Sockets.Core.Extensions;
using Sockets.Core.IO;
using System;
using System.Collections.Generic;
using System.IO;

namespace Sockets.Coap.Serialization
{
    /// <summary>
    /// https://tools.ietf.org/html/rfc7252
    /// </summary>
    public class UdpCoapReader
    {
        private const byte PayloadStartMarker = 0xFF;

        public CoapResponse Deserialize(byte[] message)
        {
            using (var input = new MemoryStream(message))
            {
                using (var reader = new EndianBinaryReader(EndianBitConverter.Big, input))
                {
                    var header = reader.ReadUInt32();

                    var version = (int)(header >> 30) & 0x3;
                    var type = (header >> 28) & 0x3;
                    var tokenLength = (int)(header >> 24) & 0xF;
                    var messageCode = (int)(header >> 16) & 0xFF;
                    var messageId = (int)header & 0xFFFF;

                    var statusClass = (messageCode >> 5) & 0x3;
                    //var statusCode = messageCode & 0x1F;

                    var token = reader.ReadBytes(tokenLength);

                    var options = DeserializeOptions(reader);

                    var payload = new byte[] { };
                    var payloadLength = reader.BaseStream.Length - reader.BaseStream.Position;
                    if (payloadLength > 0)
                    {
                        payload = reader.ReadBytes(Convert.ToInt32(payloadLength));
                    }

                    return new CoapResponse
                    {
                        Id = messageId,
                        ProtocolVersion = version,
                        MessageType = (CoapMessageType) type,
                        StatusClass = (CoapStatusClass) statusClass,
                        StatusCode = (CoapStatusCode) messageCode,

                        Token = token,
                        Options = options,
                        Payload = payload
                    };
                }
            }           
        }

        public IEnumerable<CoapOption> DeserializeOptions(EndianBinaryReader reader)
        {
            var options = new List<CoapOption>();

            var nextOptionNumber = 0;
            while (!reader.EndOfStream())
            {
                var smallDeltaLength = reader.ReadByte();
                if (smallDeltaLength == PayloadStartMarker) break;

                var delta = (smallDeltaLength >> 4) & 0xF;
                var length = smallDeltaLength & 0xF;

                // Read extended delta
                delta = ReadExtendedIfRequired(reader, delta);

                // Read extended length
                length = ReadExtendedIfRequired(reader, length);

                var number = delta + nextOptionNumber;
                var value = reader.ReadBytes(length);
                options.Add(CoapOption.FromNumberValue(number, value));

                nextOptionNumber = number;
            }

            return options;
        }

        public int ReadExtendedIfRequired(EndianBinaryReader reader, int value)
        {
            if (value == 13)
            {
                return reader.ReadByte() + 13;
            }
            else if (value == 14)
            {
                return reader.ReadUInt16() + 269;
            }

            return value;
        }
    }
}
