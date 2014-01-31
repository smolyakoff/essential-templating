using System.Threading;
using RazorEngine.Templating;

namespace Essential.Templating.Rendering
{
    public class StringRenderer : IRenderer<Template, string>
    {
        public string Render(Template template, DynamicViewBag viewBag)
        {
            var previousCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = template.Culture;
            var result = ((ITemplate) template).Run(new ExecuteContext(viewBag));
            Thread.CurrentThread.CurrentCulture = previousCulture;
            return result;
        }
    }
}