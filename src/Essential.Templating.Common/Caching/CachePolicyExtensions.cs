namespace Essential.Templating.Common.Caching
{
    internal static class CachePolicyExtensions
    {
        internal static ITemplateCache<T> GetCache<T>(this CachePolicy cachePolicy)
        {
            switch (cachePolicy)
            {
                case CachePolicy.Instance:
                    return new LocalTemplateCache<T>();
                case CachePolicy.Shared:
                    return new GlobalTemplateCache<T>();
                default:
                    return new GlobalTemplateCache<T>();
            }
        }
    }
}