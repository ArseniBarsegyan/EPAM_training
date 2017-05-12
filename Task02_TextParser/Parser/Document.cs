using System.Collections.Generic;
using System.Linq;

namespace Parser
{
    public class Document
    {
        public Document(ICollection<Sentence> sentences)
        {
            Sentences = sentences;
        }

        public ICollection<Sentence> Sentences { get; private set; }

        public IEnumerable<Sentence> GetSortedSentencesByWordsCount()
        {
            return Sentences.OrderBy(s => s.GetWordCount());
        }

        public List<Word> GetUniqueWordsInInterrogativeSentences(int length)
        {
            List<Word> uniqueWords = new List<Word>();
            Sentences.Where(s => s.IsInterrogativeSentence()).ToList()
                .ForEach(x => { uniqueWords.AddRange(x.GetAllUniqueWords(length)); });
            return uniqueWords;
        }

        public ICollection<Sentence> RemoveAllWordsStartWithConsonant(int length)
        {
            Sentences.ToList()
                .ForEach(s => s.SentenceItems = s.GetAllSentenceItemsWithoutConsonant(length));
            return Sentences;
        }

        public ICollection<Sentence> ReplaceAllWordsBySubString(int length, string subString)
        {
            //Making sentence from substring
            List<Sentence> subStringSentences = subString.ParseText();

            List<ISentenceItem> subStringSenteceItems = subStringSentences
                .SelectMany(sentence => sentence.SentenceItems)
                .ToList();

            subStringSenteceItems.Reverse();

            foreach (var sentence in Sentences)
            {
                for (int i = 0; i < sentence.SentenceItems.Count; i++)
                {
                    var item = sentence.SentenceItems.ElementAt(i);
                    if (!(item is Word)) continue;
                    if (item.Value.Length != length) continue;
                    sentence.SentenceItems.Remove(item);
                    foreach (var subItem in subStringSenteceItems)
                    {
                        sentence.SentenceItems.Insert(i, subItem);
                    }
                }
            }
            return Sentences;
        }
    }
}
