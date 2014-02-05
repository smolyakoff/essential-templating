namespace Essential.Templating.Razor.Email
{
    internal class EmailTemplateLayout : ExposingTemplate
    {
        public EmailTemplateLayout(TemplateContext templateContext) : base(templateContext)
        {
        }

        public override void Execute()
        {
            RenderSection(Conventions.HtmlSectionName, false);
            RenderSection(Conventions.TextSectionName, false);
        }
    }
}
