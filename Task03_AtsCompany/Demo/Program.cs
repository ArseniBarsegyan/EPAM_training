using System.Collections.Generic;
using System.Threading;
using AtsCompany.Classes;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new AtsServer("MTS", new List<Port>());
            var port1 = server.CreatePort();
            var terminal1 = new Terminal(port1);
            port1.SetCurrentTerminal(terminal1);
            terminal1.TurnOnTerminal();
            terminal1.TurnOffTerminal();
            terminal1.MakeCall(123);

            var port2 = server.CreatePort();
            var terminal2 = new Terminal(port2);
            port2.SetCurrentTerminal(terminal2);
            terminal2.TurnOnTerminal();
            terminal1.MakeCall(port2.Number);
            Thread.Sleep(1000);
            terminal2.EndCall();
        }
    }
}
