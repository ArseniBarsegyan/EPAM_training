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
    }
}
