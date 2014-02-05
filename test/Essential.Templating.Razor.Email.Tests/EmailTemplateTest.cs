using System.Diagnostics;
using System.IO;
using System.Text;
using Essential.Templating.Common;
using Essential.Templating.Common.Configuration;
using Essential.Templating.Razor.Configuration;
using Essential.Templating.Razor.Email.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Essential.Templating.Razor.Email.Tests
{
    [TestClass]
    [DeploymentItem("Templates", "Templates")]
    public class EmailTemplateTest
    {
        private readonly ITemplateEngine _templateEngine;

        public EmailTemplateTest()
        {
            _templateEngine = new RazorTemplateEngineBuilder()
                .FindTemplatesInDirectory("Templates")
                .UseInstanceCache()
                .Build();            
        }

        [TestMethod]
        public void RenderSimpleEmail()
        {
            var email = _templateEngine.RenderEmail("SimpleEmail.cshtml");

            Assert.IsNotNull(email);
            Assert.IsFalse(email.IsBodyHtml);
            Assert.IsTrue(email.Body.Contains("Plain text"));
            Assert.IsTrue(email.AlternateViews[0].ContentStream.AsString().Contains("HTML"));
            Assert.IsTrue(email.Subject == "Simple Email");
            Assert.IsTrue(email.From.Address == "test@email.com");
        }

        [TestMethod]
        public void RenderEmailWithModelAndLayout()
        {
            var car = new Car {Make = "Ford", Model = "Mustang"};
            var email = _templateEngine.RenderEmailAsync("CarMail.cshtml", model: car).Result;

            Assert.IsTrue(email != null);
            Debug.WriteLine(email.Body);
            Assert.IsTrue(email.Body.Contains("Car Email"));
            Assert.IsTrue(email.Body.Contains("Ford"));
            Assert.IsTrue(email.Body.Contains("Mustang"));
        }
    }
}
