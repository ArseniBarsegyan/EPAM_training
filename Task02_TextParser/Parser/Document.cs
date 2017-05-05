using System.Collections.Generic;

namespace Parser
{
    public class Document
    {
        public Document(string name, List<Page> pages)
        {
            Name = name;
            Pages = pages;
        }

        public string Name { get; private set; }
        public List<Page> Pages { get; private set; }
    }
}
