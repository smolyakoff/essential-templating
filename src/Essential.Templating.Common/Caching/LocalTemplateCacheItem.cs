using System;

namespace Essential.Templating.Common.Caching
{
    internal class LocalTemplateCacheItem<T>
    {
        private readonly TemplateCacheItem<T> _item;

        private readonly TimeSpan _slidingExpiration;

        public LocalTemplateCacheItem(TemplateCacheItem<T> item, TimeSpan slidingExpiration)
        {
            _item = item;
            _slidingExpiration = slidingExpiration;
            LastAccessTime = DateTime.UtcNow;
        }

        public TemplateCacheItem<T> Item
        {
            get { return _item; }
        }

        public DateTime LastAccessTime { get; set; }

        public TimeSpan SlidingExpiration
        {
            get { return _slidingExpiration; }
        }
    }
}