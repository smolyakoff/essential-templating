using System;
using System.Diagnostics.Contracts;
using Essential.Templating.Razor.Contracts;

namespace Essential.Templating.Razor.Runtime
{
    [ContractClass(typeof (TemplateFactoryContracts))]
    public interface ITemplateFactory
    {
        Template Create(Type templateType, TemplateContext templateContext);
    }
}