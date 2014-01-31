using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Threading.Tasks;
using RazorEngine;
using RazorEngine.Templating;

namespace Essential.Templating.Compilation
{
    internal static class TemplateServiceExtensions
    {
        public static Type Compile(this ITemplateService razorService, Stream templateStream, Type modelType)
        {
            Contract.Requires(razorService != null);
            Contract.Requires(templateStream != null);
            Contract.Ensures(Contract.Result<Type>() != null);

            Type type;
            try
            {
                using (var reader = new StreamReader(templateStream))
                {
                    var templateString = reader.ReadToEnd();
                    type = razorService.CreateTemplateType(templateString, modelType);
                }
            }
            catch (Exception ex)
            {
                throw new CompilationException(ex);
            }
            if (type == null)
            {
                throw new CompilationException();
            }
            return type;
        }

        public static async Task<Type> CompileAsync(this ITemplateService razorService, Stream templateStream,
            Type modelType)
        {
            Contract.Requires(templateStream != null);
            Contract.Ensures(Contract.Result<Type>() != null);

            Type type;
            try
            {
                using (var reader = new StreamReader(templateStream))
                {
                    var templateString = await reader.ReadToEndAsync();
                    type = await Task.Run(() => razorService.CreateTemplateType(templateString, modelType));
                }
            }
            catch (Exception ex)
            {
                throw new CompilationException(ex);
            }
            if (type == null)
            {
                throw new CompilationException();
            }
            return type;
        }

        public static ITemplateCompiler GetCompiler(this Language language)
        {
            switch (language)
            {
                case Language.CSharp:
                    return new CSharpTemplateCompiler();
                case Language.VisualBasic:
                    return new VBTemplateCompiler();
                default:
                    return new CSharpTemplateCompiler();
            }
        }

        public static string GetFileExtension(this Language language)
        {
            switch (language)
            {
                case Language.CSharp:
                    return ".cshtml";
                case Language.VisualBasic:
                    return ".vbhtml";
                default:
                    return ".tmpl";
            }
        }
    }
}