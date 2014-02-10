using Essential.Templating.Common.Rendering;

namespace Essential.Templating.Razor.Tests.Helpers
{
    internal class TemplateStructureRenderer : IRenderer<ExposingTemplate, TemplateStructure>
    {
        public TemplateStructure Render(ExposingTemplate template, object viewBag)
        {
            var visitor = new TemplateStructureVisitor();
            var exposingTemplate = (IExposingTemplate) template;
            exposingTemplate.Run(visitor, viewBag);
            return visitor.TemplateStructure;
        }
    }
}