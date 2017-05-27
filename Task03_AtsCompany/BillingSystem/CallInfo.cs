using System;
using AtsCompany.Classes;

namespace BillingSystem
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
            CountTotalCallPrice(rate);
        }

        private void CountTotalCallPrice(IRate rate)
        {
            TotalPrice = IsOutComingCall ? Duration.TotalMinutes * rate.OneMinutePrice : 0;
        }

        public bool IsOutComingCall { get; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public TimeSpan Duration { get; private set; }
        public int SenderNumber { get; private set; }
        public int RecieverNumber { get; private set; }
        public double TotalPrice { get; private set; }
    }
}
