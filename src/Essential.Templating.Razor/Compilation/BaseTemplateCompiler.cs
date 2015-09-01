using System;
using System.IO;
using System.Threading.Tasks;
using RazorEngine.Templating;

namespace Essential.Templating.Razor.Compilation
{
    internal class BaseTemplateCompiler : ITemplateCompiler
    {
        private readonly ITemplateService _razorService;

        internal BaseTemplateCompiler(ITemplateService razorService)
        {
            if (razorService == null)
            {
                throw new ArgumentNullException("razorService");
            }

            _razorService = razorService;
        }

        public Type Compile(Stream razorTemplate, Type modelType)
        {
            return _razorService.Compile(razorTemplate, modelType);
        }

        public Task<Type> CompileAsync(Stream razorTemplate, Type modelType)
        {
            return _razorService.CompileAsync(razorTemplate, modelType);
        }
    }
}