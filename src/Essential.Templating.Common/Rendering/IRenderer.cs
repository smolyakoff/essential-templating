namespace Essential.Templating.Common.Rendering
{
    public interface IRenderer<in TTemplate, out TResult>
        where TTemplate : class
        where TResult : class
    {
        TResult Render(TTemplate template, object viewBag);
    }
}