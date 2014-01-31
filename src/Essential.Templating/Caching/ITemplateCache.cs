using System;

namespace Essential.Templating.Caching
{
    internal interface ITemplateCache
    {
        bool ContainsKey(string path);

        bool Put(string path, Type templateType, TimeSpan slidingExpiration);

        TemplateCacheItem Get(string path);
    }
}