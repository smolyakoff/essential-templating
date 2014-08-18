using System.Collections.Generic;
using System.Globalization;

namespace Essential.Templating.Razor
{
    internal interface ITemplateTool
    {
        Template Resolve(string path, CultureInfo culture, object model);

        Template Resolve(string path, CultureInfo culture);

        Template Resolve(string path, CultureInfo culture, IDictionary<string, object> contextEnvironment);

        Template Resolve(string path, CultureInfo culture, object model, IDictionary<string, object> contextEnvironment);

        string RenderString(Template template);
    }
}