using System;
using System.Diagnostics.Contracts;
using Essential.Templating.Caching;
using Essential.Templating.Runtime;
using Essential.Templating.Storage;
using RazorEngine;

namespace Essential.Templating.Configuration
{
    public class TemplateEngineBuilder
    {
        private readonly TemplateEngineConfiguration _configuration = new TemplateEngineConfiguration();

        public TemplateEngineBuilder LocateResourcesWith(IResourceProvider resourceProvider)
        {
            Contract.Requires<ArgumentNullException>(resourceProvider != null);

            _configuration.ResourceProvider = resourceProvider;
            return this;
        }

        public TemplateEngineBuilder CreateTemplatesWith(ITemplateFactory templateFactory)
        {
            Contract.Requires<ArgumentNullException>(templateFactory != null);

            _configuration.TemplateFactory = templateFactory;
            return this;
        }

        public TemplateEngineBuilder UseCachePolicy(CachePolicy cachePolicy)
        {
            _configuration.CachePolicy = cachePolicy;
            return this;
        }

        public TemplateEngineBuilder CacheExpiresIn(TimeSpan timeSpan)
        {
            Contract.Requires<ArgumentException>(timeSpan.Ticks > 0);

            _configuration.CacheExpiration = timeSpan;
            return this;
        }

        public TemplateEngineBuilder UseCodeLanguage(Language language)
        {
            _configuration.CodeLanguage = language;
            return this;
        }

        public ITemplateEngine Build()
        {
            return new TemplateEngine(_configuration);
        }
    }
}