using Sockets.Core.Conversion;
using Sockets.Core.IO;
using System.IO;

namespace Sockets.Coap.Serialisation
{
    public class CoapReader
    {
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

                    return new CoapResponse
                    {
                        Id = messageId,
                        ProtocolVersion = version,
                        MessageType = (CoapMessageType) type,
                        StatusClass = (CoapStatusClass) statusClass,
                        StatusCode = (CoapStatusCode) messageCode,

                        Token = token
                    };
                }
            }           
        }
    }
}
