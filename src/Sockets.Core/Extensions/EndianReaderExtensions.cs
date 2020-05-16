using Sockets.Core.IO;

namespace Sockets.Core.Extensions
{
    public static class EndianReaderExtensions
    {
        public static bool EndOfStream(this EndianBinaryReader reader)
        {
            return reader.BaseStream.Position == reader.BaseStream.Length;
        }
    }
}
