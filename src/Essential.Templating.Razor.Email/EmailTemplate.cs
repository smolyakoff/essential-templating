using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics.Contracts;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using RazorEngine.Templating;

namespace Essential.Templating.Razor.Email
{
    public class EmailTemplate : ExposingTemplate
    {
        public EmailTemplate(TemplateContext templateContext) : base(templateContext)
        {
            Contract.Requires<ArgumentNullException>(templateContext != null);

            Attachments = new Collection<Attachment>();
            Bcc = new MailAddressCollection();
            BodyEncoding = Encoding.UTF8;
            BodyTransferEncoding = TransferEncoding.Base64;
            CC = new MailAddressCollection();
            DeliveryNotificationOptions = DeliveryNotificationOptions.None;
            Headers = new NameValueCollection();
            HeadersEncoding = Encoding.UTF8;
            Priority = MailPriority.Normal;
            ReplyToList = new MailAddressCollection();
            To = new MailAddressCollection();
            SubjectEncoding = Encoding.UTF8;
        }

        public ICollection<Attachment> Attachments { get; private set; }

        public MailAddressCollection Bcc { get; private set; }

        public Encoding BodyEncoding { get; set; }

        public TransferEncoding BodyTransferEncoding { get; set; }

        public MailAddressCollection CC { get; private set; }

        public DeliveryNotificationOptions DeliveryNotificationOptions { get; set; }

        public MailAddress From { get; set; }

        public NameValueCollection Headers { get; private set; }

        public Encoding HeadersEncoding { get; set; }

        public MailPriority Priority { get; set; }

        public MailAddressCollection ReplyToList { get; private set; }

        public MailAddress Sender { get; set; }

        public string Subject { get; set; }

        public Encoding SubjectEncoding { get; set; }

        public MailAddressCollection To { get; private set; }

        protected override ITemplate ResolveLayout(string name)
        {
            return string.IsNullOrEmpty(Layout) 
                ? new EmailTemplateLayout(DeriveContext(this.GetType().FullName)) 
                : base.ResolveLayout(name);
        }
    }
}