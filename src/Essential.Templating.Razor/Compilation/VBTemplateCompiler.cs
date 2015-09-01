using RazorEngine;
using RazorEngine.Configuration;
using RazorEngine.Templating;

namespace Essential.Templating.Razor.Compilation
{
    internal class VBTemplateCompiler : BaseTemplateCompiler
    {
        public VBTemplateCompiler()
            : base(new TemplateService(new TemplateServiceConfiguration
            {
                Language = Language.VisualBasic,
                BaseTemplateType = typeof(Template)
            }))
        {
        }
    }
}