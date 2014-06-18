using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using Essential.Templating.Common.Caching;

namespace Essential.Templating.Common.Contracts
{
    [ContractClassFor(typeof(ITemplateCache<>))]
    internal abstract class TemplateCacheContracts<T> : ITemplateCache<T>
    {
        public bool ContainsKey(TemplateCacheKey key)
        {
            Contract.Requires<ArgumentNullException>(key != null);

            throw new NotImplementedException();
        }

        public bool Put(TemplateCacheKey key, T templateInfo, TimeSpan slidingExpiration)
        {
            Contract.Requires<ArgumentNullException>(key != null);
            Contract.Requires<ArgumentException>(slidingExpiration.Ticks > 0);

            throw new NotImplementedException();
        }

        public TemplateCacheItem<T> Get(TemplateCacheKey key)
        {
            Contract.Requires<ArgumentNullException>(key != null);

            throw new NotImplementedException();
        }
    }
}
