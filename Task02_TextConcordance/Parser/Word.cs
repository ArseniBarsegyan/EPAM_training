using System.Collections.Generic;

namespace Parser
{
    //This class will be use in concordance
    public class Word
    {
        public Word(string value, int repeatCount, int numberOfPage, ICollection<int> pageNumbers)
        {
            Value = value;
            RepeatCount = repeatCount;
            PagesNumbers = pageNumbers;
            PagesNumbers.Add(numberOfPage);
        }

        public string Value { get; private set; }
        public int RepeatCount { get; private set; }
        public ICollection<int> PagesNumbers { get; }

        public void IncreaseRepeatCount()
        {
            RepeatCount++;
        }
    }
}
