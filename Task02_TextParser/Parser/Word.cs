using System.Collections.Generic;
using System.Text;

namespace Parser
{
    public class Word : ISentenceItem
    {
        public Word(ICollection<Symbol> symbols)
        {
            Symbols = symbols;
        }

        public ICollection<Symbol> Symbols { get; private set; }

        public void AddSymbol(char c)
        {
            Symbols.Add(new Symbol(c));
        }

        public string Value { get { return ToString(); } }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (var symbol in Symbols)
            {
                builder.Append(symbol.Value);
            }
            return builder.ToString();
        }
    }
}
