using Sockets.Http;
using Sockets.Http.Parser;
using System.Collections.Generic;

namespace Sockets.Core.Http
{
    public class HttpResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public Dictionary<string, string> Headers { get; }

        private readonly string httpResponse;

        public HttpResponse(string response)
        {
            httpResponse = response;

            var parser = new HttpResponseParser(httpResponse);
            Headers = parser.Headers;
            StatusCode = parser.StatusCode;
        }

        public bool IsSuccessCode
        {
            get
            {
                var code = (int)StatusCode;
                return code >= 200 && code < 300;
            }            
        }

        public override string ToString()
        {
            return httpResponse;
        }
    }
}
