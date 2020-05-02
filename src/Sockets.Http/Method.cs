using System;

namespace Sockets.Core.Http
{
    public class Method
    {
        // NOTE: Connect should not be exposed.. End users should not be able to use this.
        public static Method Get => new Method("GET");
        public static Method Head => new Method("HEAD");
        public static Method Post => new Method("POST");
        public static Method Put => new Method("PUT");
        public static Method Delete => new Method("DELETE");
        public static Method Options => new Method("OPTIONS");
        public static Method Patch => new Method("PATCH");

        private readonly string method;
        private const string validMethods = "GET,HEAD,POST,PUT,DELETE,CONNECT,OPTIONS,TRACE,PATCH";

        public Method(string method)
        {
            if (string.IsNullOrEmpty(method)) throw new ArgumentException("Http method cannot be null or empty");

            this.method = method.ToUpper();
            if (validMethods.IndexOf(this.method) == -1) throw new ArgumentException("Invalid Http method provided");            
        }

        public bool HasRequestBody()
        {
            return method != "GET"
                && method != "HEAD"
                && method != "OPTIONS"
                && method != "CONNECT"
                && method != "TRACE";
        }

        public override string ToString() => method;
    }
}
