﻿using System;

namespace AtsCompany.Classes
{
    public class Terminal
    {
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

        public delegate void TerminalMessageHandler(string message);

        public event TerminalMessageHandler TerminalSendMessage;

        private void PortOnUserIsUnavaliable(object sender, string message)
        {
            var port = sender as Port;
            if (port != null && port.Number == Number)
            {
                TerminalSendMessage?.Invoke(message);
                //Console.WriteLine(message);
            }
        }

        private void PortOnUserIsBusy(object sender, string message)
        {
            var port = sender as Port;
            if (port != null && port.Number == Number)
            {
                TerminalSendMessage?.Invoke(message);
                //Console.WriteLine(message);
            }
        }

        private void PortOnUserDoesntExists(object sender, string message)
        {
            var port = sender as Port;
            if (port != null && port.Number == Number)
            {
                TerminalSendMessage?.Invoke(message);
                //Console.WriteLine(message);
            }
        }

        public event EventHandler<PhoneNumberArgs> BeginCall;

        public void MakeCall(int number)
        {
            BeginCall?.Invoke(this, new PhoneNumberArgs(number));
        }
        
        public delegate void TerminalEnableHandler(object sender);

        public event TerminalEnableHandler TerminalIsEnabled;

        public void TurnOnTerminal()
        {
            TerminalIsEnabled?.Invoke(this);
        }
        
        public delegate void TerminalDisableHandler(object sender);

        public event TerminalDisableHandler TerminalIsDisabled;

        public void TurnOffTerminal()
        {
            TerminalIsDisabled?.Invoke(this);
        }

        public int Number { get; private set; }
        public Port Port { get; private set; }
        
        public delegate void AnswerHandler(int number1, int number2, string message);

        public event AnswerHandler RejectCall;
        public event AnswerHandler AcceptCall;

        public void Answer(string message)
        {
            _userAnswer = message;
        }

        private string _userAnswer;

        public delegate void TerminalRequiredAnswerHandler(int number, string message);

        public event TerminalRequiredAnswerHandler TerminalRequiredAnswer;

        private void PortOnCallRequesting(int number1, int number2, string message)
        {
            TerminalRequiredAnswer?.Invoke(number2, "Incoming call. Type 'y' to answer, 'n' to reject");
            if (_userAnswer != null && _userAnswer.ToLower().Equals("y"))
            {
                AcceptCall?.Invoke(number1, number2, $"User 2 : Connection with {number1} established");
            }
            else
            {
                RejectCall?.Invoke(number1, number2, $"{number2} has rejected call");
            }
        }

        public delegate void TerminalCallIsEndHandler(int senderNumber);
        public event TerminalCallIsEndHandler CallIsEnd;

        public void EndCall()
        {
            CallIsEnd?.Invoke(Number);
        }

        private void PortOnSendAcceptMessageToTerminal(int number1, int number2, string message)
        {
            if (Number == number1)
            {
                TerminalSendMessage?.Invoke(message);
                //Console.WriteLine(message);
            }
        }

        private void PortOnSendRejectMessageToTerminal(int number, string message)
        {
            if (Number == number)
            {
                TerminalSendMessage?.Invoke(message);
                //Console.WriteLine(message);
            }
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

        private void PortOnPortFinishedCall(string message)
        {
            TerminalSendMessage?.Invoke(message);
            //Console.WriteLine(message);
        }
    }
}
