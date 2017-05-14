using System.Collections.Generic;

namespace Parser
{
    public class Document
    {
        public Document(string name, ICollection<Page> pages)
        {
            Name = name;
            Pages = pages;
        }

        public string Name { get; private set; }
        public ICollection<Page> Pages { get; private set; }
    }
}
