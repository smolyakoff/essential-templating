using System.Diagnostics.Contracts;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Essential.Templating.Storage
{
    internal static class Streamer
    {
        public static Stream ToStream(byte[] array)
        {
            Contract.Requires(array != null);

            var memoryStream = new MemoryStream(array);
            return memoryStream;
        }

        public static Stream ToStream(Bitmap bitmap)
        {
            Contract.Requires(bitmap != null);

            var memoryStream = new MemoryStream();
            bitmap.Save(memoryStream, bitmap.RawFormat);
            memoryStream.Position = 0;
            return memoryStream;
        }

        public static Stream ToStream(object obj)
        {
            Contract.Requires(obj != null);

            var serializer = new BinaryFormatter();
            var memoryStream = new MemoryStream();
            serializer.Serialize(memoryStream, obj);
            memoryStream.Position = 0;
            return memoryStream;
        }
    }
}