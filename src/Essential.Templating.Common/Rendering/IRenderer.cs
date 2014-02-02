using System.Diagnostics.Contracts;
using Essential.Templating.Common.Contracts;

namespace Essential.Templating.Common.Rendering
{
    [ContractClass(typeof(RendererContracts<,>))]
    public interface IRenderer<in TTemplate, out TResult>
        where TTemplate : class
        where TResult : class
    {
        TResult Render(TTemplate template, object viewBag);
    }
}