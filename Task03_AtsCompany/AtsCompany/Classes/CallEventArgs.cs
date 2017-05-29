using System;

namespace AtsCompany.Classes
{
    public class CallEventArgs : EventArgs
    {
        public readonly int number;

        public CallEventArgs(int number)
        {
            this.number = number;
        }
    }
}
