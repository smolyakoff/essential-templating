using System;
using Essential.Templating.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Essential.Templating.Tests
{
    [TestClass]
    public class ConfigurationTest
    {
        [TestMethod]
        public void TemplateEngineBuilder_HowThatFeels()
        {
            var engine = new TemplateEngineBuilder()
                .FindTemplatesInDirectory("Templates")
                .CacheExpiresIn(TimeSpan.FromSeconds(20))
                .UseSharedCache()
                .Build();

            Assert.IsNotNull(engine);
        }
    }
}