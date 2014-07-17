using System.Globalization;
using System.IO;
using Essential.Templating.Common.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Essential.Templating.Common.Tests
{
    [TestClass]
    [DeploymentItem("Templates", "Templates")]
    [DeploymentItem(@"ru\Essential.Templating.Common.Tests.resources.dll", "ru")]
    public class ResourceProviderTest
    {
        [TestMethod]
        public void FileSystemProvider_CanFindExistingResource()
        {
            var provider = new FileSystemResourceProvider("Templates");
            var resource = provider.Get("Template.tmpl");

            Assert.IsNotNull(resource);
            Assert.IsTrue(resource.Length > 0);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void FileSystemProvider_CanNotFindResource()
        {
            var provider = new FileSystemResourceProvider("Templates");
            provider.Get("NotFoundFileName.tmpl");           
        }

        [TestMethod]
        public void ResxClassProvider_CanFindExistingResource()
        {
            var provider = ResxClassResourceProvider<ResxResource>.Create();
            var resource = provider.Get("ResourceTemplate");

            Assert.IsNotNull(resource);
            Assert.IsTrue(resource.Length > 0);
        }

        [TestMethod]
        public void EmbeddedResourceProvider_CanFindExistingResource()
        {
            var provider = new EmbeddedResourceProvider();
            var resource = provider.Get("Templates/EmbeddedResourceTemplate.tmpl");

            Assert.IsNotNull(resource);
            Assert.IsTrue(resource.Length > 0);
        }

        [TestMethod]
        public void FileSystemProvider_CanFindLocalizedResource()
        {
            var provider = new FileSystemResourceProvider("Templates");
            var resource = provider.Get("Template.tmpl", new CultureInfo("ru-RU"));

            Assert.IsNotNull(resource);
            using (var streamReader = new StreamReader(resource))
            {
                var templateString = streamReader.ReadToEnd();
                Assert.IsTrue(templateString.Contains("русском"));
            }
        }

        [TestMethod]
        public void ResxClassProvider_CanFindLocalizedResource()
        {
            var provider = ResxClassResourceProvider<ResxResource>.Create();
            var resource = provider.Get("ResourceTemplate", new CultureInfo("ru-RU"));

            Assert.IsNotNull(resource);
            using (var streamReader = new StreamReader(resource))
            {
                var templateString = streamReader.ReadToEnd();
                Assert.IsTrue(templateString.Contains("русском"));
            }
        }

        [TestMethod]
        public void EmbeddedResourceProvider_CanFindLocalizedResource()
        {
            var provider = new EmbeddedResourceProvider();
            var resource = provider.Get("Templates/EmbeddedResourceTemplate.tmpl", new CultureInfo("ru-RU"));

            Assert.IsNotNull(resource);
            using (var streamReader = new StreamReader(resource))
            {
                var templateString = streamReader.ReadToEnd();
                Assert.IsTrue(templateString.Contains("русском"));
            }
        }
    }
}