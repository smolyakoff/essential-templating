using System;
using System.Diagnostics.Contracts;
using Essential.Templating.Common.Contracts;

namespace Essential.Templating.Common.Caching
{
    [ContractClass(typeof(TemplateCacheContracts<>))]
    public interface ITemplateCache<T>
    {
        bool ContainsKey(TemplateCacheKey key);

        bool Put(TemplateCacheKey key, T templateInfo, TimeSpan slidingExpiration);

        TemplateCacheItem<T> Get(TemplateCacheKey key);
    }
}