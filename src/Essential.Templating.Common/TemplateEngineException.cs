using System;

namespace Essential.Templating.Common
{
    public class TemplateEngineException : Exception
    {
        private readonly string _message;

        public TemplateEngineException(Exception innerException) : base(null, innerException)
        {
            if (innerException == null)
            {
                throw new ArgumentNullException("innerException");
            }
        }

        public TemplateEngineException(string message, Exception innerException) : base(message, innerException)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentException("Value cannot be null or empty.", "message");
            }

            if (innerException == null)
            {
                throw new ArgumentNullException("innerException");
            }

            _message = message;
        }

        public override string Message
        {
            get { return FormatMessage(); }
        }

        private string FormatMessage()
        {
            if (string.IsNullOrEmpty(_message) && InnerException == null)
            {
                return base.Message;
            }

            if (string.IsNullOrEmpty(_message) && InnerException != null)
            {
                return InnerException.Message;
            }

            if (InnerException != null)
            {
                return string.Format("{0}{1}{2}", _message, Environment.NewLine, InnerException.Message);
            }

            return _message;
        }
    }
}