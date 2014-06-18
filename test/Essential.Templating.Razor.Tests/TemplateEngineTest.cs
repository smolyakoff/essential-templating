using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using Essential.Templating.Common;
using Essential.Templating.Common.Caching;
using Essential.Templating.Common.Storage;
using Essential.Templating.Razor.Configuration;
using Essential.Templating.Razor.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Essential.Templating.Razor.Tests
{
    [TestClass]
    [DeploymentItem("Templates", "Templates")]
    [DeploymentItem(@"ru\Essential.Templating.Razor.Tests.resources.dll", "ru")]
    public class TemplateEngineTest
    {
        private readonly ITemplateEngine _fileSystemTemplateEngine;

        private readonly ITemplateEngine _embeddedResourceTemplateEngine;

        public TemplateEngineTest()
        {
            var fileSystemConfig = new RazorTemplateEngineConfiguration
            {
                ResourceProvider = new FileSystemResourceProvider("Templates"),
                CachePolicy = CachePolicy.Instance
            };
            var embeddedResourceConfig = new RazorTemplateEngineConfiguration
            {
                ResourceProvider = new EmbeddedResourceProvider(
                    Assembly.GetExecutingAssembly(),
                    "Essential.Templating.Razor.Tests.Templates.Embedded"),
                CachePolicy = CachePolicy.Instance
            };
            _fileSystemTemplateEngine = new RazorTemplateEngine(fileSystemConfig);
            _embeddedResourceTemplateEngine = new RazorTemplateEngine(embeddedResourceConfig);
        }

        [TestMethod]
        public void RenderLocalizedTemplate_RendersInSpecifiedCulture()
        {
            var template = _fileSystemTemplateEngine.Render("Test.cshtml", null, new CultureInfo("ru-RU"));
            
            Assert.IsNotNull(template);
            Debug.WriteLine(template);

            Assert.IsTrue(!string.IsNullOrEmpty(template));
        }

        [TestMethod]
        public void RenderTemplateWithModel_RendersCorrectText()
        {
            var template = _fileSystemTemplateEngine.Render("Test.cshtml", "Model", null, CultureInfo.InvariantCulture);

            Assert.IsNotNull(template);
            Debug.WriteLine(template);

            Assert.IsTrue(template == "Rendered string: Model");
        }

        [TestMethod]
        public void RenderTemplateWithViewBag_RendersCorrectText()
        {
            var template = _fileSystemTemplateEngine.Render("ViewBag.cshtml", new {Hello = "Hello, World!"}, null);

            Assert.IsNotNull(template);
            Debug.WriteLine(template);

            Assert.IsTrue(template == "Hello, World!");
        }

        [TestMethod]
        public void RenderExposingTemplate_RendersCorrectTemplateStructure()
        {
            var templateStructure = _fileSystemTemplateEngine.Render("Exposing.cshtml", renderer: new TemplateStructureRenderer());

            Assert.IsNotNull(templateStructure);
            Debug.WriteLine(templateStructure.Body);

            Assert.IsTrue(templateStructure.StartCalled);
            Assert.IsTrue(templateStructure.EndCalled);
            Assert.IsTrue(templateStructure.Body.Length > 0);
            Assert.IsTrue(templateStructure.Sections.Contains("Section1"));
            Assert.IsTrue(templateStructure.Sections.Contains("Section2"));
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        public void TemplateEngine_OnEmptyPath_ThrowsArgumentException()
        {
            _fileSystemTemplateEngine.Render("");
        }

        [TestMethod]
        public void TemplateEngine_RenderAsync_Executes()
        {
            var template =
                _fileSystemTemplateEngine.RenderAsync("Test.cshtml", "Model", null, CultureInfo.InvariantCulture).Result;

            Assert.IsNotNull(template);
            Assert.IsTrue(template.Length > 0);
        }

        [TestMethod]
        public void TemplateEngine_RenderTemplateWithCommonLayout_Executes()
        {
            var templateInvariant =
                _embeddedResourceTemplateEngine.Render("WithLayout.cshtml", null, CultureInfo.InvariantCulture);
            var templateRu =
                _embeddedResourceTemplateEngine.Render("WithLayout.cshtml", null, new CultureInfo("ru-RU"));

            Assert.IsNotNull(templateInvariant);
            Assert.IsNotNull(templateRu);

            Assert.IsTrue(templateInvariant.Contains("Layout"));
            Assert.IsTrue(templateRu.Contains("Layout"));
            Assert.IsTrue(templateInvariant.Contains("English"));
            Assert.IsTrue(templateRu.Contains("русском"));
        }
    }
}