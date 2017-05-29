using System;

namespace AtsCompany.Classes
{
    public class ConnectionEstablishedEventArgs : EventArgs
    {
        public readonly Port port1;
        public readonly Port port2;

        public ConnectionEstablishedEventArgs(Port port1, Port port2)
        {
            this.port1 = port1;
            this.port2 = port2;
        }
    }
}
