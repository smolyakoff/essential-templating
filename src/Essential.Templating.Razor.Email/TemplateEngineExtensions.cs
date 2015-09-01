using System;
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
            if (templateEngine == null)
            {
                throw new ArgumentNullException("templateEngine");
            }

            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Path cannot be null or empty.", "path");
            }

            if (templateEngine.GetType() != typeof(RazorTemplateEngine))
            {
                throw new NotSupportedException("Only razor engine is supported.");
            }

            return templateEngine.Render(path, renderer: new EmailRenderer(), viewBag: viewBag, culture: culture);
        }

        public static MailMessage RenderEmail<T>(this ITemplateEngine templateEngine, string path, T model,
            object viewBag = null,
            CultureInfo culture = null)
        {
            if (templateEngine == null)
            {
                throw new ArgumentNullException("templateEngine");
            }

            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Path cannot be null or empty.", "path");
            }

            if (templateEngine.GetType() != typeof(RazorTemplateEngine))
            {
                throw new NotSupportedException("Only razor engine is supported.");
            }

            return templateEngine.Render(path, new EmailRenderer(), model, viewBag, culture);
        }

        public static Task<MailMessage> RenderEmailAsync(this ITemplateEngine templateEngine, string path,
            object viewBag = null,
            CultureInfo culture = null)
        {
            if (templateEngine == null)
            {
                throw new ArgumentNullException("templateEngine");
            }

            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Path cannot be null or empty.", "path");
            }

            if (templateEngine.GetType() != typeof(RazorTemplateEngine))
            {
                throw new NotSupportedException("Only razor engine is supported.");
            }

            return templateEngine.RenderAsync(path, renderer: new EmailRenderer(), viewBag: viewBag, culture: culture);
        }

        public static Task<MailMessage> RenderEmailAsync<T>(this ITemplateEngine templateEngine, string path, T model,
            object viewBag = null,
            CultureInfo culture = null)
        {
            if (templateEngine == null)
            {
                throw new ArgumentNullException("templateEngine");
            }

            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Path cannot be null or empty.", "path");
            }

            if (templateEngine.GetType() != typeof(RazorTemplateEngine))
            {
                throw new NotSupportedException("Only razor engine is supported.");
            }

            return templateEngine.RenderAsync(path, new EmailRenderer(), model, viewBag, culture);
        }
    }
}