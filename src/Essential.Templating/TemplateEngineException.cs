using System;
using System.Diagnostics.Contracts;

namespace Essential.Templating
{
    public class TemplateEngineException : Exception
    {
        private readonly string _message;

        public TemplateEngineException(string message)
        {
            _message = message;
        }

        public TemplateEngineException(Exception innerException) : base(null, innerException)
        {
            Contract.Requires(innerException != null);
        }

        public TemplateEngineException(string message, Exception innerException) : base(message, innerException)
        {
            Contract.Requires(!string.IsNullOrEmpty(message));
            Contract.Requires(innerException != null);

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