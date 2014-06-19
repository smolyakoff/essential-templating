using System.Diagnostics.Contracts;

namespace Essential.Templating.Razor.Email
{
    internal class EmailTemplateLayout : ExposingTemplate
    {
        public EmailTemplateLayout(TemplateContext templateContext) : base(templateContext)
        {
            Contract.Requires(templateContext != null);
        }

        public override void Execute()
        {
            RenderSection(Conventions.HtmlSectionName, false);
            RenderSection(Conventions.TextSectionName, false);
            Write(RenderBody());
        }
    }
}
