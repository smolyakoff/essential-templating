using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Threading;
using Essential.Templating.Common;
using Essential.Templating.Common.Caching;
using Essential.Templating.Common.Rendering;
using Essential.Templating.Common.Storage;
using Essential.Templating.Razor.Compilation;
using Essential.Templating.Razor.Configuration;
using Essential.Templating.Razor.Rendering;
using Essential.Templating.Razor.Runtime;
using RazorEngine.Templating;

namespace Essential.Templating.Razor
{
    public partial class RazorTemplateEngine : ITemplateEngine
    {
        private readonly TemplateActivator _activator;
        private readonly ITemplateCache<Type> _cache;
        private readonly TimeSpan _cacheExpiration;

        private readonly ITemplateCompiler _compiler;
        private readonly IResourceProvider _resourceProvider;

        public RazorTemplateEngine(IResourceProvider resourceProvider, ITemplateFactory templateFactory)
        {
            Contract.Requires<ArgumentNullException>(resourceProvider != null);
            Contract.Requires<ArgumentNullException>(templateFactory != null);

            var configuration = new RazorTemplateEngineConfiguration
            {
                ResourceProvider = resourceProvider,
                TemplateFactory = templateFactory
            };
            _compiler = configuration.CodeLanguage.GetCompiler();
            _cache = configuration.CachePolicy.GetCache<Type>();
            _activator = new TemplateActivator(configuration.TemplateFactory);
            _resourceProvider = configuration.ResourceProvider;
            _cacheExpiration = configuration.CacheExpiration;
        }

        public RazorTemplateEngine() : this(new RazorTemplateEngineConfiguration())
        {
        }

        public RazorTemplateEngine(RazorTemplateEngineConfiguration configuration)
        {
            Contract.Requires<ArgumentNullException>(configuration != null);

            _compiler = configuration.CodeLanguage.GetCompiler();
            _cache = configuration.CachePolicy.GetCache<Type>();
            _activator = new TemplateActivator(configuration.TemplateFactory);
            _resourceProvider = configuration.ResourceProvider;
            _cacheExpiration = configuration.CacheExpiration;
        }

        public string Render(string path, object viewBag = null, CultureInfo culture = null)
        {
            return Render(path, renderer: new StringRenderer(), viewBag: viewBag, culture: culture);
        }

        public string Render<T>(string path, T model, object viewBag = null, CultureInfo culture = null)
        {
            culture = culture ?? Thread.CurrentThread.CurrentCulture;

            var template = ResolveTemplate(path, culture, model);
            try
            {
                var concreteTemplate = template as Template;
                if (concreteTemplate == null)
                {
                    throw new TypeMismatchException(template.GetType(), typeof (Template));
                }
                var renderer = new StringRenderer();
                return renderer.Render(concreteTemplate, viewBag);
            }
            catch (Exception ex)
            {
                throw new TemplateEngineException(ex);
            }
        }

        public TResult Render<TTemplate, TResult>(string path, IRenderer<TTemplate, TResult> renderer,
            object viewBag = null, CultureInfo culture = null)
            where TTemplate : class
            where TResult : class
        {
            culture = culture ?? Thread.CurrentThread.CurrentCulture;

            var template = ResolveTemplate(path, culture);
            try
            {
                var concreteTemplate = template as TTemplate;
                if (concreteTemplate == null)
                {
                    throw new TypeMismatchException(template.GetType(), typeof (TTemplate));
                }
                return renderer.Render(concreteTemplate, viewBag);
            }
            catch (Exception ex)
            {
                throw new TemplateEngineException(ex);
            }
        }

        public TResult Render<TTemplate, TResult, T>(string path, IRenderer<TTemplate, TResult> renderer, T model,
            object viewBag = null, CultureInfo culture = null)
            where TTemplate :class
            where TResult : class
        {
            culture = culture ?? Thread.CurrentThread.CurrentCulture;

            var template = ResolveTemplate(path, culture, model);
            try
            {
                var concreteTemplate = template as TTemplate;
                if (concreteTemplate == null)
                {
                    throw new TypeMismatchException(template.GetType(), typeof (TTemplate));
                }
                return renderer.Render(concreteTemplate, viewBag);
            }
            catch (Exception ex)
            {
                throw new TemplateEngineException(ex);
            }
        }

