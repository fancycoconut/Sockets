using System;

namespace Sockets.Core.Http
{
    public class HttpMethod
    {
        // NOTE: Connect should not be exposed.. End users should not be able to use this.
        public static HttpMethod Get => new HttpMethod("GET");
        public static HttpMethod Head => new HttpMethod("HEAD");
        public static HttpMethod Post => new HttpMethod("POST");
        public static HttpMethod Put => new HttpMethod("PUT");
        public static HttpMethod Delete => new HttpMethod("DELETE");
        public static HttpMethod Options => new HttpMethod("OPTIONS");
        public static HttpMethod Patch => new HttpMethod("PATCH");

        private readonly string method;
        private const string validMethods = "GET,HEAD,POST,PUT,DELETE,CONNECT,OPTIONS,TRACE,PATCH";

        public HttpMethod(string method)
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
