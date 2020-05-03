using Sockets.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Sockets.Http.Parser
{
    public class HttpResponseParser
    {
        public DateTime Date { get; private set; }
        public HttpStatusCode StatusCode { get; private set; }
        public Dictionary<string, string> Headers { get; }

        private readonly string response;

        public HttpResponseParser(string response)
        {
            this.response = response;
            Headers = new Dictionary<string, string>();

            Parse();
        }

        private void Parse()
        {
            var responseSections = Regex.Split(response, "\r\n\r\n");
            ParseResponseHeaders(responseSections[0]);
        }

        private void ParseResponseHeaders(string headerSection)
        {
            var lines = Regex.Split(headerSection, "\r\n");
            var responseResult = lines[0].Split(' ');
            StatusCode = (HttpStatusCode)Convert.ToInt32(responseResult[1]);

            for (var i = 2; i < lines.Length; i++)
            {
                var headerValuePair = Regex.Split(lines[i], ": ");
                Headers.AddOrUpdate(headerValuePair[0], headerValuePair[1]);
            }
        }
    }
}
