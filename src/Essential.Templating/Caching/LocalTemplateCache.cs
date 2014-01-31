using System;
using System.Collections.Concurrent;

namespace Essential.Templating.Caching
{
    internal class LocalTemplateCache : ITemplateCache
    {
        private readonly ConcurrentDictionary<string, LocalTemplateCacheItem> _cache =
            new ConcurrentDictionary<string, LocalTemplateCacheItem>();

        public bool ContainsKey(string path)
        {
            var item = EnsureExpiration(path);
            return item != null;
        }

        public bool Put(string path, Type templateType, TimeSpan slidingExpiration)
        {
            var item = new LocalTemplateCacheItem(new TemplateCacheItem(path, templateType), slidingExpiration);
            _cache[path] = item;
            return true;
        }

        public TemplateCacheItem Get(string path)
        {
            var item = EnsureExpiration(path);
            if (item == null)
            {
                return null;
            }
            item.LastAccessTime = DateTime.UtcNow;
            return item.Item;
        }

        private LocalTemplateCacheItem EnsureExpiration(string path)
        {
            if (!_cache.ContainsKey(path))
            {
                return null;
            }
            var item = _cache[path];
            if (item.LastAccessTime + item.SlidingExpiration < DateTime.UtcNow)
            {
                _cache.TryRemove(path, out item);
                return null;
            }
            return item;
        }
    }
}