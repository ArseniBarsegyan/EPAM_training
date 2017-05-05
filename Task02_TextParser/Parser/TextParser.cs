using System.Collections.Generic;

namespace Parser
{
    //Processes document model and
    //provides list of words
    public class TextParser
    {
        private List<Page> _pages;
        private List<Word> _words;

        public TextParser(Document document)
        {
            _pages = document.Pages;
        }

        //Join all lines on page with 'space' symbol
        //in one string. Ignores case of letters
        private string JoinLines(Page page)
        {
            string pageText = "";
            foreach (string line in page.Lines)
            {
                pageText += line.ToLower() + " ";
            }
            return pageText;
        }
    }
}
