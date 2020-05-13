using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sockets.Core.Conversion;

namespace Sockets.Core.Tests.Conversion
{
    [TestClass]
    public class LittleEndianBitConverterTests
    {
        [TestMethod]
        public void GetBytesShort()
        {
            CheckBytes(new byte[] { 0, 0 }, EndianBitConverter.Little.GetBytes((short)0));
            CheckBytes(new byte[] { 1, 0 }, EndianBitConverter.Little.GetBytes((short)1));
            CheckBytes(new byte[] { 0, 1 }, EndianBitConverter.Little.GetBytes((short)256));
            CheckBytes(new byte[] { 255, 255 }, EndianBitConverter.Little.GetBytes((short)-1));
            CheckBytes(new byte[] { 1, 1 }, EndianBitConverter.Little.GetBytes((short)257));
        }

        [TestMethod]
        public void GetBytesUShort()
        {
            CheckBytes(new byte[] { 0, 0 }, EndianBitConverter.Little.GetBytes((ushort)0));
            CheckBytes(new byte[] { 1, 0 }, EndianBitConverter.Little.GetBytes((ushort)1));
            CheckBytes(new byte[] { 0, 1 }, EndianBitConverter.Little.GetBytes((ushort)256));
            CheckBytes(new byte[] { 255, 255 }, EndianBitConverter.Little.GetBytes((ushort)ushort.MaxValue));
            CheckBytes(new byte[] { 1, 1 }, EndianBitConverter.Little.GetBytes((ushort)257));
        }

        [TestMethod]
        public void GetBytesInt()
        {
            CheckBytes(new byte[] { 0, 0, 0, 0 }, EndianBitConverter.Little.GetBytes((int)0));
            CheckBytes(new byte[] { 1, 0, 0, 0 }, EndianBitConverter.Little.GetBytes((int)1));
            CheckBytes(new byte[] { 0, 1, 0, 0 }, EndianBitConverter.Little.GetBytes((int)256));
            CheckBytes(new byte[] { 0, 0, 1, 0 }, EndianBitConverter.Little.GetBytes((int)65536));
            CheckBytes(new byte[] { 0, 0, 0, 1 }, EndianBitConverter.Little.GetBytes((int)16777216));
            CheckBytes(new byte[] { 255, 255, 255, 255 }, EndianBitConverter.Little.GetBytes((int)-1));
            CheckBytes(new byte[] { 1, 1, 0, 0 }, EndianBitConverter.Little.GetBytes((int)257));
        }

        [TestMethod]
        public void GetBytesUInt()
        {
            CheckBytes(new byte[] { 0, 0, 0, 0 }, EndianBitConverter.Little.GetBytes((uint)0));
            CheckBytes(new byte[] { 1, 0, 0, 0 }, EndianBitConverter.Little.GetBytes((uint)1));
            CheckBytes(new byte[] { 0, 1, 0, 0 }, EndianBitConverter.Little.GetBytes((uint)256));
            CheckBytes(new byte[] { 0, 0, 1, 0 }, EndianBitConverter.Little.GetBytes((uint)65536));
            CheckBytes(new byte[] { 0, 0, 0, 1 }, EndianBitConverter.Little.GetBytes((uint)16777216));
            CheckBytes(new byte[] { 255, 255, 255, 255 }, EndianBitConverter.Little.GetBytes((uint)uint.MaxValue));
            CheckBytes(new byte[] { 1, 1, 0, 0 }, EndianBitConverter.Little.GetBytes((uint)257));
        }

        [TestMethod]
        public void GetBytesLong()
        {
            CheckBytes(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 }, EndianBitConverter.Little.GetBytes(0L));
            CheckBytes(new byte[] { 1, 0, 0, 0, 0, 0, 0, 0 }, EndianBitConverter.Little.GetBytes(1L));
            CheckBytes(new byte[] { 0, 1, 0, 0, 0, 0, 0, 0 }, EndianBitConverter.Little.GetBytes(256L));
            CheckBytes(new byte[] { 0, 0, 1, 0, 0, 0, 0, 0 }, EndianBitConverter.Little.GetBytes(65536L));
            CheckBytes(new byte[] { 0, 0, 0, 1, 0, 0, 0, 0 }, EndianBitConverter.Little.GetBytes(16777216L));
            CheckBytes(new byte[] { 0, 0, 0, 0, 1, 0, 0, 0 }, EndianBitConverter.Little.GetBytes(4294967296L));
            CheckBytes(new byte[] { 0, 0, 0, 0, 0, 1, 0, 0 }, EndianBitConverter.Little.GetBytes(1099511627776L));
            CheckBytes(new byte[] { 0, 0, 0, 0, 0, 0, 1, 0 }, EndianBitConverter.Little.GetBytes(1099511627776L * 256));
            CheckBytes(new byte[] { 0, 0, 0, 0, 0, 0, 0, 1 }, EndianBitConverter.Little.GetBytes(1099511627776L * 256 * 256));
            CheckBytes(new byte[] { 255, 255, 255, 255, 255, 255, 255, 255 }, EndianBitConverter.Little.GetBytes(-1L));
            CheckBytes(new byte[] { 1, 1, 0, 0, 0, 0, 0, 0 }, EndianBitConverter.Little.GetBytes(257L));
        }

        [TestMethod]
        public void GetBytesULong()
        {
            CheckBytes(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 }, EndianBitConverter.Little.GetBytes(0UL));
            CheckBytes(new byte[] { 1, 0, 0, 0, 0, 0, 0, 0 }, EndianBitConverter.Little.GetBytes(1UL));
            CheckBytes(new byte[] { 0, 1, 0, 0, 0, 0, 0, 0 }, EndianBitConverter.Little.GetBytes(256UL));
            CheckBytes(new byte[] { 0, 0, 1, 0, 0, 0, 0, 0 }, EndianBitConverter.Little.GetBytes(65536UL));
            CheckBytes(new byte[] { 0, 0, 0, 1, 0, 0, 0, 0 }, EndianBitConverter.Little.GetBytes(16777216UL));
            CheckBytes(new byte[] { 0, 0, 0, 0, 1, 0, 0, 0 }, EndianBitConverter.Little.GetBytes(4294967296UL));
            CheckBytes(new byte[] { 0, 0, 0, 0, 0, 1, 0, 0 }, EndianBitConverter.Little.GetBytes(1099511627776UL));
            CheckBytes(new byte[] { 0, 0, 0, 0, 0, 0, 1, 0 }, EndianBitConverter.Little.GetBytes(1099511627776UL * 256));
            CheckBytes(new byte[] { 0, 0, 0, 0, 0, 0, 0, 1 }, EndianBitConverter.Little.GetBytes(1099511627776UL * 256 * 256));
            CheckBytes(new byte[] { 255, 255, 255, 255, 255, 255, 255, 255 }, EndianBitConverter.Little.GetBytes(ulong.MaxValue));
            CheckBytes(new byte[] { 1, 1, 0, 0, 0, 0, 0, 0 }, EndianBitConverter.Little.GetBytes(257UL));
        }

        void CheckBytes(byte[] expected, byte[] actual)
        {
            Assert.AreEqual(expected.Length, actual.Length, "Lengths should match");
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }
    }
}
