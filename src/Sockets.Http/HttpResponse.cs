using Sockets.Http;
using Sockets.Http.Parser;
using System.Collections.Generic;

namespace Sockets.Core.Http
{
    public class HttpResponse
    {
        public string Status { get; private set; }
        public HttpStatusCode StatusCode { get; private set; }
        public Dictionary<string, string> Headers { get; private set; }
        public Dictionary<string, HttpCookie> Cookies { get; private set; }

        private readonly string httpResponse;

        private HttpResponse(string response)
        {
            httpResponse = response;
        }

        public static HttpResponse Create(string responseText)
        {
            var result = new HttpResponseParser(responseText);
            
            return new HttpResponse(responseText)
            {
                Headers = result.Headers,
                Cookies = result.Cookies,
                Status = result.Status,
                StatusCode = result.StatusCode
            };
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
