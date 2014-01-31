using System;
using System.Diagnostics.Contracts;
using Essential.Templating.Caching;
using Essential.Templating.Storage;
using RazorEngine;

namespace Essential.Templating.Configuration
{
    public static class TemplateEngineBuilderExtensions
    {
        public static TemplateEngineBuilder FindTemplatesInDirectory(this TemplateEngineBuilder builder,
            string directory)
        {
            Contract.Requires<ArgumentNullException>(builder != null);
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(directory));

            builder.LocateResourcesWith(new FileSystemResourceProvider(directory));
            return builder;
        }

        public static TemplateEngineBuilder FindTemplatesInResxClass<T>(this TemplateEngineBuilder builder)
            where T : class
        {
            Contract.Requires<ArgumentNullException>(builder != null);

            builder.LocateResourcesWith(ResxClassResourceProvider<T>.Create());
            return builder;
        }

        public static TemplateEngineBuilder UseInstanceCache(this TemplateEngineBuilder builder)
        {
            Contract.Requires<ArgumentNullException>(builder != null);

            builder.UseCachePolicy(CachePolicy.Instance);
            return builder;
        }

        public static TemplateEngineBuilder UseSharedCache(this TemplateEngineBuilder builder)
        {
            Contract.Requires<ArgumentNullException>(builder != null);

            builder.UseCachePolicy(CachePolicy.Shared);
            return builder;
        }

        public static TemplateEngineBuilder CodeInCSharp(this TemplateEngineBuilder builder)
        {
            Contract.Requires<ArgumentNullException>(builder != null);

            builder.UseCodeLanguage(Language.CSharp);
            return builder;
        }

        public static TemplateEngineBuilder CodeInVisualBasic(this TemplateEngineBuilder builder)
        {
            Contract.Requires<ArgumentNullException>(builder != null);

            builder.UseCodeLanguage(Language.VisualBasic);
            return builder;
        }
    }
}