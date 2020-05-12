using System.IO;

namespace Sockets.Coap.Serialisation
{
    public class CoapWriter
    {
        public byte[] Encode(CoapRequest request)
        {
            using (var ms = new MemoryStream())
            {
                using (var writer = new BinaryWriter(ms))
                {
                    var header = ((request.ProtocolVersion & 0x3) << 30)
                        | (((int)request.Type & 0x3) << 28)
                        | ((request.Token.Length & 0xF) << 24)
                        | (((int)request.Method & 0xFF) << 16)
                        | (request.Id & 0xFFFF);

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
