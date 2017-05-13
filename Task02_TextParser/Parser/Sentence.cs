using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Parser
{
    public class Sentence
    {
        public Sentence(IList<ISentenceItem> sentenceItems)
        {
            SentenceItems = sentenceItems;
        }

        public IList<ISentenceItem> SentenceItems { get; private set; }

        public int GetWordCount()
        {
            return SentenceItems.OfType<Word>().Count();
        }

        public bool IsInterrogativeSentence()
        {
            return SentenceItems.Any(item => item.Value.Contains("?"));
        }

        public IEnumerable<Word> GetAllUniqueWords(int length)
        {
            return SentenceItems.Where(s => s is Word && s.Value.Length == length)
                .GroupBy(s => s.Value)
                .Select(s => (Word) s.First());
        }

        public void ReplaceAllWordsByLengthWithSubString(int wordLength, IEnumerable<ISentenceItem> items)
        {
            for (int i = 0; i < SentenceItems.Count; i++)
            {
                var item = SentenceItems.ElementAt(i);
                if (!(item is Word)) continue;
                if (item.Value.Length != wordLength) continue;
                SentenceItems.Remove(item);
                foreach (var subItem in items)
                {
                    SentenceItems.Insert(i, subItem);
                }
            }
        }

        public void ReplaceAllSentenceWordsStartWithConsonant(int length)
        {
            Regex consonantWord = new Regex(@"\b[b-d,f-h,j-n,p-t,v,w,x,z]\S+\b", RegexOptions.IgnoreCase);
            var wordsToRemove = SentenceItems.Where(x => x is Word)
                .Where(x => consonantWord.IsMatch(x.Value) && x.Value.Length == length);
            SentenceItems = SentenceItems.Except(wordsToRemove).ToList();
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (var element in SentenceItems)
            {
                builder.Append(element.Value);
            }
            return builder.ToString();
        }
    }
}
