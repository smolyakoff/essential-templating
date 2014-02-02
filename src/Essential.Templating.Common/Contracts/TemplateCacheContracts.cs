using System;
using System.Diagnostics.Contracts;
using Essential.Templating.Common.Caching;

namespace Essential.Templating.Common.Contracts
{
    [ContractClassFor(typeof(ITemplateCache<>))]
    internal abstract class TemplateCacheContracts<T> : ITemplateCache<T>
    {
        public bool ContainsKey(string path)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(path));

            throw new NotImplementedException();
        }

        public bool Put(string path, T templateInfo, TimeSpan slidingExpiration)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(path));
            Contract.Requires<ArgumentException>(slidingExpiration.Ticks > 0);

            throw new NotImplementedException();
        }

        public TemplateCacheItem<T> Get(string path)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(path));

            throw new NotImplementedException();
        }
    }
}
