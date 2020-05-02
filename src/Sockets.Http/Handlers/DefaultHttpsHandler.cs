using System;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Sockets.Http.Handlers
{
    public class DefaultHttpsHandler : IHttpsHandler
    {
        public async Task<SslStream> GetSslStream(NetworkStream stream, Uri uri)
        {
            var ssl = new SslStream(stream, false, ValidateServerCertificate, null);
            await ssl.AuthenticateAsClientAsync(uri.Host);
            return ssl;
        }

        private bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            // TODO: This needs to be implemented properly but for now let's just accept all certs
            return true;
        }
    }
}
