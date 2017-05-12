using System;
using System.Collections.Generic;
using System.Linq;

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
    }
}
