using System.IO;
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
            var previousUICulture = Thread.CurrentThread.CurrentUICulture;
            Thread.CurrentThread.CurrentCulture = template.Culture;
            Thread.CurrentThread.CurrentUICulture = template.Culture;
            using (var writer = new StringWriter())
            {
                ((ITemplate)template).Run(new ExecuteContext(new ObjectViewBag(viewBag)), writer);
                Thread.CurrentThread.CurrentCulture = previousCulture;
                Thread.CurrentThread.CurrentUICulture = previousUICulture;
                var result = writer.GetStringBuilder().ToString();
                return result;  
            }
        }
    }
}