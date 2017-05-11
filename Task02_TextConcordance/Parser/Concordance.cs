using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Parser
{
    public class Concordance
    {
        private List<Word> _words;

        public Concordance(List<Word> words)
        {
            _words = words;
        }

        //Group words list by alphabet
        private List<string> GroupByAlphaBet()
        {
            List<string> resultList = new List<string>();

            var wordsGroup = _words.OrderBy(p => p.Value)
                .GroupBy(p => p.Value.Substring(0, 1))
                .OrderBy(p => p.Key);

            foreach (var group in wordsGroup)
            {
                resultList.Add(group.Key.ToUpper());

                foreach (var g in group)
                {
                    string temp = String.Format("{0} {1}:", g.Value, g.RepeatCount);

                    foreach (int page in g.PagesNumbers)
                    {
                        temp += " " + page;
                    }
                    resultList.Add(temp);
                }
            }
            return resultList;
        }

        //Write file with results
        public void WriteFile(string path)
        {
            File.WriteAllLines(path, GroupByAlphaBet().ToArray());
        }
    }
}
