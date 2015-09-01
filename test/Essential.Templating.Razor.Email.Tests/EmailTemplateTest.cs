using Essential.Templating.Common;
using Essential.Templating.Common.Configuration;
using Essential.Templating.Razor.Configuration;
using Xunit;
using Xunit.Abstractions;

namespace Essential.Templating.Razor.Email.Tests
{
    public class EmailTemplateTest
    {
        private readonly ITestOutputHelper _output;
        private readonly ITemplateEngine _templateEngine;

        public EmailTemplateTest(ITestOutputHelper output)
        {
            _output = output;
            _templateEngine = new RazorTemplateEngineBuilder()
                .FindTemplatesInDirectory("Templates")
                .UseInstanceCache()
                .Build();
        }

        [Fact]
        public void RenderSimpleEmail()
        {
            var email = _templateEngine.RenderEmail("SimpleEmail.cshtml");

            Assert.NotNull(email);
            Assert.False(email.IsBodyHtml);
            Assert.Contains("Plain text", email.Body);
            Assert.Contains("HTML", email.AlternateViews[0].ContentStream.AsString());
            Assert.True(email.Subject == "Simple Email");
            Assert.True(email.From.Address == "test@email.com");
        }

        [Fact]
        public void RenderSimpleEmailWithNoSections()
        {
            var email = _templateEngine.RenderEmail("SimpleEmailNoSections.cshtml");

            Assert.NotNull(email);
            Assert.False(email.IsBodyHtml);
            Assert.Contains("Plain text", email.Body);
        }

        [Fact]
        public void RenderEmailWithModelAndLayout()
        {
            var car = new Car {Make = "Ford", Model = "Mustang"};
            var email = _templateEngine.RenderEmailAsync("CarMail.cshtml", car).Result;

            Assert.True(email != null);
            var body = email.AlternateViews[0].ContentStream.AsString();
            _output.WriteLine(body);
            Assert.Contains("Car Email", body);
            Assert.Contains("Ford", body);
            Assert.Contains("Mustang", body);
        }

        [Fact]
        public void RenderEmailWithLinkedResource()
        {
            var email = _templateEngine.RenderEmail("ImageEmail.cshtml");
            Assert.NotNull(email);
            var body = email.AlternateViews[0].ContentStream.AsString();
            _output.WriteLine(body);
            Assert.NotEmpty(email.AlternateViews[0].LinkedResources);
        }

        [Fact]
        public void RenderEmailWithLinkedResourceInLayout()
        {
            var email = _templateEngine.RenderEmail("ImageEmailWithLayout.cshtml");
            Assert.NotNull(email);
            var body = email.AlternateViews[0].ContentStream.AsString();
            _output.WriteLine(body);
            Assert.NotEmpty(email.AlternateViews[0].LinkedResources);
        }
    }
}