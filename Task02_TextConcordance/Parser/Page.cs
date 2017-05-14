using System.Collections.Generic;

namespace Parser
{
    public class Page
    {
        public Page(int currentPageNumber, int numberOfLines, ICollection<string> lines)
        {
            CurrentPageNumber = currentPageNumber;
            NumberOfLines = numberOfLines;
            Lines = lines;
        }

        public int CurrentPageNumber { get; private set; }
        public int NumberOfLines { get; private set; }
        public ICollection<string> Lines { get; }

        public void AddLine(string line)
        {
            Lines.Add(line);
        }
    }
}
