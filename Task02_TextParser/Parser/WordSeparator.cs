namespace Parser
{
    public class WordSeparator : ISentenceItem
    {
        public WordSeparator(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }
    }
}
