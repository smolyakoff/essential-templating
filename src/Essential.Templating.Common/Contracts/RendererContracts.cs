using System;
using System.Diagnostics.Contracts;
using Essential.Templating.Common.Rendering;

namespace Essential.Templating.Common.Contracts
{
    [ContractClassFor(typeof(IRenderer<,>))]
    internal abstract class RendererContracts<TTemplate, TResult> : IRenderer<TTemplate, TResult>
        where TTemplate : class
        where TResult : class
    {
        public TResult Render(TTemplate template, object viewBag)
        {
            Contract.Requires<ArgumentNullException>(template != null);
            Contract.Ensures(Contract.Result<TResult>() != null);

            throw new System.NotImplementedException();
        }
    }
}
