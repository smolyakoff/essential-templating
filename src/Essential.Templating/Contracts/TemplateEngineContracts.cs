using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Threading.Tasks;
using Essential.Templating.Rendering;
using RazorEngine.Templating;

namespace Essential.Templating.Contracts
{
    [ContractClassFor(typeof (ITemplateEngine))]
    internal abstract class TemplateEngineContracts : ITemplateEngine
    {
        public string Render(string path, object viewBag = null, CultureInfo culture = null)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(path));

            throw new NotImplementedException();
        }

        public string Render<T>(string path, T model, object viewBag = null, CultureInfo culture = null)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(path));

            throw new NotImplementedException();
        }

        public TResult Render<TTemplate, TResult>(string path, IRenderer<TTemplate, TResult> renderer,
            object viewBag = null, CultureInfo culture = null)
            where TTemplate : Template
            where TResult : class
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(path));
            Contract.Requires<ArgumentNullException>(renderer != null);

            throw new NotImplementedException();
        }

        public TResult Render<TTemplate, TResult, T>(string path, IRenderer<TTemplate, TResult> renderer, T model,
            object viewBag = null,
            CultureInfo culture = null) where TTemplate : Template, ITemplate<T> where TResult : class
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(path));
            Contract.Requires<ArgumentNullException>(renderer != null);

            throw new NotImplementedException();
        }

        public Task<string> RenderAsync(string path, object viewBag = null, CultureInfo culture = null)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(path));

            throw new NotImplementedException();
        }

        public Task<string> RenderAsync<T>(string path, T model, object viewBag = null, CultureInfo culture = null)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(path));

            throw new NotImplementedException();
        }

        public Task<TResult> RenderAsync<TTemplate, TResult>(string path, IRenderer<TTemplate, TResult> renderer,
            object viewBag = null, CultureInfo culture = null) where TTemplate : Template where TResult : class
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(path));
            Contract.Requires<ArgumentNullException>(renderer != null);

            throw new NotImplementedException();
        }

        public Task<TResult> RenderAsync<TTemplate, TResult, T>(string path, IRenderer<TTemplate, TResult> renderer,
            T model, object viewBag = null,
            CultureInfo culture = null) where TTemplate : Template, ITemplate<T> where TResult : class
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(path));
            Contract.Requires<ArgumentNullException>(renderer != null);

            throw new NotImplementedException();
        }
    }
}