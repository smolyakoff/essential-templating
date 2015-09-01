using System.Globalization;
using System.IO;

namespace Essential.Templating.Common.Storage
{
    public interface IResourceProvider
    {
        Stream Get(string path, CultureInfo culture = null);
    }
}