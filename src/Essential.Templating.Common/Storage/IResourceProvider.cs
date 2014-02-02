using System.Diagnostics.Contracts;
using System.Globalization;
using System.IO;
using Essential.Templating.Common.Contracts;

namespace Essential.Templating.Common.Storage
{
    [ContractClass(typeof (ResourceProviderContracts))]
    public interface IResourceProvider
    {
        Stream Get(string path, CultureInfo culture = null);
    }
}