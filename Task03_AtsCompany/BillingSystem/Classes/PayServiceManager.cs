using System;
using System.Collections.Generic;
using System.Linq;
using AtsCompany.Classes;

namespace BillingSystem.Classes
{
    //This class stores information about every user call (rate, time etc.)
    public class PayServiceManager
    {
        public IDictionary<UserAccount, List<CallInfo>> UsersCallsInfoDictionary { get; private set; }

        public PayServiceManager(AtsManager manager, AtsServer server)
        {
            Manager = manager;
            Server = server;
            server.CallFinished += ServerOnCallFinished;
            UsersCallsInfoDictionary = new Dictionary<UserAccount, List<CallInfo>>();
        }
        
        private void ServerOnCallFinished(Call call)
        {
            foreach (var userAccount in Manager.UsersAccounts)
            {
                if (!UsersCallsInfoDictionary.ContainsKey(userAccount))
                {
                    UsersCallsInfoDictionary.Add(userAccount, new List<CallInfo>());
                }
                
                foreach (var terminal in userAccount.Terminals)
                {
                    if (call.SenderNumber == terminal.Number)
                    {
                        var callInfo = new CallInfo(call, userAccount.CurrentRate, true);
                        callInfo.SetPrice(CountCallPrice(userAccount, callInfo));
                        UsersCallsInfoDictionary[userAccount].Add(callInfo);
                    }
                    else if (call.RecieverNumber == terminal.Number)
                    {
                        var callInfo = new CallInfo(call, userAccount.CurrentRate, false);
                        UsersCallsInfoDictionary[userAccount].Add(callInfo);
                    }
                }
            }
            CountTotalPayForEveryUser();
        }
       
        //Count call price and reduce user's free minutes
        private int CountCallPrice(UserAccount userAccount, CallInfo callInfo)
        {
            var callPrice = 0;
            var callDuration = Math.Ceiling(callInfo.Duration.TotalMinutes);

            for (int i = 0; i < callDuration; i++)
            {
                if (userAccount.FreeMinutes > 0)
                {
                    userAccount.SpendFreeMinute();
                }
                else
                {
                    callPrice += callInfo.Rate.OneMinutePrice;
                }
            }
            return callPrice;
        }

        public delegate void MonthPayHandler(IDictionary<UserAccount, double> totalMonthPays);
        public event MonthPayHandler SendTotalMonthPays;

        //return dictionary with key = UserAccount and value = totalPrice for all calls
        private void CountTotalPayForEveryUser()
        {
            IDictionary<UserAccount, double> totalMonthPays = new Dictionary<UserAccount, double>();
            foreach (var pair in UsersCallsInfoDictionary)
            {
                var userAccount = pair.Key;
                var userCalls = pair.Value;
                var totalPay = userCalls.Sum(x => x.Price);

                if (!totalMonthPays.ContainsKey(userAccount))
                {
                    totalMonthPays.Add(pair.Key, totalPay);
                }
                totalMonthPays[userAccount] = totalPay;
            }
            SendTotalMonthPays?.Invoke(totalMonthPays);
        }

        public AtsManager Manager { get; }
        public AtsServer Server { get; }
    }
}
