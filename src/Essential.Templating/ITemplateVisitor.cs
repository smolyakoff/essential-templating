namespace Essential.Templating
{
    public interface ITemplateVisitor
    {
        void Body(string text);

        void Section(string name, string text);

        void Start(Template template);

        void End(Template template);
    }
}