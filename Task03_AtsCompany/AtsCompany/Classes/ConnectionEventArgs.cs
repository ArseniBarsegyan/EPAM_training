using System;

namespace AtsCompany.Classes
{
    public class ConnectionEventArgs : EventArgs
    {
        public readonly Port port1;
        public readonly Port port2;
        public readonly string message;

        public ConnectionEventArgs(Port port1, Port port2)
        {
            this.port1 = port1;
            this.port2 = port2;
        }

        public ConnectionEventArgs(Port port1, Port port2, string message)
        {
            this.port1 = port1;
            this.port2 = port2;
            this.message = message;
        }
    }
}
