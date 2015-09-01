using System;
using System.Diagnostics;
using RazorEngine.Templating;

namespace Essential.Templating.Razor
{
    public class ExposingTemplate<T> : ExposingTemplate, ITemplate<T>
    {
        public ExposingTemplate(TemplateContext templateContext)
            : base(templateContext)
        {
            if (templateContext == null)
            {
                throw new ArgumentNullException("templateContext");
            }
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