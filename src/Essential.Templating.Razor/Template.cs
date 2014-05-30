using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.IO;
using RazorEngine.Templating;
using RazorEngine.Text;

namespace Essential.Templating.Razor
{
    public class Template : TemplateBase
    {
        private readonly TemplateContext _templateContext;

        public Template(TemplateContext templateContext)
        {
            Contract.Requires<ArgumentNullException>(templateContext != null);

            _templateContext = templateContext;
        }

        public string Name
        {
            get { return _templateContext.Path; }
        }

        public CultureInfo Culture
        {
            get { return _templateContext.Culture; }
        }

        public override void WriteTo(TextWriter writer, object value)
        {
            if (value == null)
            {
                return;
            }
            var encodedString = value as IEncodedString;
            if (encodedString != null)
            {
                writer.Write(encodedString);
            }
            else
            {
                var factory = new HtmlEncodedStringFactory();
                writer.Write(factory.CreateEncodedString(value));
            }
        }

        public override TemplateWriter Include(string cacheName, object model = null)
        {
            var partial = _templateContext.Render(cacheName, Culture, model);
            return partial == null
                ? new TemplateWriter(w => { })
                : new TemplateWriter(w => w.Write(partial));
        }

        protected override ITemplate ResolveLayout(string name)
        {
            return string.IsNullOrEmpty(name)
                ? null
                : _templateContext.Resolve(name, Culture);
        }

        protected Stream GetResource(string uri, CultureInfo culture = null)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(uri));

            return _templateContext.ResourceProvider.Get(uri, culture);
        }

        protected TemplateContext DeriveContext(string path)
        {
            Contract.Requires(!string.IsNullOrEmpty(path));
            return new TemplateContext(path, _templateContext.Culture, _templateContext.ResourceProvider, _templateContext.TemplateTool);
        }
    }
}