using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace Essential.Templating.Common.Storage
{
    public class EmbeddedResourceProvider : IResourceProvider
    {
        private readonly Assembly _baseAssembly;

        private readonly string _baseNamespace;

        public EmbeddedResourceProvider(Assembly baseAssembly, string baseNamespace)
        {
            Contract.Requires<ArgumentNullException>(baseAssembly != null);
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(baseNamespace), "Base namespace should not be empty.");

            _baseAssembly = baseAssembly;
            _baseNamespace = baseNamespace;
            
        }

        public EmbeddedResourceProvider(Assembly baseAssembly)
            : this(baseAssembly, baseAssembly.GetName().Name)
        {
            Contract.Requires<ArgumentNullException>(baseAssembly != null);
        }

        public EmbeddedResourceProvider()
            :this(Assembly.GetCallingAssembly())
        {
        }

        public Stream Get(string path, CultureInfo culture = null)
        {
            culture = culture ?? Thread.CurrentThread.CurrentCulture;
            Assembly cultureAssembly = null;
            var foundCulture =
                culture.GetPossibleCultures()
                    .FirstOrDefault(x => _baseAssembly.TryGetSatteliteAssembly(x, out cultureAssembly));
            if (cultureAssembly == null)
            {
                return null;
            }
            var fullName = ToFullNameWithNamespace(path);
            return cultureAssembly.GetManifestResourceStream(fullName);
        }

        private string ToFullNameWithNamespace(string path)
        {
            Contract.Requires(!string.IsNullOrEmpty(path));
            var className = path.Replace("/", ".").Replace("\\", ".").TrimStart('.');
            var fullName = string.Format("{0}.{1}", _baseNamespace, className);
            return fullName;
        }
    }
}
