using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Reflection;

namespace Essential.Templating.Common.Storage
{
    internal static class StorageExtensions
    {
        public static IEnumerable<CultureInfo> GetPossibleCultures(this CultureInfo culture)
        {
            Contract.Requires(culture != null);
            if (culture.Equals(CultureInfo.InvariantCulture))
            {
                yield return CultureInfo.InvariantCulture;
            }
            else
            {
                yield return culture;
                yield return new CultureInfo(culture.TwoLetterISOLanguageName);
                yield return CultureInfo.InvariantCulture;
            }
        }

        public static bool TryGetSatteliteAssembly(this Assembly assembly, CultureInfo culture,
            out Assembly satteliteAssembly)
        {
            Contract.Requires(assembly != null);
            Contract.Requires(culture != null);

            if (culture.Equals(CultureInfo.InvariantCulture))
            {
                satteliteAssembly = assembly;
                return true;
            }
            try
            {
                satteliteAssembly = assembly.GetSatelliteAssembly(culture);
            }
            catch
            {
                satteliteAssembly = null;
                return false;
            }
            return true;
        }
    }
}
