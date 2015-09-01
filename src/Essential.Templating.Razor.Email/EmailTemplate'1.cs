using System.Diagnostics;
using RazorEngine.Templating;

namespace Essential.Templating.Razor.Email
{
    public class EmailTemplate<T> : EmailTemplate, ITemplate<T>
    {
        public EmailTemplate(TemplateContext templateContext) : base(templateContext)
        {
        }

        public override void SetModel(object model)
        {
            if (model is T)
            {
                Model = (T) model;
            }
        }

        protected override object GetModel()
        {
            return Model;
        }

        public T Model { get; set; }
    }
}