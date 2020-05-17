using Sockets.Coap.Serialization;
using Sockets.Core.Conversion;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text.Json;
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
        private RNGCryptoServiceProvider rng;

        public CoapUdpClient() : this(AddressFamily.InterNetworkV6) { }

        public CoapUdpClient(AddressFamily family)
        {
            rng = new RNGCryptoServiceProvider();
            client = new UdpClient(family);
        }

        public async Task<CoapResponse> Send(Uri uri, byte[] message)
        {
            if (message.Length == 0) throw new ArgumentException("An empty CoAP message cannot be sent.");

            client.Connect(uri.Host, uri.Port);
            await client.SendAsync(message, message.Length);

            var response = await client.ReceiveAsync();
            var reader = new UdpCoapReader();
            return reader.Deserialize(response.Buffer);
        }

        public Task<CoapResponse> GetConfirmable(Uri uri)
        {
            var token = GetRandomBytes(8);
            return GetConfirmable(uri, token);
        }

        public Task<CoapResponse> GetConfirmable(Uri uri, byte[] token)
        {
            if (token.Length > 8) throw new ArgumentException("CoAP message tokens cannot exceed 8 bytes in size.");

            var options = new List<CoapOption>
            {
                new CoapOption(Option.UriHost, uri.Host),
                new CoapOption(Option.UriPort, uri.Port),
                new CoapOption(Option.UriPath, uri.AbsolutePath.Replace("/", string.Empty))
            };
            options.AddRange(GetQueryParameterOptions(uri.Query));

            var messageId = GetRandomBytes(2);
            var request = new CoapRequest(CoapMethod.Get, CoapMessageType.Confirmable)
            {
                Id = EndianBitConverter.Little.ToInt16(messageId, 0),
                Token = token,
                Options = options
            };

            return Send(uri, request.Serialize());
        }

        public Task<CoapResponse> PostConfirmableJson(Uri uri, object payload)
        {
            var token = GetRandomBytes(8);
            var jsonBytes = JsonSerializer.SerializeToUtf8Bytes(payload);
            return PostConfirmable(uri, token, MediaType.ApplicationJson, jsonBytes);
        }

        public Task<CoapResponse> PostConfirmable(Uri uri, byte[] token, MediaType mediaType, byte[] payload)
        {
            var options = new List<CoapOption>
            {
                new CoapOption(Option.UriHost, uri.Host),
                new CoapOption(Option.UriPort, uri.Port),
                new CoapOption(Option.UriPath, uri.AbsolutePath.Replace("/", string.Empty)),
                new CoapOption(Option.ContentFormat, (int) mediaType)
            };
            options.AddRange(GetQueryParameterOptions(uri.Query));

            var messageId = GetRandomBytes(2);
            var request = new CoapRequest(CoapMethod.Post, CoapMessageType.Confirmable)
            {
                Id = EndianBitConverter.Little.ToInt16(messageId, 0),
                Token = token,
                Options = options,
                Payload = payload
            };

            return Send(uri, request.Serialize());
        }

        private IEnumerable<CoapOption> GetQueryParameterOptions(string query)
        {
            var queryParams = query.Replace("?", string.Empty).Split('&');
            foreach (var param in queryParams)
            {
                yield return new CoapOption(Option.UriQuery, param);
            }
        }

        private byte[] GetRandomBytes(int length)
        {
            var randomBytes = new byte[length];
            rng.GetBytes(randomBytes);
            return randomBytes;
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
                rng?.Dispose();
                client?.Dispose();
            }

            disposed = true;
        }
    }
}
