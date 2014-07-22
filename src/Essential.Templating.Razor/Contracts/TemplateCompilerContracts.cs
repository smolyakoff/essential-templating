using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Threading.Tasks;
using Essential.Templating.Razor.Compilation;

namespace Essential.Templating.Razor.Contracts
{
    [ContractClassFor(typeof(ITemplateCompiler))]
    internal abstract class TemplateCompilerContracts : ITemplateCompiler
    {
        public bool EnsureNamespace(string @namespace)
        {
            Contract.Requires(!string.IsNullOrEmpty(@namespace));

            throw new NotImplementedException();
        }

        public Type Compile(Stream razorTemplate)
        {
            Contract.Requires(razorTemplate != null);
            Contract.Ensures(Contract.Result<Type>() != null);

            throw new NotImplementedException();
        }

        public Type Compile(Stream razorTemplate, Type modelType)
        {
            Contract.Requires(razorTemplate != null);
            Contract.Requires(modelType != null);
            Contract.Ensures(Contract.Result<Type>() != null);

            throw new NotImplementedException();
        }

        public Task<Type> CompileAsync(Stream razorTemplate)
        {
            Contract.Requires(razorTemplate != null);

            throw new NotImplementedException();
        }

        public Task<Type> CompileAsync(Stream razorTemplate, Type modelType)
        {
            Contract.Requires(razorTemplate != null);
            Contract.Requires(modelType != null);

            throw new NotImplementedException();
        }
    }
}
