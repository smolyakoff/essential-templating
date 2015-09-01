using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Essential.Templating.Common.Storage;

namespace Essential.Templating.Razor
{
    public class TemplateContext
    {
        private readonly Dictionary<string, object> _environment;

        internal TemplateContext(string path, CultureInfo culture, IResourceProvider resourceProvider,
            ITemplateTool templateTool)
        {
            _environment = new Dictionary<string, object>
            {
                {Keys.Path, path},
                {Keys.Culture, culture},
                {Keys.ResourceProvider, resourceProvider},
                {Keys.TemplateTool, templateTool}
            };
        }

        private TemplateContext(Dictionary<string, object> environment)
        {
            if (environment == null)
            {
                throw new ArgumentNullException("environment");
            }

            _environment = environment;
        }

        public string Path
        {
            get { return (string) _environment[Keys.Path]; }
        }

        public CultureInfo Culture
        {
            get { return (CultureInfo) _environment[Keys.Culture]; }
        }

        public IResourceProvider ResourceProvider
        {
            get { return (IResourceProvider) _environment[Keys.ResourceProvider]; }
        }

        internal ITemplateTool TemplateTool
        {
            get { return (ITemplateTool) _environment[Keys.TemplateTool]; }
        }

        public Template Resolve(string path, CultureInfo culture, IDictionary<string, object> contextEnvironment,
            object model)
        {
            return TemplateTool.Resolve(path, culture, model, contextEnvironment);
        }

        public T AddOrGetExisting<T>(string key, Func<T> constructor)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Key cannot be null or empty.", "key");
            }

            if (_environment.ContainsKey(key))
            {
                return (T) _environment[key];
            }

            var value = constructor();
            _environment[key] = value;
            return value;
        }

        public void Put(string key, object value)
        {
            _environment[key] = value;
        }

        public string RenderString(string path, CultureInfo culture, object model)
        {
            var template = TemplateTool.Resolve(path, culture, model, new Dictionary<string, object>());
            return template == null ? null : TemplateTool.RenderString(template);
        }

        internal TemplateContext Derive(string path)
        {
            var environment = _environment.ToDictionary(x => x.Key, x => x.Value);
            environment[Keys.Path] = path;
            var context = new TemplateContext(environment);
            return context;
        }

        private static class Keys
        {
            public const string Path = "Base.Path";

            public const string Culture = "Base.Culture";

            public const string ResourceProvider = "Base.ResourceProvider";

            public const string TemplateTool = "Base.TemplateTool";
        }
    }
}