namespace Parser
{
    public class SentenceSeparator : ISentenceItem
    {
        public SentenceSeparator(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}
