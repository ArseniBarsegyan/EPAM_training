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
            SubscribeOnAllTerminalEvents();
        }

        public Terminal Terminal { get; }
        public int Number { get; }
        public PortState State { get; private set; }
        public AtsServer Server { get; private set; }

        private void TerminalOnTerminalIsEnabled(object sender)
        {
            State = PortState.Enabled;
            OnPortEnabled();
        }

        public delegate void PortEnabledHandler(object sender);

        public event PortEnabledHandler PortEnabled;

        protected virtual void OnPortEnabled()
        {
            PortEnabled?.Invoke(this);
        }

        private void TerminalOnTerminalIsDisabled(object sender)
        {
            State = PortState.Disabled;
            OnPortDisabled();
        }

        public delegate void PortDisabledHandler(object sender);

        public event PortDisabledHandler PortDisabled;

        protected virtual void OnPortDisabled()
        {
            PortDisabled?.Invoke(this);
        }

        private void SubscribeOnAllTerminalEvents()
        {
            Terminal.TerminalIsEnabled += TerminalOnTerminalIsEnabled;
            Terminal.TerminalIsDisabled += TerminalOnTerminalIsDisabled;
        }
    }
}
