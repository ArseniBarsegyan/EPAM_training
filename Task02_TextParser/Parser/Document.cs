using System.Collections.Generic;

namespace Parser
{
    public class Document
    {
        public Document(ICollection<Sentence> sentences)
        {
            Sentences = sentences;
        }

        public ICollection<Sentence> Sentences { get; private set; }

    }
}