        public bool EnsureNamespace(string @namespace)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(@namespace));

            return _compiler.EnsureNamespace(@namespace);
        }

        private Template ResolveTemplate(string path, CultureInfo culture)
        {
            Contract.Requires(culture != null);
            Contract.Ensures(Contract.Result<Template>() != null);

            var type = ResolveTemplateType(path, culture);
            return ActivateTemplate(type,
                new TemplateContext(path, culture, _resourceProvider, new Tool(this)));
        }

        private Template ResolveTemplate(string path, CultureInfo culture,
            IDictionary<string, object> contextEnvironmement)
        {
            Contract.Requires(culture != null);
            Contract.Requires(contextEnvironmement != null);
            Contract.Ensures(Contract.Result<Template>() != null);
            

            var type = ResolveTemplateType(path, culture);
            var context = new TemplateContext(path, culture, _resourceProvider, new Tool(this));
            foreach (var pair in contextEnvironmement)
            {
                context.Put(pair.Key, pair.Value);
            }
            return ActivateTemplate(type, context);
        }

        private ITemplate<T> ResolveTemplate<T>(string path, CultureInfo culture, T model)
        {
            Contract.Requires(culture != null);
            Contract.Ensures(Contract.Result<ITemplate>() != null);

            var type = ResolveTemplateType<T>(path, culture);
            return ActivateTemplate(type,
                new TemplateContext(path, culture, _resourceProvider, new Tool(this)), model);
        }

        private ITemplate<T> ResolveTemplate<T>(string path, CultureInfo culture, T model, IDictionary<string, object> contextEnvironment)
        {
            Contract.Requires(culture != null);
            Contract.Requires(contextEnvironment != null);
            Contract.Ensures(Contract.Result<ITemplate>() != null);

            var type = ResolveTemplateType<T>(path, culture);
            var context = new TemplateContext(path, culture, _resourceProvider, new Tool(this));
            foreach (var pair in contextEnvironment)
            {
                context.Put(pair.Key, pair.Value);
            }
            return ActivateTemplate(type, context, model);
        }

        private Template ResolveTemplate(string path, CultureInfo culture, object model)
        {
            Contract.Requires(culture != null);
            Contract.Ensures(Contract.Result<Template>() != null);

            var type = model == null
                ? ResolveTemplateType(path, culture)
                : ResolveTemplateType(path, culture, model);
            return ActivateTemplate(type,
                new TemplateContext(path, culture, _resourceProvider, new Tool(this)), model);
        }

        private Template ResolveTemplate(string path, CultureInfo culture, object model,
            IDictionary<string, object> contextEnvironmement)
        {
            Contract.Requires(culture != null);
            Contract.Requires(contextEnvironmement != null);
            Contract.Ensures(Contract.Result<Template>() != null);
            

            var type = ResolveTemplateType(path, culture, model);
            var context = new TemplateContext(path, culture, _resourceProvider, new Tool(this));
            foreach (var pair in contextEnvironmement)
            {
                context.Put(pair.Key, pair.Value);
            }
            return ActivateTemplate(type, context);
        }

        private Type ResolveTemplateType(string path, CultureInfo culture)
        {
            Contract.Requires(culture != null);
            var cacheKey = new TemplateCacheKey(path, culture);
            try
            {
                var cacheItem = _cache.Get(cacheKey);
                if (cacheItem != null)
                {
                    return cacheItem.TemplateInfo;
                }
                var templateStream = _resourceProvider.Get(path, culture);
                var type = _compiler.Compile(templateStream);
                _cache.Put(cacheKey, type, _cacheExpiration);
                return type;
            }
            catch (ResourceNotFoundException ex)
            {
                throw new TemplateEngineException(ex);
            }
            catch (CompilationException ex)
            {
                throw new TemplateEngineException(ex);
            }
            catch (Exception ex)
            {
                throw new TemplateEngineException("Can't resolve template type.", ex);
            }
        }

        private Type ResolveTemplateType<T>(string path, CultureInfo culture)
        {
            Contract.Requires(culture != null);
            var cacheKey = new TemplateCacheKey(path, culture);
            try
            {
                var cacheItem = _cache.Get(cacheKey);
                if (cacheItem != null)
                {
                    return cacheItem.TemplateInfo;
                }
                var templateStream = _resourceProvider.Get(path, culture);
                var type = _compiler.Compile(templateStream, typeof (T));
                _cache.Put(cacheKey, type, _cacheExpiration);
                return type;
            }
            catch (ResourceNotFoundException ex)
            {
                throw new TemplateEngineException(ex);
            }
            catch (CompilationException ex)
            {
                throw new TemplateEngineException(ex);
            }
            catch (Exception ex)
            {
                throw new TemplateEngineException("Can't resolve template type.", ex);
            }
        }

        private Type ResolveTemplateType(string path, CultureInfo culture, object model)
        {
            Contract.Requires(culture != null);
            var cacheKey = new TemplateCacheKey(path, culture);
            try
            {
                var cacheItem = _cache.Get(cacheKey);
                if (cacheItem != null)
                {
                    return cacheItem.TemplateInfo;
                }
                var templateStream = _resourceProvider.Get(path, culture);
                var type = _compiler.Compile(templateStream, model.GetType());
                _cache.Put(cacheKey, type, _cacheExpiration);
                return type;
            }
            catch (ResourceNotFoundException ex)
            {
                throw new TemplateEngineException(ex);
            }
            catch (CompilationException ex)
            {
                throw new TemplateEngineException(ex);
            }
            catch (Exception ex)
            {
                throw new TemplateEngineException("Can't resolve template type.", ex);
            }
        }

        private Template ActivateTemplate(Type templateType, TemplateContext context)
        {
            Contract.Requires(templateType != null);
            Contract.Requires(context != null);
            try
            {
                return _activator.Activate(templateType, context);
            }
            catch (Exception ex)
            {
                throw new TemplateEngineException(ex);
            }
        }

        private ITemplate<T> ActivateTemplate<T>(Type templateType, TemplateContext context, T model)
        {
            Contract.Requires(templateType != null);
            Contract.Requires(context != null);
            try
            {
                return _activator.Activate(templateType, context, model);
            }
            catch (Exception ex)
            {
                throw new TemplateEngineException(ex);
            }
        }

        private Template ActivateTemplate(Type templateType, TemplateContext context, object model)
        {
            Contract.Requires(templateType != null);
            Contract.Requires(context != null);
            try
            {
                return _activator.Activate(templateType, context, model);
            }
            catch (Exception ex)
            {
                throw new TemplateEngineException(ex);
            }
        }

        private class Tool : ITemplateTool
        {
            private readonly RazorTemplateEngine _razorTemplateEngine;

            public Tool(RazorTemplateEngine razorTemplateEngine)
            {
                _razorTemplateEngine = razorTemplateEngine;
            }

            public Template Resolve(string path, CultureInfo culture, object model)
            {
                return _razorTemplateEngine.ResolveTemplate(path, culture, model);
            }

            public Template Resolve(string path, CultureInfo culture)
            {
                return _razorTemplateEngine.ResolveTemplate(path, culture);
            }

            public Template Resolve(string path, CultureInfo culture, IDictionary<string, object> contextEnvironment)
            {
                return _razorTemplateEngine.ResolveTemplate(path, culture, contextEnvironment);
            }

            public Template Resolve(string path, CultureInfo culture, object model, IDictionary<string, object> contextEnvironment)
            {
                return _razorTemplateEngine.ResolveTemplate(path, culture, model, contextEnvironment);
            }

            public string RenderString(Template template)
            {
                var renderer = new StringRenderer();
                return renderer.Render(template, new DynamicViewBag());
            }
        }
    }
}