using System;
using System.Diagnostics.Contracts;
using RazorEngine.Templating;

namespace Essential.Templating.Razor
{
    public class ExposingTemplate<T> : ExposingTemplate, ITemplate<T>
    {
        public ExposingTemplate(TemplateContext templateContext)
            : base(templateContext)
        {
            Contract.Requires<ArgumentNullException>(templateContext != null);
        }

        public T Model { get; set; }
    }
}