using System;
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
            CurrentCalls = new List<Call>();
            StorageCalls = new List<Call>();
        }
        
        public event EventHandler<string> UserIsUnavaliable;
        public event EventHandler<string> UserIsBusy;
        public event EventHandler<string> UserDoesntExists;
        public event EventHandler<ConnectionEventArgs> EstablishConnection;
        public event EventHandler<ConnectionEventArgs> AnswerOnAccept;
        public event EventHandler<string> AnswerOnReject;
        public event EventHandler<ConnectionEventArgs> ServerFinishedCall;
        public event EventHandler CallFinished;

        public string Name { get; }
        public ICollection<Call> CurrentCalls { get; private set; }
        public ICollection<Call> StorageCalls { get; private set; }
        public IDictionary<Port, int> ActivePorts { get; }
        public ICollection<Port> DisabledPorts { get; }
        public ICollection<Port> EnabledPorts { get; }
        public ICollection<Port> CallingPorts { get; }

        public Port CreatePort()
        {
            var randomNumber = new Random();
            var number = randomNumber.Next(111111, 999999);

            while (IsDisableListContainsCalledNumber(number))
            {
                number = randomNumber.Next(111111, 999999);
            }
            var port = new Port(number, this);
            DisabledPorts.Add(port);
            SubscribeOnAllPortEvents(port);
            return port;
        }

        //Methods-handlers of Port events

        protected virtual void PortOnPortEnabled(object sender, EventArgs e)
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

        protected virtual void PortOnPortDisabled(object sender, EventArgs e)
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

        protected virtual void PortOnPortStateSetToActive(object sender, CallEventArgs phoneNumberArgs)
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
                var port2 = EnabledPorts.FirstOrDefault(x => x.Number == callNumber);
                if (port2 != null)
                {
                    EstablishConnection?.Invoke(this, new ConnectionEventArgs(port, port2));
                }
            }
            else
            {
                UserDoesntExists?.Invoke(port, "We're sorry, but user with this number doesn't exists.");
            }
        }

        protected virtual void PortOnCallAccepted(object sender, AnswerEventArgs e)
        {
            var port1 = ActivePorts.FirstOrDefault(x => x.Key.Number == e.number1).Key;
            var port2 = EnabledPorts.FirstOrDefault(x => x.Number == e.number2);
            AnswerOnAccept?.Invoke(this, new ConnectionEventArgs(port1, port2, e.message));
        }

        protected virtual void PortOnCallRejected(object sender, AnswerEventArgs e)
        {
            var port1 = ActivePorts.FirstOrDefault(x => x.Key.Number == e.number1).Key;
            AnswerOnReject?.Invoke(port1, e.message);
        }

        protected virtual void PortOnPortConnectionEstablished(object sender, ConnectionEstablishedEventArgs e)
        {
            var port1 = e.port1;
            var port2 = e.port2;

            if (port1 == null || port2 == null) return;
            ActivePorts.Remove(port1);
            EnabledPorts.Remove(port2);
            CallingPorts.Add(port1);
            CallingPorts.Add(port2);

            var call = new Call(port1.Number, port2.Number);
            call.Start();
            CurrentCalls.Add(call);
        }

        protected virtual void PortOnPortEndCall(object sender, CallEventArgs e)
        {
            var call = CurrentCalls.FirstOrDefault(x => x.RecieverNumber == e.number || x.SenderNumber == e.number);
            if (call == null) return;

            var port1 = CallingPorts.FirstOrDefault(x => x.Number == call.SenderNumber);
            if (port1 == null) return;

            var port2 = CallingPorts.FirstOrDefault(x => x.Number == call.RecieverNumber);
            if (port2 == null) return;
            call.Finish();
            ServerFinishedCall?.Invoke(this, new ConnectionEventArgs(port1, port2, string.Format($"Call finished. Duration: {call.Duration:hh\\:mm\\:ss}")));
            CallingPorts.Remove(port1);
            CallingPorts.Remove(port2);
            EnabledPorts.Add(port1);
            EnabledPorts.Add(port2);

            CurrentCalls.Remove(call);
            StorageCalls.Add(call);
            CallFinished?.Invoke(call, EventArgs.Empty);
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

        private void SubscribeOnAllPortEvents(Port port)
        {
            port.PortStateSetToActive += PortOnPortStateSetToActive;
            port.PortEnabled += PortOnPortEnabled;
            port.PortDisabled += PortOnPortDisabled;
            port.CallRejected += PortOnCallRejected;
            port.CallAccepted += PortOnCallAccepted;
            port.PortConnectionEstablished += PortOnPortConnectionEstablished;
            port.PortEndCall += PortOnPortEndCall;
        }
    }
}
