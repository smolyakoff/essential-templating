using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Net.Mail;
using System.Net.Mime;
using RazorEngine.Text;

namespace Essential.Templating.Razor.Email.Helpers
{
    public static class ResourceTemplateHelperExtensions
    {
        public static IEncodedString Link(this ResourceTemplateHelper helper, string path, string contentId, string mediaType,
            TransferEncoding transferEncoding, CultureInfo culture = null)
        {
            Contract.Requires<ArgumentNullException>(helper != null);
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(path));
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(contentId));
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(mediaType));

            var resource = helper.Get(path, culture);
            if (resource == null)
            {
                var message = string.Format("Resource [{0}] was not found.", contentId);
                throw new TemplateHelperException(message);
            }
            var linkedResource = new LinkedResource(resource, mediaType)
            {
                TransferEncoding = transferEncoding,
                ContentId = contentId
            };
            helper.AddLinkedResource(linkedResource);
            var renderedResult = new RawString(string.Format("cid:{0}", contentId));
            return renderedResult;
        }
    }
}
