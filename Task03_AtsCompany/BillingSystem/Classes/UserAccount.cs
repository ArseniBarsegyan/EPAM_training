﻿using System;
using System.Collections.Generic;
using System.Linq;
using AtsCompany.Classes;
using BillingSystem.Interfaces;

namespace BillingSystem.Classes
{
    public class UserAccount
    {
        public UserAccount(string name, IRate currentRate, AtsManager manager, PayServiceManager serviceManager)
        {
            Name = name;
            CurrentRate = currentRate;
            Terminals = new List<Terminal>();
            RegistrationTime = DateTime.Now;
            ChangeRateTime = RegistrationTime;
            FreeMinutes = currentRate.FreeMinutes;
            ServiceManager = serviceManager;
            Manager = manager;
            Balance = 1;
        }

        public event EventHandler<UserEventArgs> MoneyAdded;

        public int Balance { get; private set; }
        public DateTime ChangeRateTime { get; private set; }
        public DateTime RegistrationTime { get; private set; }
        public string Name { get; }
        public IRate CurrentRate { get; private set; }
        public ICollection<Terminal> Terminals { get; }
        public AtsManager Manager { get; private set; }
        public PayServiceManager ServiceManager { get; private set; }
        public int FreeMinutes { get; private set; }

        public void SpendFreeMinute()
        {
            FreeMinutes -= 1;
        }

        public void AddTerminal(Terminal terminal)
        {
            terminal.TerminalRequiredAnswer += TerminalOnTerminalRequiredAnswer;
            Terminals.Add(terminal);
        }

        protected virtual void TerminalOnTerminalRequiredAnswer(object sender, string message)
        {
            var terminal = sender as Terminal;
            if (terminal == null) return;
            Console.WriteLine(message);
            var userTerminal = Terminals.FirstOrDefault(x => x.Number == terminal.Number);
            terminal?.Answer(Console.ReadLine());
        }

        public void CreateNewTerminal()
        {
            Manager.CreateTerminalForUser(this);
        }

        public void Call(Terminal fromTerminal, int number)
        {
            var terminal = Terminals.FirstOrDefault(x => x.Equals(fromTerminal));
            if (terminal == null) return;
            terminal.TerminalSendMessage += TerminalOnTerminalSendMessage;
            terminal?.Call(number);
            terminal.TerminalSendMessage -= TerminalOnTerminalSendMessage;
        }

        protected virtual void TerminalOnTerminalSendMessage(object sender, string message)
        {
            Console.WriteLine(message);
        }

        public void EndCall(Terminal terminal)
        {
            terminal.TerminalSendMessage += TerminalOnTerminalSendMessage;
            terminal.EndCall();
            terminal.TerminalSendMessage -= TerminalOnTerminalSendMessage;
        }

        public void TurnOnTerminal(Terminal terminal)
        {
            terminal.TurnOnTerminal();
        }

        public void TurnOffTerminal(Terminal terminal)
        {
            terminal.TurnOffTerminal();
        }

        public void Deposit(int sum)
        {
            Balance += sum;
            MoneyAdded?.Invoke(this, new UserEventArgs(Balance));
        }
        
        public void WithDraw(int sum)
        {
            Balance -= sum;
        }
        
        public IEnumerable<CallInfo> OrderCallInfos()
        {
            return ServiceManager.OrderUserHistory(this);
        }

        public IEnumerable<CallInfo> GetHistoryOrderedByDuration()
        {
            return OrderCallInfos().OrderByDescending(x => x.Duration);
        }

        public IEnumerable<CallInfo> GetHistoryOrderedByPrice()
        {
            return OrderCallInfos().OrderByDescending(x => x.Price);
        }

        public IEnumerable<CallInfo> GetHistoryOrderedByOutComingCalls()
        {
            return OrderCallInfos().OrderByDescending(x => x.RecieverNumber);
        }
        
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
