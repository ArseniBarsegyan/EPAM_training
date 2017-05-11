using System.Collections.Generic;

namespace Parser
{
    public class Page
    {
        public Page(int currentPageNumber, int numberOfLines)
        {
            CurrentPageNumber = currentPageNumber;
            NumberOfLines = numberOfLines;
            Lines = new List<string>();
        }

        public int CurrentPageNumber { get; private set; }
        public int NumberOfLines { get; private set; }
        public List<string> Lines { get; private set; }

        public void AddLine(string line)
        {
            Lines.Add(line);
        }
    }
}
