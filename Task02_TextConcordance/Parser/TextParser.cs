using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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

        //Filling words list
        private void FillInWordsList()
        {
            Regex regex = new Regex(@"[A-Za-z]+");
            string pageText;
            MatchCollection matches;
            Word word;
            _words = new List<Word>();

            foreach (Page page in _pages)
            {
                //Creating collection of all words on page
                pageText = JoinLines(page);
                matches = regex.Matches(pageText);

                foreach (Match m in matches)
                {
                    //Creating Word object from every word on page
                    word = new Word(m.Value, 1, page.CurrentPageNumber);

                    if (!IsWordsContainsWord(word))
                    {
                        _words.Add(word);
                    }
                }
            }
        }

        //Join all lines on page with 'space' symbol
        //in one string. Ignores case of letters
        private string JoinLines(Page page)
        {
            return page.Lines.Aggregate("", (current, line) => current + (line.ToLower() + " "));
        }

        //Check if list of words already contains word. If true
        //increases word.RepeatCount. Also check if word already
        //contain current page number
        private bool IsWordsContainsWord(Word word)
        {
            foreach (Word w in _words)
            {
                if (!w.Value.Equals(word.Value)) continue;
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
            return false;
        }

        //Creating concordance of the document
        public Concordance CreateConcordance()
        {
            FillInWordsList();
            return new Concordance(_words);
        }
    }
}
