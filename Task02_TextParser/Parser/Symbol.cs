namespace Parser
{
    public class Symbol
    {
        public Symbol(char value)
        {
            Value = value;
        }

        public char Value { get; private set; }
    }
}
