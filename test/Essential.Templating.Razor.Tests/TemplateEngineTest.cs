using System;
using System.Globalization;
using System.Reflection;
using Essential.Templating.Common;
using Essential.Templating.Common.Caching;
using Essential.Templating.Common.Storage;
using Essential.Templating.Razor.Configuration;
using Essential.Templating.Razor.Tests.Helpers;
using Xunit;
using Xunit.Abstractions;

namespace Essential.Templating.Razor.Tests
{
    public class TemplateEngineTest
    {
        private readonly ITestOutputHelper _output;
        private readonly ITemplateEngine _embeddedResourceTemplateEngine;
        private readonly ITemplateEngine _fileSystemTemplateEngine;

        public TemplateEngineTest(ITestOutputHelper output)
        {
            _output = output;
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

        [Fact]
        public void RenderLocalizedTemplate_RendersInSpecifiedCulture()
        {
            var template = _fileSystemTemplateEngine.Render("Test.cshtml", null, new CultureInfo("ru-RU"));

            Assert.NotNull(template);
            _output.WriteLine(template);

            Assert.True(!string.IsNullOrEmpty(template));
        }

        [Fact]
        public void RenderTemplateWithModel_RendersCorrectText()
        {
            var template = _fileSystemTemplateEngine.Render("Test.cshtml", "Model", null, CultureInfo.InvariantCulture);

            Assert.NotNull(template);
            _output.WriteLine(template);

            Assert.True(template == "Rendered string: Model");
        }

        [Fact]
        public void RenderTemplateWithViewBag_RendersCorrectText()
        {
            var template = _fileSystemTemplateEngine.Render("ViewBag.cshtml", new {Hello = "Hello, World!"}, null);

            Assert.NotNull(template);
            _output.WriteLine(template);

            Assert.True(template == "Hello, World!");
        }

        [Fact]
        public void RenderExposingTemplate_RendersCorrectTemplateStructure()
        {
            var templateStructure =
                _fileSystemTemplateEngine.Render("Exposing.cshtml", renderer: new TemplateStructureRenderer());

            Assert.NotNull(templateStructure);
            _output.WriteLine(templateStructure.Body);

            Assert.True(templateStructure.StartCalled);
            Assert.True(templateStructure.EndCalled);
            Assert.True(templateStructure.Body.Length > 0);
            Assert.True(templateStructure.Sections.Contains("Section1"));
            Assert.True(templateStructure.Sections.Contains("Section2"));
        }

        [Fact]
        public void TemplateEngine_OnEmptyPath_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _fileSystemTemplateEngine.Render(""));
        }

        [Fact]
        public void TemplateEngine_RenderAsync_Executes()
        {
            var template =
                _fileSystemTemplateEngine.RenderAsync("Test.cshtml", "Model", null, CultureInfo.InvariantCulture)
                    .Result;

            Assert.NotNull(template);
            Assert.True(template.Length > 0);
        }

        [Fact]
        public void TemplateEngine_RenderTemplateWithCommonLayout_Executes()
        {
            var templateInvariant =
                _embeddedResourceTemplateEngine.Render("WithLayout.cshtml", null, CultureInfo.InvariantCulture);
            var templateRu =
                _embeddedResourceTemplateEngine.Render("WithLayout.cshtml", null, new CultureInfo("ru-RU"));

            Assert.NotNull(templateInvariant);
            Assert.NotNull(templateRu);

            Assert.Contains("Layout", templateInvariant);
            Assert.Contains("Layout", templateRu);
            Assert.Contains("English", templateInvariant);
            Assert.Contains("русском", templateRu);
        }

        [Fact]
        public void RenderTemplateWithModel_PassesModelToLayout_AndRendersCorrectText()
        {
            var model = new Person
            {
                FirstName = "John",
                LastName = "Doe",
                Age = 36
            };
            var result = _fileSystemTemplateEngine.Render("Hello.cshtml", viewBag: null, model: model);
            _output.WriteLine(result);

            Assert.Contains("Hello, John Doe!", result);
            Assert.Contains("The person's age is 36", result);
        }
    }
}