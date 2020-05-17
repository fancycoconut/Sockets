using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Extensions.DependencyInjection;
using Sockets.CoapServer.Options;
using Sockets.CoapServer.Udp;
using System;

namespace Sockets.CoapServer.Extensions
{
    public static class ServerExtensions
    {
        public static IWebHostBuilder UseCoapUdpServer(this IWebHostBuilder builder, Action<CoapUdpServerOptions> options)
        {
            return builder.ConfigureServices(services =>
            {
                services.Configure(options);
                services.AddSingleton<IServer, CoapUdpServer>();
            });
        }
    }
}
