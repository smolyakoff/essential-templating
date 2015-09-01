using Essential.Templating.Common;
using Essential.Templating.Common.Configuration;

namespace Essential.Templating.Razor.Configuration
{
    public class RazorTemplateEngineBuilder : TemplateEngineBuilder<RazorTemplateEngineConfiguration>
    {
        protected override ITemplateEngine Build(RazorTemplateEngineConfiguration configuration)
        {
            return new RazorTemplateEngine(configuration);
        }

        protected override RazorTemplateEngineConfiguration CreateDefaultConfiguration()
        {
            return new RazorTemplateEngineConfiguration();
        }
    }
}