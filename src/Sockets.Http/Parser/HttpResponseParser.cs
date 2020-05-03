using Sockets.Core.Extensions;
using Sockets.Http.Content;
using Sockets.Http.Factories;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Sockets.Http.Parser
{
    public class HttpResponseParser
    {
        public string ProtocolVersion { get; private set; }
        public string Status { get; private set; }
        public HttpStatusCode StatusCode { get; private set; }
        public Dictionary<string, string> Headers { get; }
        public Dictionary<string, HttpCookie> Cookies { get; }
        public IHttpContent Content { get; private set; }

        public HttpResponseParser(string response)
        {
            Headers = new Dictionary<string, string>();
            Cookies = new Dictionary<string, HttpCookie>();

            Parse(response);
        }

        private void Parse(string response)
        {
            var sections = Regex.Split(response, $"{Constants.CRLF}{Constants.CRLF}");
            if (sections.Length == 0) return;

            ParseResponseHeaders(sections[0]);

            if (sections.Length == 1) return;
            ParseResponseBody(sections[1]);
        }

        private void ParseResponseHeaders(string rawHeaders)
        {
            var lines = Regex.Split(rawHeaders, Constants.CRLF);
            SetResponseStatus(lines[0]);

            for (var i = 1; i < lines.Length; i++)
            {
                var headerValuePair = Regex.Split(lines[i], ": ");

                if (CheckAndSetCookie(headerValuePair)) continue;
                Headers.AddOrUpdate(headerValuePair[0], headerValuePair[1]);
            }
        }

        private void SetResponseStatus(string responseResult)
        {
            var parts = responseResult.Split(' ');

            ProtocolVersion = parts[0];
            StatusCode = (HttpStatusCode)Convert.ToInt32(parts[1]);
            Status = parts[2];
        }

        private bool CheckAndSetCookie(string[] keyValuePair)
        {
            if (keyValuePair[0] != "Set-Cookie") return false;

            var cookie = new HttpCookie(keyValuePair[1]);
            Cookies.AddOrUpdate(cookie.Name, cookie);
            return true;
        }

        private void ParseResponseBody(string rawBody)
        {
            var factory = new HttpContentFactory();
            Content = factory.Resolve(Headers, rawBody);
        }
    }
}
