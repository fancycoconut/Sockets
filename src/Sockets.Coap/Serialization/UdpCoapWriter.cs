using Sockets.Coap.Exceptions;
using Sockets.Core.Conversion;
using Sockets.Core.IO;
using System.Collections.Generic;
using System.IO;

namespace Sockets.Coap.Serialization
{
    /// <summary>
    /// https://tools.ietf.org/html/rfc7252
    /// </summary>
    public class UdpCoapWriter
    {
        private const byte PayloadStartMarker = 0xFF;

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
                        writer.Write(PayloadStartMarker);
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
                var smallDeltaLength = (((delta & 0xF) << 4) | (length & 0xF)) & 0xFF;
                writer.Write((byte) smallDeltaLength);

                // Write extended delta
                SerializeExtended(writer, delta);

                // Write extended length
                SerializeExtended(writer, length);

                // Write option value
                writer.Write(option.RawValue);

                prevOptionNumber = number;
            }
        }

        private void SerializeExtended(EndianBinaryWriter writer, int value)
        {
            if (13 <= value && value <= 268)
            {
                var int8Value = (value - 13) & 0xFF;
                writer.Write((byte) int8Value);
            }
            else if (269 <= value && value <= 65804)
            {
                var int16Value = (value - 269) & 0xFFFF;
                writer.Write((short) int16Value);
            }
        }
    }
}
