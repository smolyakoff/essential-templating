using System;
using System.Collections.Concurrent;
using System.Diagnostics.Contracts;
using System.Reflection;

namespace Essential.Templating.Razor.Runtime
{
    public class ReflectionTemplateFactory : ITemplateFactory
    {
        private static readonly ConcurrentDictionary<Type, ConstructorInfo> ConstructorCache = new ConcurrentDictionary<Type, ConstructorInfo>(); 

        public Template Create(Type templateType, TemplateContext templateContext)
        {
            ConstructorInfo constructor;
            if (ConstructorCache.TryGetValue(templateType, out constructor))
            {
                var template = constructor.Invoke(new object[] { templateContext }) as Template;
                return template;
            }
            constructor = FindConstructor(templateType);
            if (constructor == null)
            {
                return null;
            }
            ConstructorCache.TryAdd(templateType, constructor);
            return constructor.Invoke(new object[] {templateContext}) as Template;
        }

        private static ConstructorInfo FindConstructor(Type templateType)
        {
            Contract.Requires(templateType != null);
            return templateType.GetConstructor(new[] {typeof (TemplateContext)});
        }
    }
}