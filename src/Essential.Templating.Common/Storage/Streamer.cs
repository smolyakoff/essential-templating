using System;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Essential.Templating.Common.Storage
{
    internal static class Streamer
    {
        public static Stream ToStream(byte[] array)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }

            var memoryStream = new MemoryStream(array);
            return memoryStream;
        }

        public static Stream ToStream(Bitmap bitmap)
        {
            if (bitmap == null)
            {
                throw new ArgumentNullException("bitmap");
            }

            var memoryStream = new MemoryStream();
            bitmap.Save(memoryStream, bitmap.RawFormat);
            memoryStream.Position = 0;
            return memoryStream;
        }

        public static Stream ToStream(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            var serializer = new BinaryFormatter();
            var memoryStream = new MemoryStream();
            serializer.Serialize(memoryStream, obj);
            memoryStream.Position = 0;
            return memoryStream;
        }
    }
}