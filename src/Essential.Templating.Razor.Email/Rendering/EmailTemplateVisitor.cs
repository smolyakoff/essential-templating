using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Net.Mail;

namespace Essential.Templating.Razor.Email.Rendering
{
    internal class EmailTemplateVisitor : ITemplateVisitor
    {
        private string _body;

        private string _html;

        private string _text;

        private readonly ICollection<LinkedResource> _linkedResources = new Collection<LinkedResource>();

        public void Body(string text)
        {
            _body = text;
        }

        public void Section(string name, string text)
        {
            switch (name)
            {
                case Conventions.HtmlSectionName:
                    _html = text;
                    break;
                case Conventions.TextSectionName:
                    _text = text;
                    break;
            }
        }

        public void Start(Template template)
        {
            //do nothing
        }

        public void End(Template template)
        {
            //do nothing
        }

        public void CopyTo(MailMessage mailMessage)
        {
            Contract.Requires(mailMessage != null);
            if (string.IsNullOrEmpty(_html) && string.IsNullOrEmpty(_text))
            {
                mailMessage.IsBodyHtml = false;
                mailMessage.Body = _body;
            } 
            else if (!string.IsNullOrEmpty(_html) && string.IsNullOrEmpty(_text))
            {
                mailMessage.IsBodyHtml = false;
                var htmlView = AlternateView.CreateAlternateViewFromString(_html, mailMessage.BodyEncoding, "text/html");
                htmlView.LinkedResources.CopyFrom(_linkedResources);
                mailMessage.AlternateViews.Add(htmlView);
            }
            else if (string.IsNullOrEmpty(_html) && !string.IsNullOrEmpty(_text))
            {
                mailMessage.IsBodyHtml = false;
                mailMessage.Body = _text;
            }
            else
            {
                mailMessage.IsBodyHtml = false;
                mailMessage.Body = _text;
                var htmlView = AlternateView.CreateAlternateViewFromString(_html, mailMessage.BodyEncoding, "text/html");
                htmlView.LinkedResources.CopyFrom(_linkedResources);
                mailMessage.AlternateViews.Add(htmlView);
            }
        }
    }
}
