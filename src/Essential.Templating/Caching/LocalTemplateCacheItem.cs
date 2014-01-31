using System;

namespace Essential.Templating.Caching
{
    internal class LocalTemplateCacheItem
    {
        private readonly TemplateCacheItem _item;

        private readonly TimeSpan _slidingExpiration;

        public LocalTemplateCacheItem(TemplateCacheItem item, TimeSpan slidingExpiration)
        {
            _item = item;
            _slidingExpiration = slidingExpiration;
            LastAccessTime = DateTime.UtcNow;
        }

        public TemplateCacheItem Item
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