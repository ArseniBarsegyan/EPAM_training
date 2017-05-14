using System.Collections.Generic;
using System.Linq;

namespace Parser
{
    public class Text
    {
        public Text(ICollection<Sentence> sentences)
        {
            Sentences = sentences;
        }

        public ICollection<Sentence> Sentences { get; }

        public IEnumerable<Sentence> GetSortedSentencesByWordsCount()
        {
            return Sentences.OrderBy(s => s.GetWordCount());
        }

        public IEnumerable<Word> GetUniqueWordsInInterrogativeSentences(int length)
        {
            return Sentences.Where(x => x.IsInterrogativeSentence())
                .SelectMany(x => x.GetAllUniqueWords(length));
        }

        public void RemoveAllWordsStartWithConsonant(int length)
        {
            foreach (var sentence in Sentences)
            {
                sentence.ReplaceAllSentenceWordsStartWithConsonant(length);
            }
        }

        public void ReplaceAllWordsInSentenceBySubString(int sentenceNumber, int length,
            IEnumerable<ISentenceItem> items)
        {
            items = items.Reverse();
            Sentences.ElementAt(sentenceNumber).ReplaceAllWordsByLengthWithSubString(length, items);
        }
    }
}
