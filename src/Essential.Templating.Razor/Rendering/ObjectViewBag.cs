using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using RazorEngine.Templating;

namespace Essential.Templating.Razor.Rendering
{
    public class ObjectViewBag : DynamicViewBag
    {
        public ObjectViewBag(object viewBag)
        {
            if (viewBag != null)
            {
                AddDictionaryValues(GetProperties(viewBag).ToDictionary(x => x.Key, x => x.Value));
            }
        }

        private static IEnumerable<KeyValuePair<string, object>> GetProperties(object obj)
        {
            Contract.Requires(obj != null);

            var properties = obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            return
                properties.Select(
                    propertyInfo => new KeyValuePair<string, object>(propertyInfo.Name, propertyInfo.GetValue(obj)));
        }
    }
}