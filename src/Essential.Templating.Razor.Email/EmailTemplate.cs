using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.Contracts;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using Essential.Templating.Razor.Email.Helpers;
using RazorEngine.Templating;

namespace Essential.Templating.Razor.Email
{
    public class EmailTemplate : ExposingTemplate
    {
        private const string EmailContextKey = "Email.Context";

        private readonly EmailContext _emailContext;

        public EmailTemplate(TemplateContext templateContext) : base(templateContext)
        {
            Contract.Requires<ArgumentNullException>(templateContext != null);
            _emailContext = templateContext.AddOrGetExisting(EmailContextKey, () => new EmailContext());

            Resource = new ResourceTemplateHelper(this, templateContext);
        }

        public ICollection<Attachment> Attachments
        {
            get { return _emailContext.Attachments; }
        }

        public MailAddressCollection Bcc
        {
            get { return _emailContext.Bcc; }
        }

        public Encoding BodyEncoding
        {
            get { return _emailContext.BodyEncoding; }
            set { _emailContext.BodyEncoding = value; }
        }

        public TransferEncoding BodyTransferEncoding
        {
            get { return _emailContext.BodyTransferEncoding; }
            set { _emailContext.BodyTransferEncoding = value; }
        }

        public MailAddressCollection CC
        {
            get { return _emailContext.CC; }
        }

        public DeliveryNotificationOptions DeliveryNotificationOptions
        {
            get { return _emailContext.DeliveryNotificationOptions; }
            set { _emailContext.DeliveryNotificationOptions = value; }
        }

        public MailAddress From
        {
            get { return _emailContext.From; }
            set { _emailContext.From = value; }
        }

        public NameValueCollection Headers
        {
            get { return _emailContext.Headers; }
        }

        public Encoding HeadersEncoding
        {
            get { return _emailContext.HeadersEncoding; }
            set { _emailContext.HeadersEncoding = value; }
        }

        public ICollection<LinkedResource> LinkedResources
        {
            get { return _emailContext.LinkedResources; }
        }

        public MailPriority Priority
        {
            get { return _emailContext.Priority; }
            set { _emailContext.Priority = value; }
        }

        public MailAddressCollection ReplyToList
        {
            get { return _emailContext.ReplyToList; }
        }

        public MailAddress Sender
        {
            get { return _emailContext.Sender; }
            set { _emailContext.Sender = value; }
        }

        public string Subject
        {
            get { return _emailContext.Subject; }
            set { _emailContext.Subject = value; }
        }

        public Encoding SubjectEncoding
        {
            get { return _emailContext.SubjectEncoding; }
            set { _emailContext.SubjectEncoding = value; }
        }

        public MailAddressCollection To
        {
            get { return _emailContext.To; }
        }

        public ResourceTemplateHelper Resource { get; private set; }

        protected override ITemplate ResolveLayout(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return new EmailTemplateLayout(DeriveContext(this.GetType().FullName));
            }
            var contextEnvironment = new Dictionary<string, object>()
            {
                {EmailContextKey, _emailContext}
            };
            return base.ResolveLayout(name, contextEnvironment);
        }
    }
}