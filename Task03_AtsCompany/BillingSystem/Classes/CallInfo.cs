using System;
using AtsCompany.Classes;
using BillingSystem.Interfaces;

namespace BillingSystem.Classes
{
    public class CallInfo
    {
        public CallInfo(Call call, IRate rate, bool isOutComingCall)
        {
            IsOutComingCall = isOutComingCall;
            StartTime = call.StartTime;
            EndTime = call.EndTime;
            Duration = call.Duration;
            SenderNumber = call.SenderNumber;
            RecieverNumber = call.RecieverNumber;
            Rate = rate;
            Price = 0;
        }

        public void SetPrice(int price)
        {
            Price = price;
        }

        public IRate Rate { get; }
        public bool IsOutComingCall { get; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public TimeSpan Duration { get; private set; }
        public int SenderNumber { get; private set; }
        public int RecieverNumber { get; private set; }
        public int Price { get; private set; }
    }
}
