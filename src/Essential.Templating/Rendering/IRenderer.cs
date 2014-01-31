using System.Diagnostics.Contracts;
using Essential.Templating.Contracts;
using RazorEngine.Templating;

namespace Essential.Templating.Rendering
{
    [ContractClass(typeof(RendererContracts<,>))]
    public interface IRenderer<in TTemplate, out TResult>
        where TTemplate : Template
        where TResult : class
    {
        TResult Render(TTemplate template, DynamicViewBag viewBag);
    }
}