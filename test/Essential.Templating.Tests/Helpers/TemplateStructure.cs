using System.Collections.Generic;

namespace Essential.Templating.Tests.Helpers
{
    internal class TemplateStructure
    {
        public TemplateStructure()
        {
            Sections = new List<string>();
        }

        public string Body { get; set; }

        public bool StartCalled { get; set; }

        public bool EndCalled { get; set; }

        public IList<string> Sections { get; set; }
    }
}