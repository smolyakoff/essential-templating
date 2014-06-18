using System;
using System.Diagnostics.Contracts;
using System.Globalization;

namespace Essential.Templating.Common.Caching
{
    public class TemplateCacheKey : IEquatable<TemplateCacheKey>
    {
        private readonly string _path;

        private readonly CultureInfo _culture;

        public TemplateCacheKey(string path, CultureInfo culture)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(path));
            Contract.Requires<ArgumentNullException>(culture != null);

            _path = path;
            _culture = culture;
        }

        public string Path
        {
            get { return _path; }
        }

        public CultureInfo Culture
        {
            get { return _culture; }
        }

        public bool Equals(TemplateCacheKey other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(_path, other._path) && _culture.Equals(other._culture);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((TemplateCacheKey)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_path.GetHashCode() * 397) ^ _culture.GetHashCode();
            }
        }

        public override string ToString()
        {
            return string.Format("{0}__{1}", Path, Culture);
        }
    }
}
