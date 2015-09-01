using System;
using System.Collections.Generic;
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
            if (templateContext == null)
            {
                throw new ArgumentNullException("templateContext");
            }

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

        public TemplateWriter Include(string cacheName, object model = null)
        {
            var partial = _templateContext.RenderString(cacheName, Culture, model);
            return partial == null
                ? new TemplateWriter(w => { })
                : new TemplateWriter(w => w.Write(partial));
        }

        protected override ITemplate ResolveLayout(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            var layout = _templateContext.Resolve(name, Culture, new Dictionary<string, object>(), GetModel());
            if (layout == null)
            {
                throw new InvalidOperationException("Layout template was not found.");
            }

            return layout;
        }

        protected ITemplate ResolveLayout(string name, Dictionary<string, object> contextEnvironment)
        {
            if (contextEnvironment == null)
            {
                throw new ArgumentNullException("contextEnvironment");
            }

            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            var layout = _templateContext.Resolve(name, Culture, contextEnvironment, GetModel());
            if (layout == null)
            {
                throw new InvalidOperationException("Layout template was not found.");
            }

            return layout;
        }

        protected virtual object GetModel()
        {
            return null;
        }

        protected Stream GetResource(string uri, CultureInfo culture = null)
        {
            if (string.IsNullOrEmpty(uri))
            {
                throw new ArgumentException("Uri cannot be null or empty.", "uri");
            }

            return _templateContext.ResourceProvider.Get(uri, culture);
        }

        protected TemplateContext DeriveContext(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Path cannot be null or empty.", "path");
            }

            return _templateContext.Derive(path);
        }
    }
}