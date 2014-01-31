using System;
using Essential.Templating.Caching;
using Essential.Templating.Runtime;
using Essential.Templating.Storage;
using RazorEngine;

namespace Essential.Templating.Configuration
{
    public class TemplateEngineConfiguration
    {
        private IResourceProvider _resourceProvider;

        private ITemplateFactory _templateFactory;

        public TemplateEngineConfiguration()
        {
            TemplateFactory = new ReflectionTemplateFactory();
            ResourceProvider = new FileSystemResourceProvider();
            CodeLanguage = Language.CSharp;
            CachePolicy = CachePolicy.Shared;
            CacheExpiration = TimeSpan.FromHours(4.0);
        }

        public IResourceProvider ResourceProvider
        {
            get { return _resourceProvider; }
            set { SetRequiredProperty(ref _resourceProvider, value, "ResourceProvider"); }
        }

        public ITemplateFactory TemplateFactory
        {
            get { return _templateFactory; }
            set { SetRequiredProperty(ref _templateFactory, value, "TemplateFactory"); }
        }

        public Language CodeLanguage { get; set; }

        public CachePolicy CachePolicy { get; set; }

        public TimeSpan CacheExpiration { get; set; }


        private static void SetRequiredProperty<T>(ref T property, T value, string propertyName)
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