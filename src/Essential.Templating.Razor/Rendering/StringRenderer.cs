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
            try
            {
                using (var writer = new StringWriter())
                {
                    template.SetData(null, new ObjectViewBag(viewBag));
                    ((ITemplate) template).Run(new ExecuteContext(), writer);
                    var result = writer.GetStringBuilder().ToString();
                    return result;
                }
            }
            finally
            {
                Thread.CurrentThread.CurrentCulture = previousCulture;
                Thread.CurrentThread.CurrentUICulture = previousUICulture;
            }
        }
    }
}