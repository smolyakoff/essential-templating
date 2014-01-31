using System;
using System.Runtime.Caching;

namespace Essential.Templating.Caching
{
    internal class GlobalTemplateCache : ITemplateCache
    {
        private const string CacheKeyTemplate = "Essential.Templating.CacheItem[{0}]";

        public bool ContainsKey(string path)
        {
            var cacheKey = string.Format(CacheKeyTemplate, path);
            return MemoryCache.Default.Contains(cacheKey, null);
        }

        public bool Put(string path, Type templateType, TimeSpan slidingExpiration)
        {
            var cacheKey = string.Format(CacheKeyTemplate, path);
            return MemoryCache.Default.Add(cacheKey, new TemplateCacheItem(path, templateType),
                new CacheItemPolicy {SlidingExpiration = slidingExpiration});
        }

        public TemplateCacheItem Get(string path)
        {
            var item = MemoryCache.Default.Get(path) as TemplateCacheItem;
            return item;
        }
    }
}