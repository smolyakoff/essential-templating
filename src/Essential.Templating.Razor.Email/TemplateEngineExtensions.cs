using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Net.Mail;
using System.Threading.Tasks;
using Essential.Templating.Common;
using Essential.Templating.Razor.Email.Rendering;

namespace Essential.Templating.Razor.Email
{
    public static class TemplateEngineExtensions
    {
        public static MailMessage RenderEmail(this ITemplateEngine templateEngine, string path, object viewBag = null,
            CultureInfo culture = null)
        {
            Contract.Requires<ArgumentNullException>(templateEngine != null);
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(path));
            Contract.Requires<NotSupportedException>(templateEngine.GetType() == typeof(RazorTemplateEngine), "Only razor engine is supported.");

            return templateEngine.Render(path, renderer: new EmailRenderer(), viewBag: viewBag, culture: culture);
        }

        public static MailMessage RenderEmail<T>(this ITemplateEngine templateEngine, string path, T model,
            object viewBag = null,
            CultureInfo culture = null)
        {
            Contract.Requires<ArgumentNullException>(templateEngine != null);
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(path));
            Contract.Requires<NotSupportedException>(templateEngine.GetType() == typeof(RazorTemplateEngine), "Only razor engine is supported.");

            return templateEngine.Render(path, renderer: new EmailRenderer(), model: model, viewBag: viewBag, culture: culture);
        }

        public static Task<MailMessage> RenderEmailAsync(this ITemplateEngine templateEngine, string path, object viewBag = null,
            CultureInfo culture = null)
        {
            Contract.Requires<ArgumentNullException>(templateEngine != null);
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(path));
            Contract.Requires<NotSupportedException>(templateEngine.GetType() == typeof(RazorTemplateEngine), "Only razor engine is supported.");

            return templateEngine.RenderAsync(path, renderer: new EmailRenderer(), viewBag: viewBag, culture: culture);
        }

        public static Task<MailMessage> RenderEmailAsync<T>(this ITemplateEngine templateEngine, string path, T model,
            object viewBag = null,
            CultureInfo culture = null)
        {
            Contract.Requires<ArgumentNullException>(templateEngine != null);
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(path));
            Contract.Requires<NotSupportedException>(templateEngine.GetType() == typeof(RazorTemplateEngine), "Only razor engine is supported.");

            return templateEngine.RenderAsync(path, renderer: new EmailRenderer(), model: model, viewBag: viewBag, culture: culture);
        }
    }
}
