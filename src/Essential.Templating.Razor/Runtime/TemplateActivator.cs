using System;
using System.Diagnostics.Contracts;
using RazorEngine.Templating;

namespace Essential.Templating.Razor.Runtime
{
    internal class TemplateActivator
    {
        private readonly ITemplateFactory _templateFactory;

        public TemplateActivator(ITemplateFactory templateFactory)
        {
            Contract.Requires(templateFactory != null);

            _templateFactory = templateFactory;
        }

        public Template Activate(Type templateType, TemplateContext context)
        {
            Contract.Requires(templateType != null);
            Contract.Requires(context != null);

            Template template = null;
            try
            {
                template = _templateFactory.Create(templateType, context);
            }
            catch (Exception ex)
            {
                throw new ActivatorException(templateType, ex);
            }
            if (template == null)
            {
                throw new ActivatorException(templateType);
            }
            return template;
        }

        public ITemplate<T> Activate<T>(Type templateType, TemplateContext context, T model)
        {
            Contract.Requires(templateType != null);
            Contract.Requires(context != null);

            var template = Activate(templateType, context);
            var templateWithModel = template as ITemplate<T>;
            if (templateWithModel == null)
            {
                throw new TypeMismatchException(templateType, typeof (ITemplate<T>));
            }
            templateWithModel.Model = model;
            return templateWithModel;
        }

        public Template Activate(Type templateType, TemplateContext context, object model)
        {
            Contract.Requires(templateType != null);
            Contract.Requires(context != null);

            var template = Activate(templateType, context);
            var property = template.GetType().GetProperty("Model");
            if (property == null)
            {
                throw new TypeMismatchException(templateType, typeof (Template));
            }
            try
            {
                property.SetValue(template, model);
            }
            catch (Exception ex)
            {
                throw new ActivatorException(templateType, ex);
            }
            return template;
        }
    }
}