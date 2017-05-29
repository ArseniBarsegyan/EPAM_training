using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AtsCompany.Classes;
using BillingSystem.Classes;
using BillingSystem.Interfaces;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            StartDemo();
        }

        static void StartDemo()
        {
            //Create AtsServer, AtsManager and AtsPayService
            var server = new AtsServer("Company server", new List<Port>());
            var manager = new AtsManager("Company manager system", server);
            var payService = new PayServiceManager(manager, server);

            //Creating accounts
            var user = CreateAccount("user", manager, payService);
            var user2 = CreateAccount("user2", manager, payService);
            
            //random call
            Call(user, 1111111);

            //turn on terminal #2
            TurnOnTerminal(user2);
            
            //call user - user2
            Call(user, user2);
            Call(user2, user);
            
            //users pays for calls
            payService.GetUsersPaysForPreviousMonth();

            //user can deposit money
            user.Deposit(10);
            Call(user, user2);

            //user and user2 can order history of all calls
            ShowUserHistory(user);
            ShowUserHistory(user2);
        }

        private static void TurnOnTerminal(UserAccount user)
        {
            user.TurnOnTerminal(user.Terminals.ElementAt(0));
        }

        private static void Call(UserAccount user, int number)
        {
            user.Call(user.Terminals.ElementAt(0), number);
        }

        private static void Call(UserAccount user, UserAccount user2)
        {
            user.Call(user.Terminals.ElementAt(0), user2.Terminals.ElementAt(0).Number);
            Thread.Sleep(1000);
            user.EndCall(user.Terminals.ElementAt(0));
        }

        private static IRate CreateRate(string name, int oneMinutePrice, int freeMinutes)
        {
            return new SmartRate(name, oneMinutePrice, freeMinutes);
        }

        private static UserAccount CreateAccount(string name, AtsManager manager, PayServiceManager payService)
        {
            var user = manager.CreateUserAccount(name, CreateRate("Smart", 1, 0), payService);
            manager.CreateTerminalForUser(user);
            return user;
        }

        private static void ShowUserHistory(UserAccount user)
        {
            var userInfos = user.OrderCallInfos();
            foreach (var info in userInfos)
            {
                Console.WriteLine($"{info.Duration:hh\\:mm\\:ss}, {info.Price}, {info.IsOutComingCall}, {info.StartTime}");
            }
        }
    }
}
