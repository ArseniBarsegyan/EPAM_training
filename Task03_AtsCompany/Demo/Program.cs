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
            //Create AtsServer, AtsManager and AtsPayService
            var server = new AtsServer("Company server", new List<Port>());
            var manager = new AtsManager("Company manager system", server);
            var payService = new PayServiceManager(manager, server);

            var user = manager.CreateUserAccount("user1", new SmartRate("Smart", 1, 0), payService);
            manager.CreateTerminalForUser(user);

            var user2 = manager.CreateUserAccount("user2", new SmartRate("Smart2", 2, 60), payService);
            manager.CreateTerminalForUser(user2);
            
            //call from user's terminal#0 at number that doesn't exists
            user.MakeCall(user.Terminals.ElementAt(0), 1111);


            user2.Terminals.ElementAt(0).TurnOnTerminal();
            //user making two calls
            user.MakeCall(user.Terminals.ElementAt(0), user2.Terminals.ElementAt(0).Number);
            Thread.Sleep(1000);
            user.EndCall(user.Terminals.ElementAt(0));
            
            user.MakeCall(user.Terminals.ElementAt(0), user2.Terminals.ElementAt(0).Number);
            Thread.Sleep(1000);
            user2.EndCall(user.Terminals.ElementAt(0));

            //When the time will come, payService just invoke it's method and withdraw
            //users money
            var pays = payService.GetUsersPaysForPreviousMonth();

            //Will be unavaliable - if balance less than 0 his terminal won't work
            user.MakeCall(user.Terminals.ElementAt(0), 1111);

            //When user's balance become more then 0, he can call again
            user.Deposit(10);
            user.MakeCall(user.Terminals.ElementAt(0), 1111);

            var userInfos = user.OrderCallInfos();
            foreach (var info in userInfos)
            {
                Console.WriteLine($"{info.Duration:hh\\:mm\\:ss}, {info.Price}, {info.IsOutComingCall}, {info.StartTime}");
            }

            var user2Infos = user2.OrderCallInfos();
            foreach (var info in user2Infos)
            {
                Console.WriteLine($"{info.Duration:hh\\:mm\\:ss}, {info.Price}, {info.IsOutComingCall}, {info.StartTime}");
            }
        }
    }
}
