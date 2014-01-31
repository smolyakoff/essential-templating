using System;
using System.Diagnostics.Contracts;

namespace Essential.Templating.Runtime
{
    public class ActivatorException : Exception
    {
        public ActivatorException(Type templateType)
        {
            Contract.Requires(templateType != null);

            TemplateType = templateType;
        }

        public ActivatorException(Type templateType, Exception innerException) : base(null, innerException)
        {
            TemplateType = templateType;
        }

        public override string Message
        {
            get
            {
                return string.Format("Can't create instance of template [{0}].{1}Base type: [{2}].", TemplateType,
                    Environment.NewLine, BaseTemplateType);
            }
        }

        public Type TemplateType { get; private set; }

        public Type BaseTemplateType
        {
            get { return TemplateType.BaseType; }
        }
    }
}