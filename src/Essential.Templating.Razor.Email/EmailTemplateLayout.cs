using RazorEngine.Templating;

namespace Essential.Templating.Razor.Email
{
    internal class EmailTemplateLayout : TemplateBase
    {
        public override void Execute()
        {
            RenderSection(Conventions.HtmlSectionName);
            RenderSection(Conventions.TextSectionName);
        }
    }
}
