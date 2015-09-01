using System;
using Essential.Templating.Common.Caching;
using Essential.Templating.Common.Storage;

namespace Essential.Templating.Common.Configuration
{
    public static class TemplateEngineBuilderExtensions
    {
        public static TemplateEngineBuilder<TConfiguration> FindTemplatesInDirectory<TConfiguration>(
            this TemplateEngineBuilder<TConfiguration> builder,
            string directory) where TConfiguration : TemplateEngineConfiguration
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder");
            }

            if (string.IsNullOrEmpty(directory))
            {
                throw new ArgumentException("Expected directory to be not empty.", "directory");
            }

            builder.LocateResourcesWith(new FileSystemResourceProvider(directory));
            return builder;
        }

        public static TemplateEngineBuilder<TConfiguration> FindTemplatesInResxClass<TConfiguration, T>(
            this TemplateEngineBuilder<TConfiguration> builder)
            where T : class where TConfiguration : TemplateEngineConfiguration
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder");
            }

            builder.LocateResourcesWith(ResxClassResourceProvider<T>.Create());
            return builder;
        }

        public static TemplateEngineBuilder<TConfiguration> UseInstanceCache<TConfiguration>(
            this TemplateEngineBuilder<TConfiguration> builder)
            where TConfiguration : TemplateEngineConfiguration
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder");
            }

            builder.UseCachePolicy(CachePolicy.Instance);
            return builder;
        }

        public static TemplateEngineBuilder<TConfiguration> UseSharedCache<TConfiguration>(
            this TemplateEngineBuilder<TConfiguration> builder)
            where TConfiguration : TemplateEngineConfiguration
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder");
            }

            builder.UseCachePolicy(CachePolicy.Shared);
            return builder;
        }
    }
}