using System;
using System.Globalization;
using System.IO;
using System.Net.Mail;

namespace Essential.Templating.Razor.Email.Helpers
{
    public class ResourceTemplateHelper
    {
        private readonly TemplateContext _context;
        private readonly EmailTemplate _emailTemplate;

        internal ResourceTemplateHelper(EmailTemplate emailTemplate, TemplateContext context)
        {
            if (emailTemplate == null)
            {
                throw new ArgumentNullException("emailTemplate");
            }

            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            _emailTemplate = emailTemplate;
            _context = context;
        }

        public Stream Get(string path, CultureInfo culture = null)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Path cannot be null or empty.", "path");
            }

            culture = culture ?? _context.Culture;

            return _context.ResourceProvider.Get(path, culture);
        }

        public void AddLinkedResource(LinkedResource linkedResource)
        {
            if (linkedResource == null)
            {
                throw new ArgumentNullException("linkedResource");
            }

            _emailTemplate.LinkedResources.Add(linkedResource);
        }

        public void AddAttachment(Attachment attachment)
        {
            if (attachment == null)
            {
                throw new ArgumentNullException("attachment");
            }

            _emailTemplate.Attachments.Add(attachment);
        }
    }
}