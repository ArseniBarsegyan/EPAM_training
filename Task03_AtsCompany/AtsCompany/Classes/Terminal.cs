using System;

namespace AtsCompany.Classes
{
    public class Terminal
    {
        public Terminal(int number, AtsServer server)
        {
            Number = number;
            Server = server;
        }

        public AtsServer Server { get; private set; }
        public int Number { get; private set; }

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
    }
}
