using RazorEngine.Templating;

namespace Essential.Templating.Razor.Email
{
    public class EmailTemplate<T> : EmailTemplate, ITemplate<T>
    {
        public EmailTemplate(TemplateContext templateContext) : base(templateContext)
        {
        }

        public T Model { get; set; }
    }
}
