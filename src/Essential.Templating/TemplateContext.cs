using System.Globalization;
using Essential.Templating.Storage;

namespace Essential.Templating
{
    public class TemplateContext
    {
        private readonly CultureInfo _culture;
        private readonly string _path;

        private readonly IResourceProvider _resourceProvider;

        private readonly ITemplateTool _templateTool;

        internal TemplateContext(string path, CultureInfo culture, IResourceProvider resourceProvider,
            ITemplateTool templateTool)
        {
            _path = path;
            _culture = culture;
            _resourceProvider = resourceProvider;
            _templateTool = templateTool;
        }

        public string Path
        {
            get { return _path; }
        }

        public CultureInfo Culture
        {
            get { return _culture; }
        }

        public IResourceProvider ResourceProvider
        {
            get { return _resourceProvider; }
        }

        public Template Resolve(string path, CultureInfo culture)
        {
            return _templateTool.Resolve(path, culture);
        }

        public string Render(string path, CultureInfo culture, object model)
        {
            var template = _templateTool.Resolve(path, culture, model);
            return template == null ? null : _templateTool.RenderString(template);
        }
    }
}