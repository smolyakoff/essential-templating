using System.IO;

namespace Essential.Templating.Razor.Email.Tests
{
    internal static class Extensions
    {
        public static string AsString(this Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}