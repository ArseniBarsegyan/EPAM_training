using System;
using System.Collections.Generic;
using System.Linq;
using AtsCompany.Classes;
using BillingSystem.Interfaces;

namespace BillingSystem.Classes
{
    public class UserAccount
    {
        public UserAccount(string name, IRate currentRate, ICollection<Terminal> terminals, AtsManager manager)
        {
            Name = name;
            CurrentRate = currentRate;
            Terminals = terminals;
            RegistrationTime = DateTime.Now;
            ChangeRateTime = RegistrationTime;
            FreeMinutes = currentRate.FreeMinutes;
            Manager = manager;
        }

        private double Balance { get; set; }
        public DateTime ChangeRateTime { get; private set; }
        public DateTime RegistrationTime { get; private set; }
        public string Name { get; }
        public IRate CurrentRate { get; private set; }
        public ICollection<Terminal> Terminals { get; }
        public AtsManager Manager { get; private set; }
        public int FreeMinutes { get; private set; }

        public void SpendFreeMinute()
        {
            FreeMinutes -= 1;
        }

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
