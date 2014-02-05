using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Net.Mail;

namespace Essential.Templating.Razor.Email.Rendering
{
    internal static class RenderingExtensions
    {
        public static MailMessage Render(this EmailTemplate template, object viewBag)
        {
            Contract.Requires(template != null);

            var visitor = new EmailTemplateVisitor();
            var exposingTemplate = (IExposingTemplate) template;
            exposingTemplate.Run(visitor, viewBag);

            var message = new MailMessage();
            message.Attachments.CopyFrom(template.Attachments);
            message.Bcc.CopyFrom(template.Bcc);
            message.BodyEncoding = template.BodyEncoding;
            message.BodyTransferEncoding = template.BodyTransferEncoding;
            message.CC.CopyFrom(template.CC);
            message.DeliveryNotificationOptions = template.DeliveryNotificationOptions;
            if (template.From != null)
            {
                message.From = template.From;
            }
            message.Headers.Add(template.Headers);
            message.HeadersEncoding = template.HeadersEncoding;
            message.Priority = template.Priority;
            message.ReplyToList.CopyFrom(template.ReplyToList);
            message.Sender = template.Sender;
            message.Subject = template.Subject;
            message.SubjectEncoding = template.SubjectEncoding;
            message.To.CopyFrom(template.To);
            visitor.CopyTo(message);
            return message;
        }

        private static void CopyTo<T>(this IEnumerable<T> source, ICollection<T> destination)
        {
            Contract.Requires(source != null);
            Contract.Requires(destination != null);

            foreach (var item in source)
            {
                destination.Add(item);
            }
        }

        private static void CopyFrom<T>(this ICollection<T> destination, IEnumerable<T> source)
        {
            Contract.Requires(source != null);
            Contract.Requires(destination != null);

            source.CopyTo(destination);
        }
    }
}
