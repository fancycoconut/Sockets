using Sockets.Coap.Serialisation;

namespace Sockets.Coap
{
    public class CoapRequest
    {
        public short Id { get; set; }
        public int ProtocolVersion => 1;
        public CoapMessageType Type { get; set; }
        public CoapMethod Method { get; set; }

        public byte[] Token { get; set; }

        public CoapRequest(CoapMethod method, CoapMessageType type)
        {
            Type = type;
            Method = method;
            Token = new byte[] { };
        }

        public byte[] Serialise()
        {
            var writer = new CoapWriter();
            return writer.Encode(this);
        }
    }
}
