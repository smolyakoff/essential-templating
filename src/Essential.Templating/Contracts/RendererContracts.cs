using System;
using System.Diagnostics.Contracts;
using Essential.Templating.Rendering;
using RazorEngine.Templating;

namespace Essential.Templating.Contracts
{
    [ContractClassFor(typeof(IRenderer<,>))]
    internal abstract class RendererContracts<TTemplate, TResult> : IRenderer<TTemplate, TResult>
        where TTemplate : Template
        where TResult : class
    {
        public TResult Render(TTemplate template, DynamicViewBag viewBag)
        {
            Contract.Requires<ArgumentNullException>(template != null);
            Contract.Requires<ArgumentNullException>(viewBag != null);
            Contract.Ensures(Contract.Result<TResult>() != null);

            throw new System.NotImplementedException();
        }
    }
}
