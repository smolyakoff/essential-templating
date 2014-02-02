using System;
using System.Runtime.Caching;

namespace Essential.Templating.Common.Caching
{
    internal class GlobalTemplateCache<T> : ITemplateCache<T>
    {
        private const string CacheKeyTemplate = "Essential.Templating.CacheItem<{0}>[{1}]";

        public bool ContainsKey(string path)
        {
            var cacheKey = string.Format(CacheKeyTemplate, typeof(T), path);
            return MemoryCache.Default.Contains(cacheKey, null);
        }

        public bool Put(string path, T templateInfo, TimeSpan slidingExpiration)
        {
            var cacheKey = string.Format(CacheKeyTemplate, typeof(T), path);
            return MemoryCache.Default.Add(cacheKey, new TemplateCacheItem<T>(path, templateInfo),
                new CacheItemPolicy {SlidingExpiration = slidingExpiration});
        }

        public TemplateCacheItem<T> Get(string path)
        {
            var item = MemoryCache.Default.Get(path) as TemplateCacheItem<T>;
            return item;
        }
    }
}