using System;
using System.Diagnostics.Contracts;
using RazorEngine.Templating;

namespace Essential.Templating.Razor.Email
{
    public class EmailTemplate<T> : EmailTemplate, ITemplate<T>
    {
        public EmailTemplate(TemplateContext templateContext) : base(templateContext)
        {
            Contract.Requires<ArgumentNullException>(templateContext != null);
        }

        public T Model { get; set; }
    }
}
