using System.Collections.Generic;
using System.Globalization;

namespace Essential.Templating.Razor
{
    internal interface ITemplateTool
    {
        Template Resolve(string path, CultureInfo culture, object model,
            IDictionary<string, object> contextEnvironment);

        string RenderString(Template template);
    }
}