using System;

namespace Essential.Templating.Razor.Email.Helpers
{
    public class TemplateHelperException : Exception
    {
        public TemplateHelperException(string message) : base(message)
        {
        }

        public TemplateHelperException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}