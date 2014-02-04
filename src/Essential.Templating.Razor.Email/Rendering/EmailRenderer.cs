using System;
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
            Thread.CurrentThread.CurrentCulture = template.Culture;
            var message = template.Render(viewBag);
            Thread.CurrentThread.CurrentCulture = previousCulture;
            return message;
        }
    }
}
