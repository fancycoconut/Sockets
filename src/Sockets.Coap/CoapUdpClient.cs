using System;
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

        private UdpClient client;

        public CoapUdpClient()
        {
            client = new UdpClient(AddressFamily.InterNetworkV6);
        }

        public CoapUdpClient(AddressFamily family)
        {
            client = new UdpClient(family);
        }

        public async Task Send(Uri uri, byte[] message)
        {
            if (message.Length == 0) throw new ArgumentException("An empty CoAP message cannot be sent.");

            client.Connect(uri.Host, uri.Port);
            await client.SendAsync(message, message.Length);

            var response = client.ReceiveAsync();
        }

        //public async Task Connect(Uri uri)
        //{
        //    var host = await Dns.GetHostEntryAsync(uri.Host);
        //    var endpoint = new IPEndPoint(host.AddressList[0], uri.Port);

        //    socket = new Socket(AddressFamily.InterNetworkV6, SocketType.Dgram, ProtocolType.Udp);
        //    await socket.ConnectAsync(endpoint);

        //    if (!socket.Connected) throw new Exception("Unable to establish connection with the CoAP server.");
        //}

        //public void Send(byte[] data)
        //{
        //    socket.Send(data, data.Length, SocketFlags.None);
        //}

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
