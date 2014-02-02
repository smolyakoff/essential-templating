using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Essential.Templating.Common.Caching;
using Essential.Templating.Common.Storage;

namespace Essential.Templating.Common.Configuration
{
    public abstract class TemplateEngineBuilder<T>
        where T : TemplateEngineConfiguration
    {
        private readonly List<Action<T>> _changes = new List<Action<T>>(); 

        public TemplateEngineBuilder<T> LocateResourcesWith(IResourceProvider resourceProvider)
        {
            Contract.Requires<ArgumentNullException>(resourceProvider != null);

            Configure(c => c.ResourceProvider = resourceProvider);
            return this;
        }

        public TemplateEngineBuilder<T> UseCachePolicy(CachePolicy cachePolicy)
        {
            Configure(c => c.CachePolicy = cachePolicy);
            return this;
        }

        public TemplateEngineBuilder<T> CacheExpiresIn(TimeSpan expiration)
        {
            Contract.Requires<ArgumentException>(expiration.Ticks > 0);

            Configure(c => c.CacheExpiration = expiration);
            return this;
        }

        public ITemplateEngine Build()
        {
            Contract.Ensures(Contract.Result<ITemplateEngine>() != null);
            try
            {
                var configuration = CreateDefaultConfiguration();
                foreach (var change in _changes)
                {
                    change(configuration);
                }
                return Build(configuration);
            }
            catch (Exception ex)
            {
                throw new ConfigurationException(ex);
            }
        }

        protected abstract ITemplateEngine Build(T configuration);

        protected abstract T CreateDefaultConfiguration();

        protected void Configure(Action<T> configurator)
        {
            Contract.Requires(configurator != null);
            _changes.Add(configurator);
        }
    }
}