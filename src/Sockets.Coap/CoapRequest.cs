using Sockets.Coap.Serialisation;
using System.Collections.Generic;

namespace Sockets.Coap
{
    public class CoapRequest
    {
        public int Id { get; set; }
        public int ProtocolVersion => 1;
        public CoapMessageType MessageType { get; set; }
        public CoapMethod Method { get; set; }
        public IEnumerable<CoapOption> Options { get; set; }
        public byte[] Token { get; set; }

        public CoapRequest(CoapMethod method, CoapMessageType type)
        {
            MessageType = type;
            Method = method;
            Token = new byte[] { };
            Options = new List<CoapOption>();
        }

        public byte[] Serialize()
        {
            var writer = new CoapWriter();
            return writer.Serialize(this);
        }
    }
}
