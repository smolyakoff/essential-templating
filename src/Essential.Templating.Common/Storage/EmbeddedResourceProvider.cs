﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Essential.Templating.Common.Storage
{
    public class EmbeddedResourceProvider : IResourceProvider
    {
        private readonly Assembly _baseAssembly;

        private readonly string _baseNamespace;

        public EmbeddedResourceProvider(Assembly baseAssembly, string baseNamespace)
        {
            if (baseAssembly == null)
            {
                throw new ArgumentNullException("baseAssembly");
            }

            if (string.IsNullOrEmpty(baseNamespace))
            {
                throw new ArgumentException("Base namespace cannot be null or empty.", "baseNamespace");
            }

            _baseAssembly = baseAssembly;
            _baseNamespace = baseNamespace;
        }

        public EmbeddedResourceProvider(Assembly baseAssembly)
            : this(baseAssembly, baseAssembly.GetName().Name)
        {
        }

        public EmbeddedResourceProvider()
            : this(Assembly.GetCallingAssembly())
        {
        }

        public Stream Get(string path, CultureInfo culture = null)
        {
            culture = culture ?? Thread.CurrentThread.CurrentCulture;
            var fullName = ToFullNameWithNamespace(path);
            var attemptedAssemblies = new List<Assembly>();
            foreach (var possibleCulture in culture.GetPossibleCultures())
            {
                Assembly cultureAssembly;
                if (!_baseAssembly.TryGetSatteliteAssembly(possibleCulture, out cultureAssembly))
                {
                    continue;
                }

                attemptedAssemblies.Add(cultureAssembly);
                var stream = cultureAssembly.GetManifestResourceStream(fullName);
                if (stream != null)
                {
                    return stream;
                }
            }

            var messageBuilder = new StringBuilder();
            messageBuilder.AppendLine("The specified resource was not found.")
                .AppendLine(string.Format("Resource name: {0}. Attempted assemblies:", fullName));
            foreach (var attemptedAssembly in attemptedAssemblies)
                messageBuilder.AppendLine(string.Format(" - {0}", attemptedAssembly.FullName));
            throw new ResourceNotFoundException(messageBuilder.ToString(), path);
        }

        private string ToFullNameWithNamespace(string path)
        {
            var className = path.Replace("/", ".").Replace("\\", ".").TrimStart('.');
            var fullName = string.Format("{0}.{1}", _baseNamespace, className);
            return fullName;
        }
    }
}