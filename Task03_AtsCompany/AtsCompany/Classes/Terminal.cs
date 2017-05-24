using System;

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

        private void PortOnUserIsUnavaliable(object sender, string message)
        {
            var port = sender as Port;
            if (port != null && port.Number == Number)
            {
                Console.WriteLine(message);
            }
        }

        private void PortOnUserIsBusy(object sender, string message)
        {
            var port = sender as Port;
            if (port != null && port.Number == Number)
            {
                Console.WriteLine(message);
            }
        }

        private void PortOnUserDoesntExists(object sender, string message)
        {
            var port = sender as Port;
            if (port != null && port.Number == Number)
            {
                Console.WriteLine(message);
            }
        }

        public event EventHandler<PhoneNumberArgs> BeginCall;

        public void MakeCall(int number)
        {
            BeginCall?.Invoke(this, new PhoneNumberArgs(number));
        }

        //Event happens when terminal turn on (state set to enabled)
        public delegate void TerminalEnableHandler(object sender);

        public event TerminalEnableHandler TerminalIsEnabled;

        public void TurnOnTerminal()
        {
            TerminalIsEnabled?.Invoke(this);
        }

        //Event happens when terminal turn off (state set to disabled)
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

        private void PortOnCallRequesting(int number1, int number2, string message)
        {
            Console.WriteLine("Incoming call. Type 'y' to answer, 'n' to reject");
            var answer = Console.ReadLine();
            if (answer != null && answer.ToLower().Equals("y"))
            {
                AcceptCall?.Invoke(number1, number2, $"User 2 : Connection with {number1} established");
            }
            else
            {
                RejectCall?.Invoke(number1, number2, $"{number2} has rejected call");
            }
        }

        private void PortOnSendAcceptMessageToTerminal(int number1, int number2, string message)
        {
            if (Number == number1)
            {
                Console.WriteLine(message);
            }
        }

        private void PortOnSendRejectMessageToTerminal(int number, string message)
        {
            if (Number == number)
            {
                Console.WriteLine(message);
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
        }

        private void PortOnPortRemovedTerminal()
        {
            Port.UserIsUnavaliable -= PortOnUserIsUnavaliable;
            Port.UserIsBusy -= PortOnUserIsBusy;
            Port.UserDoesntExists -= PortOnUserDoesntExists;
            Port.CallRequesting -= PortOnCallRequesting;
            Port.SendRejectMessageToTerminal -= PortOnSendRejectMessageToTerminal;
            Port.SendAcceptMessageToTerminal -= PortOnSendAcceptMessageToTerminal;
        }
    }
}
