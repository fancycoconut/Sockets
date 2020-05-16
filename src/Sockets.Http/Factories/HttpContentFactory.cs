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

            if (chunkedTransfer) return HandleChunkedContent(contentType, content);            

            return new NullHttpContent();
        }

        private IHttpContent HandleChunkedContent(string contentType, string content)
        {
            if (content.IndexOf("text") > 0) return TextContent.FromChunkedText(content);

            return new NullHttpContent();
        }
    }
}
