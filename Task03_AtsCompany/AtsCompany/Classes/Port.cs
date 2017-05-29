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

        public event Action PortRemoved;

        public delegate void PortEnabledHandler(object sender);

        public event PortEnabledHandler PortEnabled;

        public delegate void PortDisabledHandler(object sender);

        public event PortDisabledHandler PortDisabled;

        public event EventHandler<PhoneNumberArgs> PortStateSetToActive;

        public delegate void PortEndCallHandler(int initializatorNumber);

        public event PortEndCallHandler PortEndCall;

        public delegate void PortCallFinishedHandler(string message);

        public event PortCallFinishedHandler PortFinishedCall;

        public delegate void TerminalContactHandler(object sender, string message);

        public event TerminalContactHandler UserIsUnavaliable;
        public event TerminalContactHandler UserIsBusy;
        public event TerminalContactHandler UserDoesntExists;

        public delegate void CallRequestHandler(int number1, int number2, string message);

        public event CallRequestHandler CallRequesting;

        public delegate void PortCallStateHandler(int number1, int number2, string message);

        public event PortCallStateHandler CallRejected;
        public event PortCallStateHandler CallAccepted;

        public delegate void PortStateCallingHandler(object sender1, object sender2);

        public event PortStateCallingHandler PortConnectionEstablished;

        public delegate void ServerAcceptHandler(int number1, int number2, string message);

        public event ServerAcceptHandler SendAcceptMessageToTerminal;

        public delegate void ServerRejectHandler(int number, string message);

        public event ServerRejectHandler SendRejectMessageToTerminal;

        public Terminal Terminal { get; private set; }
        public int Number { get; }
        public PortState State { get; private set; }
        public AtsServer Server { get; private set; }

        public void SetCurrentTerminal(Terminal terminal)
        {
            Terminal = terminal;
            SubscribeOnAllTerminalEvents();
        }

        public void RemovePortFromTerminal(Terminal terminal)
        {
            UnsubscribeOnAllTerminalEvents();
            PortRemoved?.Invoke();
        }

        private void TerminalOnTerminalIsEnabled(object sender)
        {
            State = PortState.Enabled;
            PortEnabled?.Invoke(this);
        }

        private void TerminalOnTerminalIsDisabled(object sender)
        {
            State = PortState.Disabled;
            PortDisabled?.Invoke(this);
        }

        private void TerminalOnBeginCall(object sender, PhoneNumberArgs e)
        {
            State = PortState.Active;
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

        private void TerminalOnCallIsEnd(int senderNumber)
        {
            PortEndCall?.Invoke(senderNumber);
        }

        private void ServerOnServerFinishedCall(Port port1, Port port2, string message)
        {
            if (this == port1 || this == port2)
            {
                State = PortState.Enabled;
                PortEnabled?.Invoke(this);
                PortFinishedCall?.Invoke(message);
            }
        }

        private void ServerOnUserIsUnavaliable(object sender, string message)
        {
            if (sender as Port != this) return;
            State = PortState.Enabled;
            UserIsUnavaliable?.Invoke(sender, message);
            PortEnabled?.Invoke(this);
        }

        private void ServerOnUserIsBusy(object sender, string message)
        {
            if (sender as Port != this) return;
            State = PortState.Enabled;
            UserIsBusy?.Invoke(sender, message);
            PortEnabled?.Invoke(this);
        }

        private void ServerOnUserDoesntExists(object sender, string message)
        {
            if (sender as Port != this) return;
            State = PortState.Enabled;
            UserDoesntExists?.Invoke(sender, message);
            PortEnabled?.Invoke(this);
        }

        private void ServerOnConnectionEstablish(Port port1, Port port2)
        {
            if (this == port2)
            {
                CallRequesting?.Invoke(port1.Number, this.Number, $"{port1.Number} requesting connection");
            }
        }

        private void TerminalOnAcceptCall(int number1, int number2, string message)
        {
            CallAccepted?.Invoke(number1, number2, message);
        }

        private void TerminalOnRejectCall(int number1, int number2, string message)
        {
            CallRejected?.Invoke(number1, number2, message);
        }

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
                PortEnabled?.Invoke(this);
            }
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

        private void SubscribeOnAllServerEvents()
        {
            Server.UserDoesntExists += ServerOnUserDoesntExists;
            Server.UserIsBusy += ServerOnUserIsBusy;
            Server.UserIsUnavaliable += ServerOnUserIsUnavaliable;
            Server.ConnectionEstablish += ServerOnConnectionEstablish;
            Server.AnswerOnReject += OnAnswerOnReject;
            Server.AnswerOnAccept += ServerOnAnswerOnAccept;
            Server.ServerFinishedCall += ServerOnServerFinishedCall;
        }
    }
}
