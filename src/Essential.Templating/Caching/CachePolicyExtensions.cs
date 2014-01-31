namespace Essential.Templating.Caching
{
    internal static class CachePolicyExtensions
    {
        internal static ITemplateCache GetCache(this CachePolicy cachePolicy)
        {
            switch (cachePolicy)
            {
                case CachePolicy.Instance:
                    return new LocalTemplateCache();
                case CachePolicy.Shared:
                    return new GlobalTemplateCache();
                default:
                    return new GlobalTemplateCache();
            }
        }
    }
}