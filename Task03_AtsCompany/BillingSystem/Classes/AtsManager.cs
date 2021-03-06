﻿using System.Collections.Generic;
using AtsCompany.Classes;
using BillingSystem.Interfaces;

namespace BillingSystem.Classes
{
    public class AtsManager
    {
        public AtsManager(string name, AtsServer server)
        {
            Name = name;
            Server = server;
            UsersAccounts = new List<UserAccount>();
        }

        public string Name { get; }
        public AtsServer Server { get; }
        public ICollection<UserAccount> UsersAccounts { get; }

        public UserAccount CreateUserAccount(string name, IRate rate, PayServiceManager serviceManager)
        {
            var user = new UserAccount(name, rate, this, serviceManager);
            UsersAccounts.Add(user);
            return user;
        }

        public void CreateTerminalForUser(UserAccount userAccount)
        {
            var port = Server.CreatePort();
            var terminal = new Terminal(port);
            port.SetCurrentTerminal(terminal);
            userAccount.AddTerminal(terminal);
        }
    }
}
