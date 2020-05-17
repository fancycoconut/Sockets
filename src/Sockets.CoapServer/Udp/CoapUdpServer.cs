using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Options;
using Sockets.CoapServer.Options;
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Sockets.CoapServer.Udp
{
    public class CoapUdpServer : IServer
    {
        public IFeatureCollection Features { get; }

        private bool serverHasStarted = false;
        private bool disposed = false;

        public CoapUdpServer(IServiceProvider serviceProvider, IOptions<CoapUdpServerOptions> options)
        {
            Features = new FeatureCollection();

            //Features.Set<IHttpRequestFeature>
        }

        public Task StartAsync<TContext>(IHttpApplication<TContext> application, CancellationToken cancellationToken)
        {
            //try
            //{
            //    if (serverHasStarted) throw new InvalidOperationException("The server has alreader started and/or has not been cleaned up yet.");
            //    serverHasStarted = true;

            //    async Task OnBind(ListenOptions options)
            //    {

            //    }
            //}
            //catch (Exception ex)
            //{
            //    Dispose();
            //    throw;
            //}
            throw new NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            StopAsync(new CancellationToken(canceled: true)).GetAwaiter().GetResult();
        }
    }
}
