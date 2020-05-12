using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Sockets.Coap
{
    /// <summary>
    /// https://tools.ietf.org/html/rfc7252
    /// </summary>
    public class CoapUdpClient : IDisposable
    {
        private bool disposed = false;

        private Socket socket;

        public CoapUdpClient()
        {
        }

        public async Task Connect(Uri uri)
        {
            var host = await Dns.GetHostEntryAsync(uri.Host);
            var endpoint = new IPEndPoint(host.AddressList[0], uri.Port);

            socket = new Socket(AddressFamily.InterNetworkV6, SocketType.Dgram, ProtocolType.Udp);
            await socket.ConnectAsync(endpoint);

            if (!socket.Connected) throw new Exception("Unable to establish connection with the CoAP server.");
        }

        public void Send(byte[] data)
        {
            socket.Send(data, data.Length, SocketFlags.None);
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
                socket?.Dispose();
            }

            disposed = true;
        }
    }
}
