namespace Essential.Templating.Razor.Tests.Helpers
{
    internal class TemplateStructureVisitor : ITemplateVisitor
    {
        private readonly TemplateStructure _structure = new TemplateStructure();

        public TemplateStructure TemplateStructure
        {
            get { return _structure; }
        }

        public void Body(string text)
        {
            _structure.Body = text;
        }

        public void Section(string name, string text)
        {
            _structure.Sections.Add(name);
        }

        public void Start(Template template)
        {
            _structure.StartCalled = true;
        }

        public void End(Template template)
        {
            _structure.EndCalled = true;
        }
    }
}