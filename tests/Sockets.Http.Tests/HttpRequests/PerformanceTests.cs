using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sockets.Core.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Sockets.Http.Tests.HttpRequests
{
    [TestClass]
    public class PerformanceTests
    {
        private HttpClient client;

        [TestInitialize]
        public void Initialise()
        {
            client = new HttpClient();
        }

        [TestCleanup]
        public void TearDown()
        {
            client.Dispose();
        }

        [TestMethod]
        public async Task SendRequest_BasicGet()
        {
            // Arrange
            var stopwatch = new Stopwatch();

            var request = new HttpRequest(HttpMethod.Get)
            {
                Uri = new Uri("https://www.google.co.nz/")
            };

            request.SetHeader("Accept", "*/*");
            request.SetHeader("Connection", "close");

            // Act
            stopwatch.Start();
            var response = await client.Send(request);
            stopwatch.Stop();

            // Assert
            Debug.WriteLine($"Elapsed time: {stopwatch.Elapsed.Seconds} secs");
        }

        [TestMethod]
        public async Task Get_BasicGet()
        {
            // Arrange & Act
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var response = await client.Get("https://www.google.co.nz/");
            stopwatch.Stop();

            // Assert
            Debug.WriteLine($"Elapsed time: {stopwatch.Elapsed.Seconds} secs");
        }

        [TestMethod]
        public async Task HttpClient_BasicGet()
        {
            // Arrange & Act
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            using (var client = new System.Net.Http.HttpClient())
            {
                var response = await client.GetAsync("https://www.google.co.nz/");
            }
            stopwatch.Stop();

            // Assert
            Debug.WriteLine($"Elapsed time: {stopwatch.Elapsed.Seconds} secs");
        }
    }
}
