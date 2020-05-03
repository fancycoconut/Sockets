using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sockets.Http.Parser;
using Sockets.Http.Tests.Helpers;
using System.Threading.Tasks;

namespace Sockets.Http.Tests.Parser
{
    [TestClass]
    public class HttpResponseParserTests
    {
        [TestMethod]
        public async Task ParseHeadRequestTest()
        {
            // Arrange
            var responseText = await ResourceHelper.GetResponse("SampleHeadResponse");

            // Act
            var parser = new HttpResponseParser(responseText);

            // Assert
            Assert.AreEqual(HttpStatusCode.Ok, parser.StatusCode);
            Assert.AreEqual(12, parser.Headers.Count);
        }
    }
}
