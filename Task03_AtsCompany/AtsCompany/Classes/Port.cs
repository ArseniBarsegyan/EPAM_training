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
        public event EventHandler PortEnabled;
        public event EventHandler PortDisabled;
        public event EventHandler<CallEventArgs> PortStateSetToActive;
        public event EventHandler<CallEventArgs> PortEndCall;
        public event EventHandler<string> PortFinishedCall;

        public event EventHandler<string> UserIsUnavaliable;
        public event EventHandler<string> UserIsBusy;
        public event EventHandler<string> UserDoesntExists;

        public event EventHandler<AnswerEventArgs> CallRequesting;
        public event EventHandler<AnswerEventArgs> CallRejected;
        public event EventHandler<AnswerEventArgs> CallAccepted;

        public event EventHandler<AnswerEventArgs> SendAcceptMessageToTerminal;
        public event EventHandler<ConnectionEstablishedEventArgs> PortConnectionEstablished;
        public event EventHandler<AnswerEventArgs> SendRejectMessageToTerminal;

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

        //Methods-handlers of terminal events

        protected virtual void TerminalOnTerminalIsEnabled(object sender, EventArgs e)
        {
            State = PortState.Enabled;
            PortEnabled?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void TerminalOnTerminalIsDisabled(object sender, EventArgs e)
        {
            State = PortState.Disabled;
            PortDisabled?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void TerminalOnBeginCall(object sender, CallEventArgs e)
        {
            State = PortState.Active;
            PortStateSetToActive?.Invoke(this, e);
        }

        protected virtual void TerminalOnCallIsEnd(object sender, CallEventArgs e)
        {
            PortEndCall?.Invoke(this, new CallEventArgs(e.number));
        }

        //Methods-handlers of Server events

        protected virtual void ServerOnUserIsUnavaliable(object sender, string message)
        {
            if (sender as Port != this) return;
            State = PortState.Enabled;
            UserIsUnavaliable?.Invoke(sender, message);
            PortEnabled?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void ServerOnUserIsBusy(object sender, string message)
        {
            if (sender as Port != this) return;
            State = PortState.Enabled;
            UserIsBusy?.Invoke(sender, message);
            PortEnabled?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void ServerOnUserDoesntExists(object sender, string message)
        {
            if (sender as Port != this) return;
            State = PortState.Enabled;
            UserDoesntExists?.Invoke(sender, message);
            PortEnabled?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void ServerOnEstablishConnection(object sender, ConnectionEventArgs e)
        {
            if (this == e.port2)
            {
                CallRequesting?.Invoke(this, new AnswerEventArgs(e.port1.Number, e.port2.Number, $"{e.port1.Number} requesting connection"));
            }
        }

        protected virtual void TerminalOnAcceptCall(object sender, AnswerEventArgs e)
        {
            CallAccepted?.Invoke(this, e);
        }

        protected virtual void TerminalOnRejectCall(object sender, AnswerEventArgs e)
        {
            CallRejected?.Invoke(this, e);
        }

        protected virtual void ServerOnAnswerOnAccept(object sender, ConnectionEventArgs e)
        {
            if (this == e.port1)
            {
                State = PortState.Calling;
                if (e.port2 != null)
                {
                    PortConnectionEstablished?.Invoke(this, new ConnectionEstablishedEventArgs(e.port1, e.port2));
                    SendAcceptMessageToTerminal?.Invoke(this, new AnswerEventArgs(e.port1.Number, e.port2.Number, e.message));
                }
            }

            if (this == e.port2)
            {
                State = PortState.Calling;
            }
        }

        protected virtual void ServerOnAnswerOnReject(object sender, string message)
        {
            if (this != sender) return;
            var port1 = sender as Port;
            if (port1 != null)
            {
                SendRejectMessageToTerminal?.Invoke(this, new AnswerEventArgs(port1.Number, message));
            }
            State = PortState.Enabled;
            PortEnabled?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void ServerOnServerFinishedCall(object sender, ConnectionEventArgs e)
        {
            if (this != e.port1 && this != e.port2) return;
            State = PortState.Enabled;
            PortEnabled?.Invoke(this, EventArgs.Empty);
            PortFinishedCall?.Invoke(this, e.message);
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
            Server.EstablishConnection += ServerOnEstablishConnection;
            Server.AnswerOnReject += ServerOnAnswerOnReject;
            Server.AnswerOnAccept += ServerOnAnswerOnAccept;
            Server.ServerFinishedCall += ServerOnServerFinishedCall;
        }
    }
}
