using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sockets.Core.Conversion;
using Sockets.Core.IO;
using System;
using System.IO;
using System.Text;

namespace Sockets.Core.Tests.IO
{
    [TestClass]
    public class EndianBinaryReaderTests
    {
        const string TestString = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmopqrstuvwxyz";
        static readonly byte[] TestBytes = Encoding.ASCII.GetBytes(TestString);

        /// <summary>
        /// Check fix to bug found by Jamie Rothfeder
        /// </summary>
        [TestMethod]
        public void ReadCharsBeyondInternalBufferSize()
        {
            var stream = new MemoryStream(TestBytes);
            var subject = new EndianBinaryReader(EndianBitConverter.Little, stream);

            char[] chars = new char[TestString.Length];
            subject.Read(chars, 0, chars.Length);
            Assert.AreEqual(TestString, new string(chars));
        }

        [TestMethod]
        public void ReadCharsBeyondProvidedBufferSize()
        {
            var stream = new MemoryStream(TestBytes);
            var subject = new EndianBinaryReader(EndianBitConverter.Little, stream);

            char[] chars = new char[TestString.Length - 1];
            try
            {
                subject.Read(chars, 0, TestString.Length);
                Assert.Fail("Expected exception");
            }
            catch (ArgumentException)
            {
                // Expected
            }
        }
    }
}
