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
            var server = new AtsServer("MTS", new List<Port>());

            //var port1 = server.CreatePort();
            //var terminal1 = new Terminal(port1);
            //port1.SetCurrentTerminal(terminal1);
            //terminal1.TurnOnTerminal();
            //terminal1.TurnOffTerminal();
            //terminal1.MakeCall(123);

            //var port2 = server.CreatePort();
            //var terminal2 = new Terminal(port2);
            //port2.SetCurrentTerminal(terminal2);
            //terminal2.TurnOnTerminal();
            //terminal1.MakeCall(port2.Number);
            //Thread.Sleep(1000);
            //terminal2.EndCall();

            var manager = new AtsManager("MTS manager system", server);
            var payService = new PayServiceManager(manager, server);

            var user = manager.CreateUserAccount("Arseni", new SmartRate("Smart", 1, 1));
            manager.CreateTerminalForUser(user);
            var user2 = manager.CreateUserAccount("Arseni-2", new SmartRate("Smart-2", 2, 60));
            manager.CreateTerminalForUser(user2);
            
            user2.Terminals.ElementAt(0).TurnOnTerminal();
            //Making few calls
            user.MakeCall(user.Terminals.ElementAt(0), user2.Terminals.ElementAt(0).Number);
            Thread.Sleep(1000);
            user.EndCall(user.Terminals.ElementAt(0));

            user.MakeCall(user.Terminals.ElementAt(0), user2.Terminals.ElementAt(0).Number);
            user.EndCall(user.Terminals.ElementAt(0));

            //Test PayServiceManager
        }
    }
}
