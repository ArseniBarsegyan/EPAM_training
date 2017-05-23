using System;

namespace AtsCompany.Classes
{
    public class PhoneNumberArgs : EventArgs
    {
        public readonly int number;

        public PhoneNumberArgs(int number)
        {
            this.number = number;
        }
    }
}
