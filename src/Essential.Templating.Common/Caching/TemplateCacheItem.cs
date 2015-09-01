using System;
using System.Globalization;

namespace Essential.Templating.Common.Caching
{
    public class TemplateCacheItem<T>
    {
        private readonly TemplateCacheKey _key;

        private readonly T _templateInfo;

        public TemplateCacheItem(TemplateCacheKey key, T templateInfo)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            _key = key;
            _templateInfo = templateInfo;
        }

        public string Path
        {
            get { return _key.Path; }
        }

        public CultureInfo Culture
        {
            get { return _key.Culture; }
        }

        public T TemplateInfo
        {
            get { return _templateInfo; }
        }
    }
}