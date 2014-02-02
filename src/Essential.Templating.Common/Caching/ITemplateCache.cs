using System;
using System.Diagnostics.Contracts;
using Essential.Templating.Common.Contracts;

namespace Essential.Templating.Common.Caching
{
    [ContractClass(typeof(TemplateCacheContracts<>))]
    public interface ITemplateCache<T>
    {
        bool ContainsKey(string path);

        bool Put(string path, T templateInfo, TimeSpan slidingExpiration);

        TemplateCacheItem<T> Get(string path);
    }
}