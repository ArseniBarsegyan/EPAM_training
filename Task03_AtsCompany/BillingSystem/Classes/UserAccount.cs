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
            Balance = 50;
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

        //User can pay his debt
        public void Deposit(int sum)
        {
            Balance += sum;
        }
        
        public void WithDraw(int sum)
        {
            Balance -= sum;
        }

        //User can change rate once in month
        public void ChangeRate(IRate rate)
        {
            if (IsOneMonthExpired())
            {
                CurrentRate = rate;
                ChangeRateTime = DateTime.Now;
                Console.WriteLine("Rate has been changed succesfully");
            }
            else
            {
                Console.WriteLine("You can change rate only 1 time in month");
            }            
        }

        private bool IsOneMonthExpired()
        {
            var timeFromChangeRate = DateTime.Now - RegistrationTime;
            var expiredDays = timeFromChangeRate.TotalDays;
            if (DateTime.DaysInMonth(ChangeRateTime.Year, ChangeRateTime.Month) == 31)
            {
                if (expiredDays >= 31)
                    return true;
            }
            else
            {
                if (expiredDays >= 30)
                    return true;
            }
            return false;
        }
    }
}
