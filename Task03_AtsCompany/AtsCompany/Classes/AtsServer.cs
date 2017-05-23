using System;
using System.Collections.Generic;
using System.Linq;

namespace AtsCompany.Classes
{
    public class AtsServer
    {
        public AtsServer(string name, ICollection<Port> disabledPorts)
        {
            Name = name;
            DisabledPorts = disabledPorts;
            ActivePorts = new Dictionary<Port, int>();
            EnabledPorts = new List<Port>();
            CallingPorts = new List<Port>();
        }

        public string Name { get; }
        public IDictionary<Port, int> ActivePorts { get; }
        public ICollection<Port> DisabledPorts { get; }
        public ICollection<Port> EnabledPorts { get; }
        public ICollection<Port> CallingPorts { get; }

        //When terminal created it's port get into DisabledPorts list
        public Terminal CreateTerminal()
        {
            var randomNumber = new Random();
            var number = randomNumber.Next(111111, 999999);

            while (IsPortListContainPortByNumber(number))
            {
                number = randomNumber.Next(111111, 999999);
            }
            var terminal = new Terminal(number, this);
            var port = new Port(number, terminal, this);
            DisabledPorts.Add(port);

            return terminal;
        }

        private bool IsPortListContainPortByNumber(int number)
        {
            return DisabledPorts.Any(x => x.Number == number);
        }
    }
}
