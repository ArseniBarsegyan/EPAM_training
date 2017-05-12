using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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

        public ICollection<Sentence> RemoveAllWordsStartWithConsonant()
        {
            Regex consonantWord = new Regex(@"\b[b-d,f-h,j-n,p-t,v,w,x,z]\S+\b");

            foreach (var sentence in Sentences)
            {
                for (int i = 0; i < sentence.SentenceItems.Count; i++)
                {
                    var item = sentence.SentenceItems.ElementAt(i);
                    if (consonantWord.IsMatch(item.Value))
                        sentence.SentenceItems.Remove(item);
                }
            }
            return Sentences;
        }
    }
}
