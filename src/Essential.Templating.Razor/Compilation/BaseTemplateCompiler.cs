using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Threading.Tasks;
using RazorEngine.Configuration;
using RazorEngine.Templating;

namespace Essential.Templating.Razor.Compilation
{
    internal class BaseTemplateCompiler : ITemplateCompiler
    {
        private readonly ITemplateService _razorService;

        private readonly HashSet<string> _namespaces = new HashSet<string>(); 

        internal BaseTemplateCompiler(ITemplateService razorService)
        {
            Contract.Requires(razorService != null);

            _razorService = razorService;
        }

        public bool EnsureNamespace(string @namespace)
        {
            var added = _namespaces.Add(@namespace);
            if (added)
            {
                _razorService.AddNamespace(@namespace);
            }
            return added;
        }

        public Type Compile(Stream razorTemplate)
        {
            return _razorService.Compile(razorTemplate, null);
        }

        public Type Compile(Stream razorTemplate, Type modelType)
        {
            return _razorService.Compile(razorTemplate, modelType);
        }

        public Task<Type> CompileAsync(Stream razorTemplate)
        {
            return _razorService.CompileAsync(razorTemplate, null);
        }

        public Task<Type> CompileAsync(Stream razorTemplate, Type modelType)
        {
            return _razorService.CompileAsync(razorTemplate, modelType);
        }
    }
}
