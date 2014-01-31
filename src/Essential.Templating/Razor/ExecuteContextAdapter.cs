using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Reflection;
using RazorEngine.Templating;

namespace Essential.Templating.Razor
{
    internal class ExecuteContextAdapter
    {
        private readonly ExecuteContext _context;

        private readonly FieldInfo _contextField = typeof (TemplateBase).GetFields(BindingFlags.NonPublic |
                                                                                   BindingFlags.Instance)
            .FirstOrDefault(f => f.FieldType == typeof (ExecuteContext));

        private readonly PropertyInfo _currentWriterProperty = typeof (ExecuteContext).GetProperty("CurrentWriter",
            BindingFlags.NonPublic | BindingFlags.Instance);

        private readonly MethodInfo _pushBodyMethod = typeof (ExecuteContext).GetMethod("PushBody",
            BindingFlags.NonPublic | BindingFlags.Instance);

        private readonly TemplateBase _template;

        internal ExecuteContextAdapter(TemplateBase template, ExecuteContext context)
        {
            Contract.Requires(template != null);

            _template = template;
            _context = context;
            SetContext(_context);
        }

        internal TextWriter CurrentWriter
        {
            set { SetCurrentWriter(value); }
            get { return GetCurrentWriter(); }
        }

        internal ExecuteContext Context
        {
            get { return _context; }
        }

        private TextWriter GetCurrentWriter()
        {
            if (_currentWriterProperty == null)
            {
                throw new InvalidOperationException("CurrentWriter property was not found.");
            }
            return (TextWriter) _currentWriterProperty.GetValue(_context);
        }

        internal void PushBody(TemplateWriter templateWriter)
        {
            if (_pushBodyMethod == null)
            {
                throw new InvalidOperationException("PushBody method was not found.");
            }
            _pushBodyMethod.Invoke(_context, new object[] {templateWriter});
        }

        private void SetContext(ExecuteContext executeContext)
        {
            if (_contextField == null)
            {
                throw new InvalidOperationException("_context field was not found.");
            }
            _contextField.SetValue(_template, executeContext);
        }

        private void SetCurrentWriter(TextWriter textWriter)
        {
            if (_currentWriterProperty == null)
            {
                throw new InvalidOperationException("CurrentWriter property was not found.");
            }
            _currentWriterProperty.SetValue(_context, textWriter);
        }
    }
}