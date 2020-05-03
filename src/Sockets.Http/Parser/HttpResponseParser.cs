using Sockets.Core.Extensions;
using Sockets.Core.Http;
using Sockets.Http.Helpers;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Sockets.Http.Parser
{
    public class HttpResponseParser
    {
        public string Status { get; set; }
        public HttpStatusCode StatusCode { get; private set; }
        public Dictionary<string, string> Headers { get; }
        public Dictionary<string, HttpCookie> Cookies { get; }

        private readonly string response;

        public HttpResponseParser(string response)
        {
            Headers = new Dictionary<string, string>();
            Cookies = new Dictionary<string, HttpCookie>();
            this.response = response;

            Parse(response);
        }

        private void Parse(string response)
        {
            var sections = Regex.Split(response, "\r\n\r\n");
            if (sections.Length == 0) return;

            ParseResponseHeaders(sections[0]);

            if (sections.Length == 1) return;
            ParseResponseBody(sections[1]);
        }

        private void ParseResponseHeaders(string headerSection)
        {
            var lines = Regex.Split(headerSection, "\r\n");
            var responseResult = lines[0].Split(' ');
            
            StatusCode = (HttpStatusCode)Convert.ToInt32(responseResult[1]);
            Status = responseResult[2];

            for (var i = 1; i < lines.Length; i++)
            {
                var headerValuePair = Regex.Split(lines[i], ": ");

                // Parse cookies...
                if (headerValuePair[0] == "Set-Cookie")
                {
                    var cookie = new HttpCookie(headerValuePair[1]);
                    Cookies.AddOrUpdate(cookie.Name, cookie);
                    continue;
                }

                Headers.AddOrUpdate(headerValuePair[0], headerValuePair[1]);
            }
        }

        private void ParseResponseBody(string bodySection)
        {
            var body = Regex.Split(bodySection, "\r\n");
        }
    }
}
