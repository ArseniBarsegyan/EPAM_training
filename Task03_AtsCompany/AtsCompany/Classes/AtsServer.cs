﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace AtsCompany.Classes
{
    public class AtsServer
    {
        public AtsServer(string name, ICollection<Port> disabledPorts)
        {
            Name = name;
            DisabledPorts = disabledPorts;
            ActivePorts = new Dictionary<Port, int>();
            EnabledPorts = new List<Port>();
            CallingPorts = new List<Port>();
        }

        public string Name { get; }
        public IDictionary<Port, int> ActivePorts { get; }
        public ICollection<Port> DisabledPorts { get; }
        public ICollection<Port> EnabledPorts { get; }
        public ICollection<Port> CallingPorts { get; }

        //When terminal created it's port get into DisabledPorts list
        public Terminal CreateTerminal()
        {
            var randomNumber = new Random();
            var number = randomNumber.Next(111111, 999999);

            while (IsPortListContainPortByNumber(number))
            {
                number = randomNumber.Next(111111, 999999);
            }
            var terminal = new Terminal(number, this);
            var port = new Port(number, terminal, this);
            DisabledPorts.Add(port);
            SubscribeOnAllPortEvents(port);
            return terminal;
        }

        private void PortOnPortEnabled(object sender)
        {
            var port = sender as Port;
            if (DisabledPorts.Contains(port))
            {
                DisabledPorts.Remove(port);
                EnabledPorts.Add(port);
            }
            else if (ActivePorts.ContainsKey(port))
            {
                ActivePorts.Remove(port);
                EnabledPorts.Add(port);
            }
        }

        private void PortOnPortDisabled(object sender)
        {
            var port = sender as Port;

            if (EnabledPorts.Contains(port))
            {
                EnabledPorts.Remove(port);
                DisabledPorts.Add(port);
            }
            else if (ActivePorts.ContainsKey(port))
            {
                ActivePorts.Remove(port);
                DisabledPorts.Add(port);
            }
        }

        private bool IsPortListContainPortByNumber(int number)
        {
            return DisabledPorts.Any(x => x.Number == number);
        }

        private void SubscribeOnAllPortEvents(Port port)
        {
            port.PortStateSetToActive += PortOnPortStateSetToActive;
            port.PortEnabled += PortOnPortEnabled;
            port.PortDisabled += PortOnPortDisabled;
        }

        private void PortOnPortStateSetToActive(object sender, PhoneNumberArgs phoneNumberArgs)
        {
            var port = sender as Port;
            if (DisabledPorts.Contains(port))
            {
                DisabledPorts.Remove(port);
                ActivePorts.Add(port, phoneNumberArgs.number);
            }
            if (EnabledPorts.Contains(port))
            {
                EnabledPorts.Remove(port);
                ActivePorts.Add(port, phoneNumberArgs.number);
            }
            CheckActivePortCalledNumber(port, phoneNumberArgs.number);
        }


        public delegate void PortContactHandler(object sender, string message);

        public event PortContactHandler UserIsUnavaliable;
        public event PortContactHandler UserIsBusy;
        public event PortContactHandler UserDoesntExists;

        private void CheckActivePortCalledNumber(Port port, int callNumber)
        {
            if (IsDisableListContainsCalledNumber(callNumber))
            {
                UserIsUnavaliable?.Invoke(port, "User is unavaliable now. Please try again.");
            }
            else if (IsActiveListContainsCalledNumber(callNumber))
            {
                UserIsBusy?.Invoke(port, "User is busy. Please try again later.");
            }
            else if (IsEnabledListContainsCalledNumber(callNumber))
            {
                
            }
            else
            {
                UserDoesntExists?.Invoke(port, "We're sorry, but user with this number doesn't exists.");
            }
        }

        private bool IsDisableListContainsCalledNumber(int number)
        {
            return DisabledPorts.Any(port => port.Number == number);
        }

        private bool IsActiveListContainsCalledNumber(int number)
        {
            return ActivePorts.Any(pair => pair.Key.Number == number);
        }

        private bool IsEnabledListContainsCalledNumber(int number)
        {
            return EnabledPorts.Any(port => port.Number == number);
        }
    }
}