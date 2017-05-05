using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Parser
{
    public class Concordance
    {
        private List<Word> _words;
        private FileStream _fileStream;

        public Concordance(List<Word> words)
        {
            _words = words;
        }

        //Group words by alphabet
        public string GroupByAlphaBet()
        {
            string resultString = "";
            var wordsGroup = _words.GroupBy(p => p.Value.Substring(0, 1)).OrderBy(p => p.Key);

            foreach (var group in wordsGroup)
            {
                resultString += group.Key + "\n";

                foreach (var g in group)
                {
                    resultString += String.Format("{0} {1}:", g.Value, g.RepeatCount);

                    foreach (int page in g.PagesNumbers)
                    {
                        resultString += " " + page;
                    }
                    resultString += "\n";
                }
            }

            return resultString;
        }
    }
}
