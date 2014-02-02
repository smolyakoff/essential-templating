using System.Diagnostics.Contracts;

namespace Essential.Templating.Common.Caching
{
    public class TemplateCacheItem<T>
    {
        private readonly string _path;

        private readonly T _templateInfo;

        public TemplateCacheItem(string path, T templateInfo)
        {
            Contract.Requires(!string.IsNullOrEmpty(path));

            _path = path;
            _templateInfo = templateInfo;
        }

        public string Path
        {
            get { return _path; }
        }

        public T TemplateInfo
        {
            get { return _templateInfo; }
        }
    }
}