using System;

namespace Essential.Templating.Common.Storage
{
    [Serializable]
    public class ResourceNotFoundException : Exception
    {
        private const string DefaultMessage = "The specified resource was not found. Attempted path: '{0}'";

        public ResourceNotFoundException(string path)
            : base(string.Format(DefaultMessage, path))
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Path cannot be null or empty.", "path");
            }

            Path = path;
        }

        public ResourceNotFoundException(string message, string path)
            : base(message)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentException("Message cannot be null or empty.", "message");
            }

            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Path cannot be null or empty.", "path");
            }

            Path = path;
        }

        public string Path { get; protected set; }
    }
}