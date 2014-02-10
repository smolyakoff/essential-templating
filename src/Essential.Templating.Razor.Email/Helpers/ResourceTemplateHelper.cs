using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.IO;
using System.Net.Mail;

namespace Essential.Templating.Razor.Email.Helpers
{
    public class ResourceTemplateHelper
    {
        private readonly EmailTemplate _emailTemplate;

        private readonly TemplateContext _context;

        internal ResourceTemplateHelper(EmailTemplate emailTemplate, TemplateContext context)
        {
            Contract.Requires(emailTemplate != null);
            Contract.Requires(context != null);
            
            _emailTemplate = emailTemplate;
            _context = context;
        }

        public Stream Get(string path, CultureInfo culture = null)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(path));
            culture = culture ?? _context.Culture;

            return _context.ResourceProvider.Get(path, culture);
        }

        public void AddLinkedResource(LinkedResource linkedResource)
        {
            Contract.Requires<ArgumentNullException>(linkedResource != null);
            _emailTemplate.LinkedResources.Add(linkedResource);
        }

        public void AddAttachment(Attachment attachment)
        {
            Contract.Requires<ArgumentNullException>(attachment != null);
            _emailTemplate.Attachments.Add(attachment);
        }
    }
}
