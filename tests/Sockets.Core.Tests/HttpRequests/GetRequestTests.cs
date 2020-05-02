using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sockets.Core.Http;
using System;
using System.Threading.Tasks;

namespace Sockets.Http.Tests.HttpRequests
{
    [TestClass]
    public class GetRequestTests
    {
        private Client client;

        [TestInitialize]
        public void Initialise()
        {
            client = new Client();
        }

        [TestMethod]
        public async Task SendRequest_BasicGet()
        {
            // Arrange
            var request = new Request(Method.Get)
            {
                Uri = new Uri("https://www.google.co.nz/")
            };

            request.SetHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
            request.SetHeader("Accept-Encoding", "gzip, deflate, br");
            request.SetHeader("Accept-Language", "en-US,en;q=0.5");
            request.SetHeader("Connection", "keep-alive");
            request.SetHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:75.0) Gecko/20100101 Firefox/75.0");

            // Act
            var response = await client.Send(request);

            // Assert
        }
    }
}
