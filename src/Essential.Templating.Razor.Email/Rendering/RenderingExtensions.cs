using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace Essential.Templating.Razor.Email.Rendering
{
    internal static class RenderingExtensions
    {
        public static MailMessage Render(this EmailTemplate template, object viewBag)
        {
            if (template == null)
            {
                throw new ArgumentNullException("template");
            }

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
            if (template.LinkedResources.Count > 0 && message.AlternateViews.Count > 0)
            {
                message.AlternateViews[0].LinkedResources.CopyFrom(template.LinkedResources);
            }

            return message;
        }

        internal static void CopyTo<T>(this IEnumerable<T> source, ICollection<T> destination)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }

            foreach (var item in source) destination.Add(item);
        }

        internal static void CopyFrom<T>(this ICollection<T> destination, IEnumerable<T> source)
        {
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }

            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            source.CopyTo(destination);
        }
    }
}