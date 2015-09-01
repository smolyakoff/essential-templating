using System.Net.Mail;
using System.Threading;
using Essential.Templating.Common.Rendering;

namespace Essential.Templating.Razor.Email.Rendering
{
    public class EmailRenderer : IRenderer<EmailTemplate, MailMessage>
    {
        public MailMessage Render(EmailTemplate template, object viewBag)
        {
            var previousCulture = Thread.CurrentThread.CurrentCulture;
            var previousUICulture = Thread.CurrentThread.CurrentUICulture;
            Thread.CurrentThread.CurrentCulture = template.Culture;
            Thread.CurrentThread.CurrentUICulture = template.Culture;
            try
            {
                var message = template.Render(viewBag);
                return message;
            }
            finally
            {
                Thread.CurrentThread.CurrentCulture = previousCulture;
                Thread.CurrentThread.CurrentUICulture = previousUICulture;
            }
        }
    }
}