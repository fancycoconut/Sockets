using Sockets.Coap.Exceptions;
using Sockets.Core.Conversion;
using Sockets.Core.IO;
using System;
using System.Collections.Generic;
using System.IO;

namespace Sockets.Coap.Serialisation
{
    public class UdpCoapWriter
    {
        public byte[] Serialize(CoapRequest request)
        {
            using (var output = new MemoryStream())
            {
                using (var writer = new EndianBinaryWriter(EndianBitConverter.Big, output))
                {
                    var version = (request.ProtocolVersion & 0x3) << 30;
                    var type = ((int)request.MessageType & 0x3) << 28;
                    var tokenLength = (request.Token.Length & 0xF) << 24;
                    var messageCode = ((int)request.Method & 0xFF) << 16;
                    var messageId = request.Id & 0xFFFF;

                    var header = version | type | tokenLength | messageCode | messageId;

                    writer.Write(header);
                    writer.Write(request.Token);

                    // Options
                    SerializeOptions(writer, request.Options);

                    // Payload
                    if (request.Payload?.Length > 0)
                    {
                        writer.Write(Convert.ToByte(0xFF));
                        writer.Write(request.Payload);
                    }
                }

                return output.ToArray();
            }
        }

        private void SerializeOptions(EndianBinaryWriter writer, IEnumerable<CoapOption> options)
        {
            var prevOptionNumber = 0;
            foreach (var option in options)
            {
                var number = (int) option.Number;
                if (prevOptionNumber > number) throw new CoapOptionException("The previous option number cannot be larger than the current option number.");
                var delta = number - prevOptionNumber;
                var length = option.ValueLength;

                // Write 4-bit delta & length
                var smallDeltaLength = (byte)((((delta & 0xF) << 4) | (length & 0xF)) & 0xFF);
                writer.Write(smallDeltaLength);

                // Write extended delta
                if (13 <= delta && delta <= 268)
                {
                    var int8Delta = (byte)((delta - 13) & 0xFF);
                    writer.Write(int8Delta);
                }
                else if (269 <= delta && delta <= 65804)
                {
                    var int16Delta = (delta - 269) & 0xFFFF;
                    writer.Write(int16Delta);
                }

                // Write extended length
                if (13 <= length && length <= 268)
                {
                    var int8Length = (byte)((length - 13) & 0xFF);
                    writer.Write(int8Length);
                }
                else if (269 <= length && length <= 65804)
                {
                    var int16Length = (length - 269) & 0xFFFF;
                    writer.Write(int16Length);
                }

                // Write option value
                writer.Write(option.RawValue);

                prevOptionNumber = number;
            }
        }
    }
}
