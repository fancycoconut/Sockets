using System;

namespace Sockets.Coap.Exceptions
{
    public class CoapOptionException : Exception
    {
        public CoapOptionException(string message) : base(message)
        {
        }
    }
}
