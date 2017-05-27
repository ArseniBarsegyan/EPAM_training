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
            Terminal.RejectCall += TerminalOnRejectCall;
            Terminal.AcceptCall += TerminalOnAcceptCall;
            Terminal.CallIsEnd += TerminalOnCallIsEnd;
        }

        public delegate void PortEndCallHandler(int initializatorNumber);
        public event PortEndCallHandler PortEndCall;

        private void TerminalOnCallIsEnd(int senderNumber)
        {
            PortEndCall?.Invoke(senderNumber);
        }

        private void UnsubscribeOnAllTerminalEvents()
        {
            Terminal.BeginCall -= TerminalOnBeginCall;
            Terminal.TerminalIsEnabled -= TerminalOnTerminalIsEnabled;
            Terminal.TerminalIsDisabled -= TerminalOnTerminalIsDisabled;
            Terminal.RejectCall -= TerminalOnRejectCall;
            Terminal.AcceptCall -= TerminalOnAcceptCall;
            Terminal.CallIsEnd -= TerminalOnCallIsEnd;
        }

        public void SubscribeOnAllServerEvents()
        {
            Server.UserDoesntExists += ServerOnUserDoesntExists;
            Server.UserIsBusy += ServerOnUserIsBusy;
            Server.UserIsUnavaliable += ServerOnUserIsUnavaliable;
            Server.ConnectionEstablish += ServerOnConnectionEstablish;
            Server.AnswerOnReject += OnAnswerOnReject;
            Server.AnswerOnAccept += ServerOnAnswerOnAccept;
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

        public delegate void CallRequestHandler(int number1, int number2, string message);

        public event CallRequestHandler CallRequesting;

        private void ServerOnConnectionEstablish(Port port1, Port port2)
        {
            if (this == port2)
            {
                CallRequesting?.Invoke(port1.Number, this.Number, $"{port1.Number} requesting connection");
            }
        }

        public delegate void PortCallStateHandler(int number1, int number2, string message);

        public event PortCallStateHandler CallRejected;
        public event PortCallStateHandler CallAccepted;

        private void TerminalOnAcceptCall(int number1, int number2, string message)
        {
            CallAccepted?.Invoke(number1, number2, message);
        }

        private void TerminalOnRejectCall(int number1, int number2, string message)
        {
            CallRejected?.Invoke(number1, number2, message);
        }

        public delegate void PortStateCallingHandler(object sender1, object sender2);

        public event PortStateCallingHandler PortConnectionEstablished;

        public delegate void ServerAcceptHandler(int number1, int number2, string message);

        public event ServerAcceptHandler SendAcceptMessageToTerminal;

        private void ServerOnAnswerOnAccept(object sender1, object sender2, string message)
        {
            var port1 = sender1 as Port;
            var port2 = sender2 as Port;

            if (this == port1)
            {
                State = PortState.Calling;
                if (port2 != null)
                {
                    PortConnectionEstablished?.Invoke(sender1, sender2);
                    SendAcceptMessageToTerminal?.Invoke(port1.Number, port2.Number, message);
                }
            }

            if (this == port2)
            {
                State = PortState.Calling;
            }
        }

        public delegate void ServerRejectHandler(int number, string message);

        public event ServerRejectHandler SendRejectMessageToTerminal;

        private void OnAnswerOnReject(object sender, string message)
        {
            if (this == sender)
            {
                var port1 = sender as Port;
                if (port1 != null)
                {
                    SendRejectMessageToTerminal?.Invoke(port1.Number, message);
                }
                State = PortState.Enabled;
                OnPortEnabled();
            }
        }

        public Terminal Terminal { get; private set; }
        public int Number { get; }
        public PortState State { get; private set; }
        public AtsServer Server { get; private set; }
    }
}
