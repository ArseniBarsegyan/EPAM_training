using System;

namespace BillingSystem.Classes
{
    public class UserEventArgs : EventArgs
    {
        public readonly int balance;

        public UserEventArgs(int balance)
        {
            this.balance = balance;
        }
    }
}
