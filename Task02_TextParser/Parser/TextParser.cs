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

        //Check if list of words already contains word. If true
        //increases word.RepeatCount. Also check if word already
        //contain current page number
        private bool IsWordsContainsWord(Word word)
        {
            foreach (Word w in _words)
            {
                if (w.Value.Equals(word.Value))
                {
                    w.RepeatCount++;

                    //Check if 'w' already contains page number
                    foreach (int number in word.PagesNumbers)
                    {
                        if (!w.PagesNumbers.Contains(number))
                        {
                            w.PagesNumbers.Add(number);
                        }
                    }
                    return true;
                }
            }
            return false;
        }
    }
}
