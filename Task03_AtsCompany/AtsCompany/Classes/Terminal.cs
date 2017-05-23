namespace AtsCompany.Classes
{
    public class Terminal
    {
        public Terminal(int number, AtsServer server)
        {
            Number = number;
            Server = server;
        }

        public AtsServer Server { get; private set; }
        public int Number { get; private set; }
    }
}
