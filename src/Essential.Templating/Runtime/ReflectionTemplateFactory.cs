using System;
using System.Diagnostics.Contracts;
using System.Reflection;
using System.Runtime.Caching;

namespace Essential.Templating.Runtime
{
    public class ReflectionTemplateFactory : ITemplateFactory
    {
        private const string CacheKeyTemplate = "Essential.Templating.Template[{0}]";

        public Template Create(Type templateType, TemplateContext templateContext)
        {
            var cacheKey = string.Format(CacheKeyTemplate, templateType);
            ConstructorInfo constructor;
            if (MemoryCache.Default.Contains(cacheKey))
            {
                constructor = MemoryCache.Default.Get(cacheKey) as ConstructorInfo;
                if (constructor != null)
                {
                    var template = constructor.Invoke(new object[] {templateContext}) as Template;
                    return template;
                }
            }
            constructor = FindConstructor(templateType);
            if (constructor == null)
            {
                return null;
            }
            MemoryCache.Default.Add(cacheKey, constructor, DateTime.MaxValue);
            return constructor.Invoke(new object[] {templateContext}) as Template;
        }

        private static ConstructorInfo FindConstructor(Type templateType)
        {
            Contract.Requires(templateType != null);
            return templateType.GetConstructor(new[] {typeof (TemplateContext)});
        }
    }
}