using System;
using System.Diagnostics.Contracts;

namespace Essential.Templating.Common.Storage
{
    [Serializable]
    public class ResourceNotFoundException : Exception
    {
        private const string DefaultMessage = "The specified resource was not found. Attempted path: '{0}'";

        public string Path { get; protected set; }

        public ResourceNotFoundException(string path) 
            : base(string.Format(DefaultMessage, path))
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(path));

            Path = path;
        }

        public ResourceNotFoundException(string message, string path) 
            : base(message)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(message));
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(path));

            Path = path;
        }

        public ResourceNotFoundException(string message, string path, Exception innerException)
            :base(message, innerException)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(message));
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(path));
            Contract.Requires<ArgumentNullException>(innerException != null);

            Path = path;
        }

        public ResourceNotFoundException(string path, Exception innerException)
            : base(string.Format(DefaultMessage, path), innerException)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(path));
            Contract.Requires<ArgumentNullException>(innerException != null);

            Path = path;
        }
    }
}
