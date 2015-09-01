using System;

namespace Essential.Templating.Common.Configuration
{
    public class ConfigurationException : Exception
    {
        public ConfigurationException(string message) : base(message)
        {
        }

        public ConfigurationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ConfigurationException(Exception innerException)
            : base("Invalid template engine configuration.", innerException)
        {
        }
    }
}