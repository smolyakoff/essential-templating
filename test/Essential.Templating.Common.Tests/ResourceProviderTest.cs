using System.Globalization;
using System.IO;
using Essential.Templating.Common.Storage;
using Xunit;

namespace Essential.Templating.Common.Tests
{
    public class ResourceProviderTest
    {
        [Fact]
        public void FileSystemProvider_CanFindExistingResource()
        {
            var provider = new FileSystemResourceProvider("Templates");
            var resource = provider.Get("Template.tmpl");

            Assert.NotNull(resource);
            Assert.True(resource.Length > 0);
        }

        [Fact]
        public void ResxClassProvider_CanFindExistingResource()
        {
            var provider = ResxClassResourceProvider<ResxResource>.Create();
            var resource = provider.Get("ResourceTemplate");

            Assert.NotNull(resource);
            Assert.True(resource.Length > 0);
        }

        [Fact]
        public void EmbeddedResourceProvider_CanFindExistingResource()
        {
            var provider = new EmbeddedResourceProvider();
            var resource = provider.Get("Templates/EmbeddedResourceTemplate.tmpl");

            Assert.NotNull(resource);
            Assert.True(resource.Length > 0);
        }

        [Fact]
        public void FileSystemProvider_CanFindLocalizedResource()
        {
            var provider = new FileSystemResourceProvider("Templates");
            var resource = provider.Get("Template.tmpl", new CultureInfo("ru-RU"));

            Assert.NotNull(resource);
            using (var streamReader = new StreamReader(resource))
            {
                var templateString = streamReader.ReadToEnd();
                Assert.Contains( "русском", templateString);
            }
        }

        [Fact]
        public void FileSystemProvider_ThrowsResourceNotFoundException_WhenResourceIsNotFound()
        {
            var provider = new FileSystemResourceProvider("Templates");

            Assert.Throws<ResourceNotFoundException>(() => provider.Get("TemplateNotFound.tmpl", new CultureInfo("en")));
        }

        [Fact]
        public void ResxClassProvider_CanFindLocalizedResource()
        {
            var provider = ResxClassResourceProvider<ResxResource>.Create();
            var resource = provider.Get("ResourceTemplate", new CultureInfo("ru-RU"));

            Assert.NotNull(resource);
            using (var streamReader = new StreamReader(resource))
            {
                var templateString = streamReader.ReadToEnd();
                Assert.Contains("русском", templateString);
            }
        }

        [Fact]
        public void EmbeddedResourceProvider_CanFindLocalizedResource()
        {
            var provider = new EmbeddedResourceProvider();
            var resource = provider.Get("Templates/EmbeddedResourceTemplate.tmpl", new CultureInfo("ru-RU"));

            Assert.NotNull(resource);
            using (var streamReader = new StreamReader(resource))
            {
                var templateString = streamReader.ReadToEnd();
                Assert.Contains("русском", templateString);
            }
        }
    }
}