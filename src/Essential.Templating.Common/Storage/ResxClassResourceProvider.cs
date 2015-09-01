using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Text;

namespace Essential.Templating.Common.Storage
{
    public class ResxClassResourceProvider<T> : IResourceProvider
        where T : class
    {
        private readonly ResourceManager _resourceManager;

        private ResxClassResourceProvider(ResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
        }

        public Stream Get(string path, CultureInfo culture = null)
        {
            culture = culture ?? CultureInfo.InvariantCulture;
            dynamic resource = _resourceManager.GetObject(path, culture);
            if (resource != null)
            {
                return Streamer.ToStream(resource);
            }

            var messageBuilder = new StringBuilder();
            messageBuilder
                .AppendLine("The specified resource was not found.")
                .AppendLine(string.Format("Resource manager type: {0}", _resourceManager.GetType().FullName))
                .AppendLine(string.Format("Attempted culture: {0}", culture));
            throw new ResourceNotFoundException(messageBuilder.ToString(), path);
        }

        public static ResxClassResourceProvider<T> Create()
        {
            var resxClassType = typeof(T);
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