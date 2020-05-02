using Sockets.Core.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Sockets.Core.Http
{
    public class Request
    {
        public Uri Uri { get; set; }
        public Dictionary<string, string> Headers => headers;

        private readonly Method method;
        private readonly Dictionary<string, string> headers;

        public Request(string method) : this(new Method(method))
        {
        }

        public Request(Method method)
        {
            this.method = method;
            headers = new Dictionary<string, string>();
        }

        public virtual async Task<string> SendRequest(Stream stream)
        {
            using (var ms = new MemoryStream())
            {
                using (var reader = new StreamReader(stream))
                {
                    var requestHeaders = Encoding.UTF8.GetBytes(BuildRequestHeader());
                    await ms.WriteAsync(requestHeaders, 0, requestHeaders.Length);

                    if (method.HasRequestBody())
                    {

                    }

                    //if (!stream.CanWrite) 
                    //    throw new InvalidOperationException("The remote network connection has been closed therefore the request cannot be sent.");
                    //var buffer = ms.ToArray();
                    await ms.CopyToAsync(stream);

                    //if (!stream.CanRead)
                    //    throw new InvalidOperationException("The remote network connection has been closed therefore the response cannot be received.");

                    var result = await reader.ReadToEndAsync();
                    return result;
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
            headers.AddOrUpdate(name, value);
        }

        public string BuildRequestHeader()
        {
            var sb = new StringBuilder();
            sb.Append($"{method} {Uri.AbsoluteUri} {GetProtocol()}{Environment.NewLine}");

            foreach (var header in headers)
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
