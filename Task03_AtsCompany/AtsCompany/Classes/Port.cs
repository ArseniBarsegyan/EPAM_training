using System;

namespace AtsCompany.Classes
{
    public class Port
    {
        public Port(int number, AtsServer server)
        {
            Number = number;
            State = PortState.Disabled;
            Server = server;
            SubscribeOnAllServerEvents();
        }

        public void SetCurrentTerminal(Terminal terminal)
        {
            Terminal = terminal;
            SubscribeOnAllTerminalEvents();
        }

        public void ReplaceCurrentTerminalWithNewTerminal(Terminal terminal)
        {
            UnsubscribeOnAllTerminalEvents();
            Terminal = terminal;
            SubscribeOnAllTerminalEvents();
        }

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

        private void UnsubscribeOnAllTerminalEvents()
        {
            Terminal.BeginCall -= TerminalOnBeginCall;
            Terminal.TerminalIsEnabled -= TerminalOnTerminalIsEnabled;
            Terminal.TerminalIsDisabled -= TerminalOnTerminalIsDisabled;
        }

        public void SubscribeOnAllServerEvents()
        {
            Server.UserDoesntExists += ServerOnUserDoesntExists;
            Server.UserIsBusy += ServerOnUserIsBusy;
            Server.UserIsUnavaliable += ServerOnUserIsUnavaliable;
        }

        public delegate void TerminalContactHandler(object sender, string message);

        public event TerminalContactHandler UserIsUnavaliable;
        public event TerminalContactHandler UserIsBusy;
        public event TerminalContactHandler UserDoesntExists;

        private void ServerOnUserIsUnavaliable(object sender, string message)
        {
            if (sender as Port != this) return;
            State = PortState.Enabled;
            UserIsUnavaliable?.Invoke(sender, message);
            OnPortEnabled();
        }

        private void ServerOnUserIsBusy(object sender, string message)
        {
            if (sender as Port != this) return;
            State = PortState.Enabled;
            UserIsBusy?.Invoke(sender, message);
            OnPortEnabled();
        }

        private void ServerOnUserDoesntExists(object sender, string message)
        {
            if (sender as Port != this) return;
            State = PortState.Enabled;
            UserDoesntExists?.Invoke(sender, message);
            OnPortEnabled();
        }

        public Terminal Terminal { get; private set; }
        public int Number { get; }
        public PortState State { get; private set; }
        public AtsServer Server { get; private set; }
    }
}
