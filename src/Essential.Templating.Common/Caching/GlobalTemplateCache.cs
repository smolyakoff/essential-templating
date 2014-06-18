using System;
using System.Runtime.Caching;

namespace Essential.Templating.Common.Caching
{
    internal class GlobalTemplateCache<T> : ITemplateCache<T>
    {
        private const string CacheKeyTemplate = "Essential.Templating.CacheItem<{0}>[{1}]";

        public bool ContainsKey(TemplateCacheKey key)
        {
            var cacheKey = string.Format(CacheKeyTemplate, typeof(T), key);
            return MemoryCache.Default.Contains(cacheKey, null);
        }

        public bool Put(TemplateCacheKey key, T templateInfo, TimeSpan slidingExpiration)
        {
            var cacheKey = string.Format(CacheKeyTemplate, typeof(T), key);
            return MemoryCache.Default.Add(cacheKey, new TemplateCacheItem<T>(key, templateInfo),
                new CacheItemPolicy {SlidingExpiration = slidingExpiration});
        }

        public TemplateCacheItem<T> Get(TemplateCacheKey key)
        {
            var cacheKey = string.Format(CacheKeyTemplate, typeof(T), key);
            var item = MemoryCache.Default.Get(cacheKey) as TemplateCacheItem<T>;
            return item;
        }
    }
}