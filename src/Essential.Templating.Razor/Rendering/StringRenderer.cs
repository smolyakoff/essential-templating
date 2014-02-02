using System.Diagnostics.Contracts;
using System.Threading;
using Essential.Templating.Common.Rendering;
using RazorEngine.Templating;

namespace Essential.Templating.Razor.Rendering
{
    public class StringRenderer : IRenderer<Template, string>
    {
        public string Render(Template template, object viewBag)
        {
            var previousCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = template.Culture;
            var result = ((ITemplate) template).Run(new ExecuteContext(new ObjectViewBag(viewBag)));
            Thread.CurrentThread.CurrentCulture = previousCulture;
            return result;
        }
    }
}