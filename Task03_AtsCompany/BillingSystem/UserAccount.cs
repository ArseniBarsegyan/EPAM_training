using System;
using System.Collections.Generic;
using System.Linq;
using AtsCompany.Classes;

namespace BillingSystem
{
    public class UserAccount
    {
        public UserAccount(string name, IRate currentRate, ICollection<Terminal> terminals, AtsManager manager)
        {
            Name = name;
            CurrentRate = currentRate;
            PreviousRate = CurrentRate;
            Terminals = terminals;
            RegistrationTime = DateTime.Now;
            ChangeRateTime = RegistrationTime;
            Manager = manager;
        }

        private double Balance { get; set; }
        public DateTime ChangeRateTime { get; private set; }
        public DateTime RegistrationTime { get; private set; }
        public string Name { get; }
        public IRate PreviousRate { get; private set; }
        public IRate CurrentRate { get; private set; }
        public ICollection<Terminal> Terminals { get; }
        public AtsManager Manager { get; private set; }

        public void AddNewTerminal()
        {
            Manager.CreateTerminalForUser(this);
        }

        public void MakeCall(Terminal fromTerminal, int number)
        {
            var terminal = Terminals.FirstOrDefault(x => x.Equals(fromTerminal));
            terminal?.MakeCall(number);
        }

        public void EndCall(Terminal terminal)
        {
            terminal.EndCall();
        }

        public void TurnOnTerminal(Terminal terminal)
        {
            terminal.TurnOnTerminal();
        }

        public void TurnOffTerminal(Terminal terminal)
        {
            terminal.TurnOffTerminal();
        }
    }
}
