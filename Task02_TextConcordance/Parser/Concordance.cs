using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Parser
{
    public class Concordance
    {
        private ICollection<Word> _words;

        public Concordance(ICollection<Word> words)
        {
            _words = words;
        }

        //Group words list by alphabet
        private IEnumerable<string> GroupByAlphaBet()
        {
            var resultList = new List<string>();

            var wordsGroup = _words.OrderBy(p => p.Value)
                .GroupBy(p => p.Value.Substring(0, 1))
                .OrderBy(p => p.Key);

            foreach (var group in wordsGroup)
            {
                resultList.Add(group.Key.ToUpper());

                foreach (var word in group)
                {
                    StringBuilder wordInfo = new StringBuilder(string.Format("{0} {1}: ", 
                        word.Value, word.RepeatCount));

                    foreach (var pageNumber in word.PagesNumbers)
                    {
                        wordInfo.Append(string.Format(" {0}", pageNumber));
                    }

                    resultList.Add(wordInfo.ToString());
                }
            }
            return resultList;
        }

        //Write file with results
        public void WriteFile(string path)
        {
            File.WriteAllLines(path, GroupByAlphaBet());
        }
    }
}
