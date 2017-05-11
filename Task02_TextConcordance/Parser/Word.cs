using System.Collections.Generic;

namespace Parser
{
    //This class will be use in concordance
    public class Word
    {
        public Word(string value, int repeatCount, int numberOfPage)
        {
            Value = value;
            RepeatCount = repeatCount;
            PagesNumbers = new List<int>();
            PagesNumbers.Add(numberOfPage);
        }

        public string Value { get; private set; }
        public int RepeatCount { get; set; }
        public List<int> PagesNumbers { get; private set; }
    }
}
