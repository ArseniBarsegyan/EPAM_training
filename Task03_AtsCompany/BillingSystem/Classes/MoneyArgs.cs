using System;

namespace BillingSystem.Classes
{
    public class MoneyArgs : EventArgs
    {
        public readonly int balance;

        public MoneyArgs(int balance)
        {
            this.balance = balance;
        }
    }
}
