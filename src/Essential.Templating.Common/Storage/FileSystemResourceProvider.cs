using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Essential.Templating.Common.Storage
{
    public class FileSystemResourceProvider : IResourceProvider
    {
        private readonly string _baseDirectory;

        public FileSystemResourceProvider()
            : this("Templates")
        {
        }

        public FileSystemResourceProvider(string baseDirectory)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(baseDirectory));
            if (baseDirectory.Any(x => Path.GetInvalidPathChars().Contains(x)))
            {
                throw new ArgumentException("Invalid base directory path.", "baseDirectory");
            }
            _baseDirectory = baseDirectory;
        }


        public Stream Get(string path, CultureInfo culture = null)
        {
            culture = culture ?? Thread.CurrentThread.CurrentCulture;

            var paths = EnumeratePaths(path, culture).ToList();
            var filePath = paths.Where(File.Exists).FirstOrDefault();
            if (filePath != null)
            {
                return new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            }
            var builder = new StringBuilder();
            builder.AppendLine("The specified resource was not found. The following files do not exist:");
            paths.ForEach(p => builder.AppendLine(string.Format(" - {0}", p)));
            throw new ResourceNotFoundException(builder.ToString(), path);
        }

        private IEnumerable<string> EnumeratePaths(string path, CultureInfo culture = null)
        {
            Contract.Requires(path != null);

            var filePath = Path.Combine(_baseDirectory, path.Replace('/', Path.DirectorySeparatorChar));
            var directory = Path.GetDirectoryName(filePath);
            var name = Path.Combine(directory ?? "\\", Path.GetFileNameWithoutExtension(filePath));
            var extension = Path.GetExtension(filePath);
            var localizedName = new LocalizedName(name, culture);
            return localizedName.GetPossibleNames()
                .Select(x => string.Format("{0}{1}", x, extension))
                .Select(Path.GetFullPath);
        }
    }
}