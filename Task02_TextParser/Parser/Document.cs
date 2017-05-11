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

    }
}
