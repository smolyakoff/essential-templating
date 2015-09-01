using System;
using Essential.Templating.Common.Caching;
using Essential.Templating.Common.Storage;

namespace Essential.Templating.Common.Configuration
{
    public abstract class TemplateEngineConfiguration
    {
        private IResourceProvider _resourceProvider;

        protected TemplateEngineConfiguration()
        {
            ResourceProvider = new FileSystemResourceProvider();
            CachePolicy = CachePolicy.Shared;
            CacheExpiration = TimeSpan.FromHours(4.0);
        }

        public IResourceProvider ResourceProvider
        {
            get { return _resourceProvider; }
            set { SetRequiredProperty(ref _resourceProvider, value, "ResourceProvider"); }
        }

        public CachePolicy CachePolicy { get; set; }

        public TimeSpan CacheExpiration { get; set; }

        protected static void SetRequiredProperty<T>(ref T property, T value, string propertyName)
            where T : class
        {
            if (value == null)
            {
                throw new ArgumentNullException(propertyName, "Property is required.");
            }

            property = value;
        }
    }
}