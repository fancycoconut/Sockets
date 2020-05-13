using Sockets.Coap.Serialisation;

namespace Sockets.Coap
{
    public class CoapRequest
    {
        public int Id { get; set; }
        public int ProtocolVersion => 1;
        public CoapMessageType MessageType { get; set; }
        public CoapMethod Method { get; set; }

        public byte[] Token { get; set; }

        public CoapRequest(CoapMethod method, CoapMessageType type)
        {
            MessageType = type;
            Method = method;
            Token = new byte[] { };
        }

        public byte[] Serialize()
        {
            var writer = new CoapWriter();
            return writer.Serialize(this);
        }
    }
}
