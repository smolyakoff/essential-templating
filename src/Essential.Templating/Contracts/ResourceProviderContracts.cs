using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.IO;
using Essential.Templating.Storage;

namespace Essential.Templating.Contracts
{
    [ContractClassFor(typeof (IResourceProvider))]
    internal abstract class ResourceProviderContracts : IResourceProvider
    {
        public Stream Get(string path, CultureInfo culture = null)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(path));

            throw new NotImplementedException();
        }
    }
}