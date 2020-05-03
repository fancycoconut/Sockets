using Sockets.Core.Extensions;
using Sockets.Http.Content;
using System.Collections.Generic;

namespace Sockets.Http.Factories
{
    public class HttpContentFactory
    {
        public IHttpContent Resolve(Dictionary<string, string> headers, string content)
        {
            var contentType = headers.GetValue("Content-Type");
            var chunkedTransfer = headers.GetValue("Transfer-Encoding") == "chunked";

            //if (content.IndexOf("text/html") > 0)

            return new NullHttpContent();
        }
    }
}
