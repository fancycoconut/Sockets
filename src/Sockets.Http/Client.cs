using Sockets.Http.Handlers;
using System;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Sockets.Core.Http
{
    public class Client : IDisposable
    {
        private bool disposed = false;
        private readonly TcpClient client;

        private IHttpsHandler httpsHandler;

        public Client()
        {
            client = new TcpClient();
            httpsHandler = new DefaultHttpsHandler();
        }

        public async Task<Response> Send(Request request)
        {
            using (var stream = await OpenConnection(request.Uri))
            {
                var response = await request.SendRequest(stream);
                return new Response(response);
            }
        }

        public async Task<Stream> OpenConnection(Uri uri)
        {
            await client.ConnectAsync(uri.Host, uri.Port);

            var stream = client.GetStream();
            if (uri.Scheme == "http") return stream;

            return await httpsHandler.GetSslStream(stream, uri);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;

            if (disposing)
            {
                client?.Dispose();
            }

            disposed = true;
        }
    }
}
