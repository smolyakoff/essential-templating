using System;
using System.Diagnostics.Contracts;
using Essential.Templating.Runtime;

namespace Essential.Templating.Contracts
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