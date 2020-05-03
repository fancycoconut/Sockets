using System.Collections.Generic;

namespace Sockets.Http.Parser
{
    public interface IHttpResponseParser
    {
        Dictionary<string, string> GetHeaders();
    }
}
