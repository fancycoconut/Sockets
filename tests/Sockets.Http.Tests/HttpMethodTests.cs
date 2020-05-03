using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sockets.Core.Http;
using System;

namespace Sockets.Http.Tests
{
    [TestClass]
    public class HttpMethodTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestInvalidHttpMethod()
        {
            // Arrange & Act
            new HttpMethod("Invalid");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestEmptyHttpMethod()
        {
            // Arrange & Act
            new HttpMethod("");
        }

        [TestMethod]
        public void TestGet()
        {
            // Arrange & Act
            var method = HttpMethod.Get;

            // Assert
            Assert.IsFalse(method.HasRequestBody());
        }

        [TestMethod]
        public void TestHead()
        {
            // Arrange & Act
            var method = HttpMethod.Head;

            // Assert
            Assert.IsFalse(method.HasRequestBody());
        }

        [TestMethod]
        public void TestPost()
        {
            // Arrange & Act
            var method = HttpMethod.Post;

            // Assert
            Assert.IsTrue(method.HasRequestBody());
        }

        [TestMethod]
        public void TestPut()
        {
            // Arrange & Act
            var method = HttpMethod.Put;

            // Assert
            Assert.IsTrue(method.HasRequestBody());
        }

        [TestMethod]
        public void TestDelete()
        {
            // Arrange & Act
            var method = HttpMethod.Delete;

            // Assert
            Assert.IsTrue(method.HasRequestBody());
        }

        [TestMethod]
        public void TestOptions()
        {
            // Arrange & Act
            var method = HttpMethod.Options;

            // Assert
            Assert.IsFalse(method.HasRequestBody());
        }

        [TestMethod]
        public void TestPatch()
        {
            // Arrange & Act
            var method = HttpMethod.Patch;

            // Assert
            Assert.IsTrue(method.HasRequestBody());
        }

        [TestMethod]
        public void TestTrace()
        {
            // Arrange & Act
            var method = new HttpMethod("Trace");

            // Assert
            Assert.IsFalse(method.HasRequestBody());
        }
    }
}
