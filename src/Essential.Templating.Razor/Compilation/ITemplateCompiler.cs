using System;
using System.IO;
using System.Threading.Tasks;

namespace Essential.Templating.Razor.Compilation
{
    internal interface ITemplateCompiler
    {
        bool EnsureNamespace(string @namespace);

        Type Compile(Stream razorTemplate);

        Type Compile(Stream razorTemplate, Type modelType);

        Task<Type> CompileAsync(Stream razorTemplate);

        Task<Type> CompileAsync(Stream razorTemplate, Type modelType);
    }
}