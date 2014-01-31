namespace Essential.Templating
{
    public interface IExposingTemplate
    {
        void Run(ITemplateVisitor templateVisitor);
    }
}