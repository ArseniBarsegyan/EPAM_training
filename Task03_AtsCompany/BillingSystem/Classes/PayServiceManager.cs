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

        public AtsManager Manager { get; }
        public AtsServer Server { get; }

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

        //return dictionary with key = UserAccount and value = totalPrice for all calls
        private IDictionary<UserAccount, int> GetTotalPayForEveryUser(DateTime startTime, DateTime endTime)
        {
            IDictionary<UserAccount, int> totalMonthPays = new Dictionary<UserAccount, int>();
            foreach (var pair in UsersCallsInfoDictionary)
            {
                var userAccount = pair.Key;
                List<CallInfo> userCalls = pair.Value
                    .Where(x => x.StartTime > startTime && x.StartTime < endTime && x.EndTime < endTime)
                    .ToList();

                var totalPay = userCalls.Sum(x => x.Price);

                if (!totalMonthPays.ContainsKey(userAccount))
                {
                    totalMonthPays.Add(pair.Key, totalPay);
                }
                totalMonthPays[userAccount] = totalPay;
            }
            return totalMonthPays;
        }

        public IDictionary<UserAccount, int> GetUsersPaysForPreviousMonth()
        {
            var previousPeriodStart = GetPreviousDatePeriod();
            var daysInPreviousPeriod = DateTime.DaysInMonth(previousPeriodStart.Year, previousPeriodStart.Month);

            var previousPeriodEnd = previousPeriodStart.AddDays(daysInPreviousPeriod);
            var usersPays = GetTotalPayForEveryUser(previousPeriodStart, previousPeriodEnd);

            foreach (var pair in usersPays)
            {
                pair.Key.WithDraw(pair.Value);
            }
            return usersPays;
        }

        private IEnumerable<Port> WithdrawedPorts(UserAccount userAccount)
        {
            var withdrawedPorts = new List<Port>();
            if (userAccount.Balance <= 0)
            {
                var userTerminals = userAccount.Terminals;
                foreach (var terminal in userTerminals)
                {
                    var port = terminal.Port;
                    port.RemovePortFromTerminal(terminal);
                    withdrawedPorts.Add(port);
                }
            }
            return withdrawedPorts;
        }

        private DateTime GetPreviousDatePeriod()
        {
            var currentDate = DateTime.Now;
            var currentYear = currentDate.Year;
            var currentMonth = currentDate.Month;
            
            var previousPeriodYear = currentYear;
            int previousPeriodMonth;
            if (currentMonth > 1)
            {
                previousPeriodMonth = currentMonth - 1;
            }
            else
            {
                previousPeriodMonth = 12;
                previousPeriodYear -= 1;
            }
            return new DateTime(previousPeriodYear, previousPeriodMonth, 1);
        }
    }
}
