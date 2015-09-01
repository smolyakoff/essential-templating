using System;

namespace Essential.Templating.Razor.Runtime
{
    public interface ITemplateFactory
    {
        Template Create(Type templateType, TemplateContext templateContext);
    }
}