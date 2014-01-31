using System.Globalization;
using System.IO;
using Essential.Templating.Storage;
using Essential.Templating.Tests.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Essential.Templating.Tests
{
    [TestClass]
    [DeploymentItem("Templates", "Templates")]
    [DeploymentItem(@"ru\Essential.Templating.Tests.resources.dll", "ru")]
    public class ResourceProviderTest
    {
        [TestMethod]
        public void FileSystemProvider_CanFindExistingResource()
        {
            var provider = new FileSystemResourceProvider("Templates");
            var resource = provider.Get("Test.cshtml");

            Assert.IsNotNull(resource);
            Assert.IsTrue(resource.Length > 0);
        }

        [TestMethod]
        public void ResxClassProvider_CanFindExistingResource()
        {
            var provider = ResxClassResourceProvider<ResxResource>.Create();
            var resource = provider.Get("ResourceTemplate");

            Assert.IsNotNull(resource);
            Assert.IsNotNull(resource.Length > 0);
        }

        [TestMethod]
        public void FileSystemProvider_CanFindLocalizedResource()
        {
            var provider = new FileSystemResourceProvider("Templates");
            var resource = provider.Get("Test.cshtml", new CultureInfo("ru-RU"));

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
    }
}