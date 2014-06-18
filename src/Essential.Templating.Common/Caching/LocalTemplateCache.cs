using System;
using System.Collections.Concurrent;

namespace Essential.Templating.Common.Caching
{
    internal class LocalTemplateCache<T> : ITemplateCache<T>
    {
        private readonly ConcurrentDictionary<TemplateCacheKey, LocalTemplateCacheItem<T>> _cache =
            new ConcurrentDictionary<TemplateCacheKey, LocalTemplateCacheItem<T>>();

        public bool ContainsKey(TemplateCacheKey key)
        {
            var item = EnsureExpiration(key);
            return item != null;
        }

        public bool Put(TemplateCacheKey key, T templateInfo, TimeSpan slidingExpiration)
        {
            var item = new LocalTemplateCacheItem<T>(new TemplateCacheItem<T>(key, templateInfo), slidingExpiration);
            _cache[key] = item;
            return true;
        }

        public TemplateCacheItem<T> Get(TemplateCacheKey key)
        {
            var item = EnsureExpiration(key);
            if (item == null)
            {
                return null;
            }
            item.LastAccessTime = DateTime.UtcNow;
            return item.Item;
        }

        private LocalTemplateCacheItem<T> EnsureExpiration(TemplateCacheKey key)
        {
            if (!_cache.ContainsKey(key))
            {
                return null;
            }
            var item = _cache[key];
            if (item.LastAccessTime + item.SlidingExpiration < DateTime.UtcNow)
            {
                _cache.TryRemove(key, out item);
                return null;
            }
            return item;
        }
    }
}