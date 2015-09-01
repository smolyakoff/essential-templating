using RazorEngine;
using RazorEngine.Configuration;
using RazorEngine.Templating;

namespace Essential.Templating.Razor.Compilation
{
    internal class CSharpTemplateCompiler : BaseTemplateCompiler
    {
        public CSharpTemplateCompiler()
            : base(new TemplateService(new TemplateServiceConfiguration
            {
                Language = Language.CSharp,
                BaseTemplateType = typeof(Template)
            }))
        {
        }
    }
}