using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Threading.Tasks;
using Essential.Templating.Razor.Contracts;

namespace Essential.Templating.Razor.Compilation
{
    [ContractClass(typeof(TemplateCompilerContracts))]
    internal interface ITemplateCompiler
    {
        bool EnsureNamespace(string @namespace);

        Type Compile(Stream razorTemplate);

        Type Compile(Stream razorTemplate, Type modelType);

        Task<Type> CompileAsync(Stream razorTemplate);

        Task<Type> CompileAsync(Stream razorTemplate, Type modelType);
    }
}