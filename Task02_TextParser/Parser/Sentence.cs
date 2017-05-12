using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Parser
{
    public class Sentence
    {
        public Sentence(IList<ISentenceItem> sentenceItems)
        {
            SentenceItems = sentenceItems;
        }

        public IList<ISentenceItem> SentenceItems { get; set; }

        public int GetWordCount()
        {
            return SentenceItems.OfType<Word>().Count();
        }

        public override string ToString()
        {
            return SentenceItems.Aggregate(String.Empty, (current, element) => current + element.Value);
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

        public List<ISentenceItem> GetAllSentenceItemsStartWithNoConsonant()
        {
            Regex consonantWord = new Regex(@"\b[b-d,f-h,j-n,p-t,v,w,x,z]\S+\b");
            return SentenceItems.Where(x => !consonantWord.IsMatch(x.Value)).ToList();
        }
    }
}
