using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sockets.Core.Http;
using System;
using System.Threading.Tasks;

namespace Sockets.Http.Tests.HttpRequests
{
    [TestClass]
    public class GetRequestTests
    {
        private HttpClient client;

        [TestInitialize]
        public void Initialise()
        {
            client = new HttpClient();
        }

        [TestMethod]
        public async Task SendRequest_BasicGet()
        {
            // Arrange
            var request = new HttpRequest(HttpMethod.Get)
            {
                Uri = new Uri("https://www.google.co.nz/")
            };

            request.SetHeader("Accept", "*/*");
            request.SetHeader("Connection", "close");

            // Act
            var response = await client.Send(request);

            // Assert
            Assert.AreEqual(HttpStatusCode.Ok, response.StatusCode);
        }

        [TestMethod]
        public async Task Get_BasicGet()
        {
            // Arrange & Act
            var response = await client.Get("https://www.google.co.nz/");

            // Assert
            Assert.AreEqual(HttpStatusCode.Ok, response.StatusCode);
        }
    }
}
