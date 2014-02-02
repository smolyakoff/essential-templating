using System;

namespace Essential.Templating.Common.Caching
{
    internal interface ITemplateCache<T>
    {
        bool ContainsKey(string path);

        bool Put(string path, T templateInfo, TimeSpan slidingExpiration);

        TemplateCacheItem<T> Get(string path);
    }
}