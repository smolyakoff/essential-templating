using System;
using System.Diagnostics.Contracts;

namespace Essential.Templating.Caching
{
    internal class TemplateCacheItem
    {
        private readonly string _path;

        private readonly Type _templateType;

        public TemplateCacheItem(string path, Type templateType)
        {
            Contract.Requires(!string.IsNullOrEmpty(path));
            Contract.Requires(templateType != null);

            _path = path;
            _templateType = templateType;
        }

        public string Path
        {
            get { return _path; }
        }

        public Type TemplateType
        {
            get { return _templateType; }
        }
    }
}