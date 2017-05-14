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

        private ICollection<Symbol> Symbols { get; }

        public void AddSymbol(char c)
        {
            Symbols.Add(new Symbol(c));
        }

        public string Value => ToString();

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
