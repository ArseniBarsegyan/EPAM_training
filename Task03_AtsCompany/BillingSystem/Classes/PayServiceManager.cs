using System;
using System.Collections.Generic;
using System.Linq;
using AtsCompany.Classes;

namespace BillingSystem.Classes
{
    //This class stores information about every user call (rate, time etc.)
    public class PayServiceManager
    {
        private IDictionary<UserAccount, List<CallInfo>> UsersCallsInfoDictionary { get; set; }
        private List<Port> _withdrawedPorts;

        public PayServiceManager(AtsManager manager, AtsServer server)
        {
            Manager = manager;
            Server = server;
            server.CallFinished += ServerOnCallFinished;
            UsersCallsInfoDictionary = new Dictionary<UserAccount, List<CallInfo>>();
            _withdrawedPorts = new List<Port>();
        }

        private AtsManager Manager { get; }
        private AtsServer Server { get; }

        public IEnumerable<CallInfo> OrderUserHistory(UserAccount userAccount)
        {
            return (from pair in UsersCallsInfoDictionary where pair.Key == userAccount select pair.Value).FirstOrDefault();
        }

        private void ServerOnCallFinished(object sender)
        {
            var call = sender as Call;
            if (call == null) return;
            
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
            SubscribeOnAllUsersEvents();
            var previousPeriodStart = GetPreviousDatePeriod();
            var daysInPreviousPeriod = DateTime.DaysInMonth(previousPeriodStart.Year, previousPeriodStart.Month);

            var previousPeriodEnd = previousPeriodStart.AddDays(daysInPreviousPeriod);
            var usersPays = GetTotalPayForEveryUser(previousPeriodStart, previousPeriodEnd);

            foreach (var pair in usersPays)
            {
                pair.Key.WithDraw(pair.Value);
            }

            foreach (var userAccount in usersPays.Keys)
            {
                _withdrawedPorts.AddRange(GetWithdrawedPorts(userAccount));
            }

            return usersPays;
        }

        //withdraw port from every user with balance <= 0
        private IEnumerable<Port> GetWithdrawedPorts(UserAccount userAccount)
        {
            var withdrawedPorts = new List<Port>();
            if (userAccount.Balance > 0) return withdrawedPorts;
            var userTerminals = userAccount.Terminals;
            foreach (var terminal in userTerminals)
            {
                var port = terminal.Port;
                port.RemovePortFromTerminal(terminal);
                withdrawedPorts.Add(port);
            }
            return withdrawedPorts;
        }

        private void GetBackWithdrawedPorts(UserAccount userAccount)
        {
            foreach (var terminal in userAccount.Terminals)
            {
                foreach (var port in _withdrawedPorts)
                {
                    if (port.Number != terminal.Number) continue;
                    terminal.SetCurrentPort(port);
                    port.SetCurrentTerminal(terminal);
                }
            }
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

        private void SubscribeOnAllUsersEvents()
        {
            foreach (var userAccount in UsersCallsInfoDictionary.Keys)
            {
                userAccount.MoneyAdded += UserAccountOnMoneyAdded;
            }
        }

        private void UserAccountOnMoneyAdded(object sender, MoneyArgs e)
        {
            var userAccount = sender as UserAccount;
            if (userAccount == null) return;
            if (e.balance > 0)
            {
                GetBackWithdrawedPorts(userAccount);
            }
        }
    }
}
