using System.Collections.Generic;
using AtsCompany.Classes;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new AtsServer("MTS", new List<Port>());
            var terminal = server.CreateTerminal();
            terminal.TurnOnTerminal();
            var terminal2 = server.CreateTerminal();
        }
    }
}
