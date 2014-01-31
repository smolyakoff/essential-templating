using System;
using System.Diagnostics.Contracts;
using Essential.Templating.Contracts;

namespace Essential.Templating.Runtime
{
    [ContractClass(typeof (TemplateFactoryContracts))]
    public interface ITemplateFactory
    {
        Template Create(Type templateType, TemplateContext templateContext);
    }
}