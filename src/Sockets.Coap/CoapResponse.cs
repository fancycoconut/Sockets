using System.Collections.Generic;

namespace Sockets.Coap
{
    public class CoapResponse
    {
        public int Id { get; set; }
        public int ProtocolVersion { get; set; }
        public CoapMessageType MessageType { get; set; }
        public CoapStatusClass StatusClass { get; set; }
        public CoapStatusCode StatusCode { get; set; }

        public byte[] Token { get; set; }
        public IEnumerable<CoapOption> Options { get; set; }
        public byte[] Payload { get; set; }
    }
}
