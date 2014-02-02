using System;
using System.Collections.Concurrent;

namespace Essential.Templating.Common.Caching
{
    internal class LocalTemplateCache<T> : ITemplateCache<T>
    {
        private readonly ConcurrentDictionary<string, LocalTemplateCacheItem<T>> _cache =
            new ConcurrentDictionary<string, LocalTemplateCacheItem<T>>();

        public bool ContainsKey(string path)
        {
            var item = EnsureExpiration(path);
            return item != null;
        }

        public bool Put(string path, T templateInfo, TimeSpan slidingExpiration)
        {
            var item = new LocalTemplateCacheItem<T>(new TemplateCacheItem<T>(path, templateInfo), slidingExpiration);
            _cache[path] = item;
            return true;
        }

        public TemplateCacheItem<T> Get(string path)
        {
            var item = EnsureExpiration(path);
            if (item == null)
            {
                return null;
            }
            item.LastAccessTime = DateTime.UtcNow;
            return item.Item;
        }

        private LocalTemplateCacheItem<T> EnsureExpiration(string path)
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