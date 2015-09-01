using System;

namespace Essential.Templating.Razor.Runtime
{
    public class TypeMismatchException : ActivatorException
    {
        public TypeMismatchException(Type templateType, Type expectedBaseTemplateType)
            : base(templateType)
        {
            if (expectedBaseTemplateType == null)
            {
                throw new ArgumentNullException("expectedBaseTemplateType");
            }

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