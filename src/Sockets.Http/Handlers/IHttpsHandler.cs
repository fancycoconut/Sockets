using System;
using System.Net.Security;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Sockets.Http.Handlers
{
    public interface IHttpsHandler
    {
        Task<SslStream> GetSslStream(NetworkStream stream, Uri uri);
    }
}
