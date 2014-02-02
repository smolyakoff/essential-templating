using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;

namespace Essential.Templating.Common.Storage
{
    public class ResxClassResourceProvider<T> : IResourceProvider
        where T : class
    {
        private readonly ResourceManager _resourceManager;

        private ResxClassResourceProvider(ResourceManager resourceManager)
        {
            Contract.Requires(resourceManager != null);

            _resourceManager = resourceManager;
        }

        public Stream Get(string path, CultureInfo culture = null)
        {
            culture = culture ?? CultureInfo.InvariantCulture;
            dynamic resource = _resourceManager.GetObject(path, culture);
            return resource == null ? null : Streamer.ToStream(resource);
        }

        public static ResxClassResourceProvider<T> Create()
        {
            var resxClassType = typeof (T);
            var resourceManagerProperty = resxClassType.GetProperty("ResourceManager",
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            if (resourceManagerProperty == null)
            {
                throw new InvalidOperationException(string.Format("ResourceManager property was not found in {0}.",
                    resxClassType));
            }
            ResourceManager resourceManager;
            try
            {
                resourceManager = resourceManagerProperty.GetValue(null, null) as ResourceManager;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    string.Format("ResourceManager property was not found in {0}.", resxClassType), ex);
            }
            if (resourceManager == null)
            {
                throw new InvalidOperationException(string.Format("ResourceManager property was not found in {0}.",
                    resxClassType));
            }
            return new ResxClassResourceProvider<T>(resourceManager);
        }
    }
}