namespace Essential.Templating.Razor
{
    public interface IExposingTemplate
    {
        void Run(ITemplateVisitor templateVisitor);
    }
}