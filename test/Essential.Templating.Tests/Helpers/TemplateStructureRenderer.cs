using Essential.Templating.Rendering;
using RazorEngine.Templating;

namespace Essential.Templating.Tests.Helpers
{
    internal class TemplateStructureRenderer : IRenderer<ExposingTemplate, TemplateStructure>
    {
        public TemplateStructure Render(ExposingTemplate template, DynamicViewBag viewBag)
        {
            var visitor = new TemplateStructureVisitor();
            var exposingTemplate = (IExposingTemplate) template;
            exposingTemplate.Run(visitor);
            return visitor.TemplateStructure;
        }
    }
}