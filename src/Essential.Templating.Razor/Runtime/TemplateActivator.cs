using System;

namespace Essential.Templating.Razor.Runtime
{
    internal class TemplateActivator
    {
        private readonly ITemplateFactory _templateFactory;

        public TemplateActivator(ITemplateFactory templateFactory)
        {
            if (templateFactory == null)
            {
                throw new ArgumentNullException("templateFactory");
            }

            _templateFactory = templateFactory;
        }

        public Template Activate(Type templateType, TemplateContext context)
        {
            if (templateType == null)
            {
                throw new ArgumentNullException("templateType");
            }

            if (context == null)
            {
                throw new ArgumentNullException("context");
            }


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

        public Template Activate(Type templateType, TemplateContext context, object model)
        {
            if (templateType == null)
            {
                throw new ArgumentNullException("templateType");
            }

            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            var template = Activate(templateType, context);
            if (model != null)
            {
                try
                {
                    template.SetModel(model);
                }
                catch (Exception ex)
                {
                    throw new ActivatorException(templateType, ex);
                }

                return template;
            }

            return template;
        }
    }
}