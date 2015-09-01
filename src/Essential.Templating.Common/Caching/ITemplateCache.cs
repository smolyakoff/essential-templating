using System;

namespace Essential.Templating.Common.Caching
{
    public interface ITemplateCache<T>
    {
        bool ContainsKey(TemplateCacheKey key);

        bool Put(TemplateCacheKey key, T templateInfo, TimeSpan slidingExpiration);

        TemplateCacheItem<T> Get(TemplateCacheKey key);
    }
}