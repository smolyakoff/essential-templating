using Essential.Templating.Common.Configuration;
using Essential.Templating.Razor.Runtime;
using RazorEngine;

namespace Essential.Templating.Razor.Configuration
{
    public class RazorTemplateEngineConfiguration : TemplateEngineConfiguration
    {
        private ITemplateFactory _templateFactory;

        public RazorTemplateEngineConfiguration()
        {
            TemplateFactory = new ReflectionTemplateFactory();
            CodeLanguage = Language.CSharp;
        }

        public ITemplateFactory TemplateFactory
        {
            get { return _templateFactory; }
            set { SetRequiredProperty(ref _templateFactory, value, "TemplateFactory"); }
        }

        public Language CodeLanguage { get; set; }
    }
}