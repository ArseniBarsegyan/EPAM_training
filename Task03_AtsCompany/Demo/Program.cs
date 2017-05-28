using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AtsCompany.Classes;
using BillingSystem.Classes;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new AtsServer("Company server", new List<Port>());
            var manager = new AtsManager("Company manager system", server);
            var payService = new PayServiceManager(manager, server);

            var user = manager.CreateUserAccount("user1", new SmartRate("Smart", 1, 0), payService);
            manager.CreateTerminalForUser(user);
            var user2 = manager.CreateUserAccount("user2", new SmartRate("Smart2", 2, 60), payService);
            manager.CreateTerminalForUser(user2);
            
            user2.Terminals.ElementAt(0).TurnOnTerminal();
            //Making few calls
            user.MakeCall(user.Terminals.ElementAt(0), user2.Terminals.ElementAt(0).Number);
            Thread.Sleep(1000);
            user.EndCall(user.Terminals.ElementAt(0));

            user.MakeCall(user.Terminals.ElementAt(0), user2.Terminals.ElementAt(0).Number);
            Thread.Sleep(1000);
            user.EndCall(user.Terminals.ElementAt(0));
            var pays = payService.GetUsersPaysForPreviousMonth();

            //Now is unavaliable
            user.Terminals.ElementAt(0).MakeCall(1221);

            //When user's balance become more then 0, he can call again
            user.Deposit(10);
            user.Terminals.ElementAt(0).MakeCall(1221);

            var infos = user.OrderCallInfos();
            foreach (var info in infos)
            {
                Console.WriteLine($"{info.Duration}, {info.Price}, {info.RecieverNumber}");
            }
        }
    }
}
