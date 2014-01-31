using System.Diagnostics.Contracts;
using System.Globalization;
using System.Threading.Tasks;
using Essential.Templating.Contracts;
using Essential.Templating.Rendering;
using RazorEngine.Templating;

namespace Essential.Templating
{
    [ContractClass(typeof (TemplateEngineContracts))]
    public interface ITemplateEngine
    {
        string Render(string path, object viewBag = null, CultureInfo culture = null);
        string Render<T>(string path, T model, object viewBag = null, CultureInfo culture = null);

        TResult Render<TTemplate, TResult>(string path, IRenderer<TTemplate, TResult> renderer, object viewBag = null,
            CultureInfo culture = null)
            where TTemplate : Template
            where TResult : class;

        TResult Render<TTemplate, TResult, T>(string path, IRenderer<TTemplate, TResult> renderer, T model,
            object viewBag = null, CultureInfo culture = null)
            where TTemplate : Template, ITemplate<T>
            where TResult : class;

        Task<string> RenderAsync(string path, object viewBag = null, CultureInfo culture = null);

        Task<string> RenderAsync<T>(string path, T model, object viewBag = null, CultureInfo culture = null);

        Task<TResult> RenderAsync<TTemplate, TResult>(string path, IRenderer<TTemplate, TResult> renderer,
            object viewBag = null, CultureInfo culture = null)
            where TTemplate : Template
            where TResult : class;

        Task<TResult> RenderAsync<TTemplate, TResult, T>(string path, IRenderer<TTemplate, TResult> renderer, T model,
            object viewBag = null, CultureInfo culture = null)
            where TTemplate : Template, ITemplate<T>
            where TResult : class;
    }
}