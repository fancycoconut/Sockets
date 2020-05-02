using Sockets.Core.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace Sockets.Core.Http
{
    public class Request
    {
        public Uri Uri { get; set; }
        public Dictionary<string, string> Headers { get; }

        private readonly Method method;

        public Request(string method) : this(new Method(method))
        {
        }

        public Request(Method method)
        {
            this.method = method;
            Headers = new Dictionary<string, string>();
        }

        public virtual async Task<string> SendRequest(Stream connection)
        {
            using (var ms = new MemoryStream())
            {
                using (var reader = new StreamReader(connection))
                {
                    var requestHeaders = Encoding.ASCII.GetBytes(BuildRequestHeader());
                    await ms.WriteAsync(requestHeaders, 0, requestHeaders.Length);

                    if (method.HasRequestBody())
                    {

                    }

                    if (!connection.CanWrite)
                        throw new InvalidOperationException("The remote network connection has been closed therefore the request cannot be sent.");

                    var buffer = ms.ToArray();
                    await connection.WriteAsync(buffer, 0, buffer.Length);

                    if (!connection.CanRead)
                        throw new InvalidOperationException("The remote network connection has been closed therefore the response cannot be received.");

                    return await reader.ReadToEndAsync();
                }              
            }            
        }

        public void ClearHeaders()
        {
            headers.Clear();
        }

        public void SetHeader(string name, string value)
        {
            // TODO Add validation
            Headers.AddOrUpdate(name, value);
        }

        public string BuildRequestHeader()
        {
            var sb = new StringBuilder();
            sb.Append($"{method} {Uri.AbsoluteUri} {GetProtocol()}{Environment.NewLine}");

            foreach (var header in Headers)
            {
                sb.Append($"{header.Key}: {header.Value}{Environment.NewLine}");
            }

            sb.Append($"Host: { GetHostHeaderValue() }{Environment.NewLine}");
            sb.Append(Environment.NewLine);
            return sb.ToString();
        }

        private string GetHostHeaderValue()
        {
            return (Uri.Port == 80 || Uri.Port == 443) ? Uri.Host : $"{Uri.Host}:{Uri.Port}";
        }

        // TODO do some sort of protocol support for different versions
        private string GetProtocol() => "HTTP/1.1";
    }
}
