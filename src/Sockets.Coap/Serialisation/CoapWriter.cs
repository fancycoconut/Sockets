using Sockets.Core.Conversion;
using Sockets.Core.IO;
using System.IO;

namespace Sockets.Coap.Serialisation
{
    public class CoapWriter
    {
        public byte[] Encode(CoapRequest request)
        {
            using (var ms = new MemoryStream())
            {
                using (var writer = new EndianBinaryWriter(EndianBitConverter.Big, ms))
                {
                    var version = (request.ProtocolVersion & 0x3) << 30;
                    var type = ((int)request.Type & 0x3) << 28;
                    var tokenLength = (request.Token.Length & 0xF) << 24;
                    var messageCode = ((int)request.Method & 0xFF) << 16;
                    var messageId = request.Id & 0xFFFF;

                    var header = version | type | tokenLength | messageCode | messageId;

                    writer.Write(header);
                    writer.Write(request.Token);

                    // Options

                    // Payload
                }

                return ms.ToArray();
            }
        }
    }
}
