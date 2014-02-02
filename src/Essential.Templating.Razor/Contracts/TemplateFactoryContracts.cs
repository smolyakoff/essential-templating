using System;
using System.Diagnostics.Contracts;
using Essential.Templating.Razor.Runtime;

namespace Essential.Templating.Razor.Contracts
{
    [ContractClassFor(typeof (ITemplateFactory))]
    internal abstract class TemplateFactoryContracts : ITemplateFactory
    {
        public Template Create(Type templateType, TemplateContext templateContext)
        {
            Contract.Requires<ArgumentNullException>(templateType != null);
            Contract.Requires<ArgumentNullException>(templateContext != null);

            throw new NotImplementedException();
        }
    }
}