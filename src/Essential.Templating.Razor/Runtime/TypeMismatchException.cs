using System;
using System.Diagnostics.Contracts;

namespace Essential.Templating.Razor.Runtime
{
    public class TypeMismatchException : ActivatorException
    {
        public TypeMismatchException(Type templateType, Type expectedBaseTemplateType)
            : base(templateType)
        {
            Contract.Requires(templateType != null);
            Contract.Requires(expectedBaseTemplateType != null);

            ExpectedBaseTemplateType = expectedBaseTemplateType;
        }

        public override string Message
        {
            get
            {
                return string.Format("Expected base template type: {0}. Actual base template type: {1}",
                    ExpectedBaseTemplateType, BaseTemplateType);
            }
        }

        public Type ExpectedBaseTemplateType { get; private set; }
    }
}