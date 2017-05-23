using System;

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
            SubscribeOnAllServerEvents();
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

        private void TerminalOnBeginCall(object sender, PhoneNumberArgs e)
        {
            State = PortState.Active;
            OnPortStateSetToActive(e);
        }

        public event EventHandler<PhoneNumberArgs> PortStateSetToActive;

        private void OnPortStateSetToActive(PhoneNumberArgs e)
        {
            PortStateSetToActive?.Invoke(this, e);
        }

        private void SubscribeOnAllTerminalEvents()
        {
            Terminal.BeginCall += TerminalOnBeginCall;
            Terminal.TerminalIsEnabled += TerminalOnTerminalIsEnabled;
            Terminal.TerminalIsDisabled += TerminalOnTerminalIsDisabled;
        }

        public void SubscribeOnAllServerEvents()
        {
            Server.UserDoesntExists += ServerOnUserDoesntExists;
            Server.UserIsBusy += ServerOnUserIsBusy;
            Server.UserIsUnavaliable += ServerOnUserIsUnavaliable;
        }

        private void ServerOnUserIsUnavaliable(object sender, string message)
        {
            if (sender as Port != this) return;
            State = PortState.Enabled;
            OnPortEnabled();
        }

        private void ServerOnUserIsBusy(object sender, string message)
        {
            if (sender as Port != this) return;
            State = PortState.Enabled;
            OnPortEnabled();
        }

        private void ServerOnUserDoesntExists(object sender, string message)
        {
            if (sender as Port != this) return;
            State = PortState.Enabled;
            OnPortEnabled();
        }
    }
}
