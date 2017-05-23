namespace AtsCompany.Classes
{
    public class Port
    {
        public Port(int number, Terminal terminal, AtsServer server)
        {
            Number = number;
            State = PortState.Disabled;
            Terminal = terminal;
            Server = server;
        }

        public Terminal Terminal { get; }
        public int Number { get; }
        public PortState State { get; private set; }
        public AtsServer Server { get; private set; }
    }
}
