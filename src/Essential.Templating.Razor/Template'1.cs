using RazorEngine.Templating;

namespace Essential.Templating.Razor
{
    public class Template<T> : Template, ITemplate<T>
    {
        public Template(TemplateContext templateContext) : base(templateContext)
        {
        }

        public T Model { get; set; }

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
    }
}