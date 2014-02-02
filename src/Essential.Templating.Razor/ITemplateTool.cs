using System.Globalization;

namespace Essential.Templating.Razor
{
    internal interface ITemplateTool
    {
        Template Resolve(string path, CultureInfo culture, object model);

        Template Resolve(string path, CultureInfo culture);

        string RenderString(Template template);
    }
}