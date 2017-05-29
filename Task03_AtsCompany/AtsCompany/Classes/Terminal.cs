using System;

namespace AtsCompany.Classes
{
    public class Terminal
    {
        private string _userAnswer;

        public Terminal(Port port)
        {
            Port = port;
            Number = port.Number;
            SubscribeOnAllPortEvents();
        }
        
        public void SetCurrentPort(Port port)
        {
            Port = port;
            SubscribeOnAllPortEvents();
        }

        public event EventHandler TerminalIsEnabled;
        public event EventHandler TerminalIsDisabled;
        public event EventHandler<CallEventArgs> BeginCall;
        public event EventHandler<CallEventArgs> CallIsEnd;
        public event EventHandler<string> TerminalSendMessage;
        public event EventHandler<string> TerminalRequiredAnswer;
        public event EventHandler<AnswerEventArgs> RejectCall;
        public event EventHandler<AnswerEventArgs> AcceptCall;

        public int Number { get; private set; }
        public Port Port { get; private set; }

        //Following methods invoke events

        public void TurnOnTerminal()
        {
            TerminalIsEnabled?.Invoke(this, EventArgs.Empty);
        }

        public void TurnOffTerminal()
        {
            TerminalIsDisabled?.Invoke(this, EventArgs.Empty);
        }

        public void EndCall()
        {
            CallIsEnd?.Invoke(this, new CallEventArgs(Number));
        }

        public void Call(int number)
        {
            BeginCall?.Invoke(this, new CallEventArgs(number));
        }
        
        public void Answer(string message)
        {
            _userAnswer = message;
        }

        //methods-handlers of port events

        protected virtual void PortOnCallRequesting(object sender, AnswerEventArgs e)
        {
            TerminalRequiredAnswer?.Invoke(this, "Incoming call. Type 'y' to answer, 'n' to reject");
            if (_userAnswer != null && _userAnswer.ToLower().Equals("y"))
            {
                AcceptCall?.Invoke(this, new AnswerEventArgs(e.number1, e.number2, $"User 2 : Connection with {e.number1} established"));
            }
            else
            {
                RejectCall?.Invoke(this, new AnswerEventArgs(e.number1, e.number2, $"{e.number2} has rejected call"));
            }
        }

        protected virtual void PortOnSendAcceptMessageToTerminal(object sender, AnswerEventArgs e)
        {
            if (Number == e.number1)
            {
                TerminalSendMessage?.Invoke(this, e.message);
            }
        }

        protected virtual void PortOnSendRejectMessageToTerminal(object sender, AnswerEventArgs e)
        {
            if (Number == e.number2)
            {
                TerminalSendMessage?.Invoke(this, e.message);
            }
        }

        protected virtual void PortOnUserIsUnavaliable(object sender, string message)
        {
            var port = sender as Port;
            if (port != null && port.Number == Number)
            {
                TerminalSendMessage?.Invoke(this, message);
            }
        }

        protected virtual void PortOnUserIsBusy(object sender, string message)
        {
            var port = sender as Port;
            if (port != null && port.Number == Number)
            {
                TerminalSendMessage?.Invoke(this, message);
            }
        }

        protected virtual void PortOnUserDoesntExists(object sender, string message)
        {
            var port = sender as Port;
            if (port != null && port.Number == Number)
            {
                TerminalSendMessage?.Invoke(this, message);
            }
        }

        protected virtual void PortOnPortFinishedCall(object sender, string message)
        {
            TerminalSendMessage?.Invoke(this, message);
        }

        private void SubscribeOnAllPortEvents()
        {
            Port.UserIsUnavaliable += PortOnUserIsUnavaliable;
            Port.UserIsBusy += PortOnUserIsBusy;
            Port.UserDoesntExists += PortOnUserDoesntExists;
            Port.CallRequesting += PortOnCallRequesting;
            Port.SendRejectMessageToTerminal += PortOnSendRejectMessageToTerminal;
            Port.SendAcceptMessageToTerminal += PortOnSendAcceptMessageToTerminal;
            Port.PortFinishedCall += PortOnPortFinishedCall;
            Port.PortRemoved += PortOnPortRemoved;
        }

        private void PortOnPortRemoved()
        {
            Port.UserIsUnavaliable -= PortOnUserIsUnavaliable;
            Port.UserIsBusy -= PortOnUserIsBusy;
            Port.UserDoesntExists -= PortOnUserDoesntExists;
            Port.CallRequesting -= PortOnCallRequesting;
            Port.SendRejectMessageToTerminal -= PortOnSendRejectMessageToTerminal;
            Port.SendAcceptMessageToTerminal -= PortOnSendAcceptMessageToTerminal;
            Port.PortFinishedCall -= PortOnPortFinishedCall;
        }
    }
}
