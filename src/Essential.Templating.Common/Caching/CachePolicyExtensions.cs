namespace Essential.Templating.Common.Caching
{
    public static class CachePolicyExtensions
    {
        public static ITemplateCache<T> GetCache<T>(this CachePolicy cachePolicy)
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