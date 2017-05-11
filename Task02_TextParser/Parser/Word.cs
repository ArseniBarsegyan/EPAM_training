using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Parser
{
    public class Word : ISentenceItem
    {
        public Word(ICollection<Symbol> symbols)
        {
            Symbols = symbols;
        }

        public ICollection<Symbol> Symbols { get; private set; }

        public override string ToString()
        {
            return Symbols.Aggregate(string.Empty, (current, s) => current + s.Value);
        }

        public void AddSymbol(char c)
        {
            Symbols.Add(new Symbol(c));
        }

        public string Value { get { return ToString(); } }

        public bool IsWordStartWithConsonantLetter()
        {
            return new Regex(@"\b[b-d,f-h,j-n,p-t,v,w,x,z]\S+\b").IsMatch(Value);
        }
    }
}
