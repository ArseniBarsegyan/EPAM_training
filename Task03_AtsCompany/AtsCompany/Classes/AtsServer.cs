using System;
using System.Collections.Generic;
using System.Linq;

namespace AtsCompany.Classes
{
    public class AtsServer
    {
        private List<Call> _currentCalls = new List<Call>();
        private List<Call> _storageCalls = new List<Call>();

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
        public Port CreatePort()
        {
            var randomNumber = new Random();
            var number = randomNumber.Next(111111, 999999);

            while (IsPortListContainPortByNumber(number))
            {
                number = randomNumber.Next(111111, 999999);
            }
            var port = new Port(number, this);
            DisabledPorts.Add(port);
            SubscribeOnAllPortEvents(port);
            return port;
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
            port.CallRejected += PortOnCallRejected;
            port.CallAccepted += PortOnCallAccepted;
            port.PortConnectionEstablished += PortOnPortConnectionEstablished;
            port.PortEndCall += PortOnPortEndCall;
        }

        public delegate void ServerEndCallHandler(Port port1, Port port2, string message);
        public event ServerEndCallHandler ServerFinishedCall;

        private void PortOnPortEndCall(int initializatorNumber)
        {
            var call = _currentCalls.FirstOrDefault(x => x.RecieverNumber == initializatorNumber || x.SenderNumber == initializatorNumber);
            if (call == null) return;

            var port1 = CallingPorts.FirstOrDefault(x => x.Number == call.SenderNumber);
            if (port1 == null) return;

            var port2 = CallingPorts.FirstOrDefault(x => x.Number == call.RecieverNumber);
            if (port2 == null) return;
            call.Finish();
            ServerFinishedCall?.Invoke(port1, port2, string.Format($"Call finished. Duration: {call.Duration:hh\\:mm\\:ss}"));

            _currentCalls.Remove(call);
            _storageCalls.Add(call);
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
                var port2 = EnabledPorts.FirstOrDefault(x => x.Number == callNumber);
                if (port2 != null)
                {
                    EstablishConnection(port, port2);
                }
            }
            else
            {
                UserDoesntExists?.Invoke(port, "We're sorry, but user with this number doesn't exists.");
            }
        }

        public delegate void ConnectionHandler(Port port1, Port port2);
        public event ConnectionHandler ConnectionEstablish;

        private void EstablishConnection(Port port1, Port port2)
        {
            ConnectionEstablish?.Invoke(port1, port2);
        }

        public delegate void ServerAcceptHandler(object sender1, object sender2, string message);

        public event ServerAcceptHandler AnswerOnAccept;

        private void PortOnCallAccepted(int number1, int number2, string message)
        {
            var port1 = ActivePorts.FirstOrDefault(x => x.Key.Number == number1).Key;
            var port2 = EnabledPorts.FirstOrDefault(x => x.Number == number2);
            AnswerOnAccept?.Invoke(port1, port2, message);
        }

        public delegate void ServerRejectHandler(object sender, string message);

        public event ServerRejectHandler AnswerOnReject;

        private void PortOnCallRejected(int number1, int number2, string message)
        {
            var port1 = ActivePorts.FirstOrDefault(x => x.Key.Number == number1).Key;
            AnswerOnReject?.Invoke(port1, message);
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

        private bool IsPortListsContainPortByNumber(int number)
        {
            if (IsActiveListContainsCalledNumber(number))
                return true;
            if (IsDisableListContainsCalledNumber(number))
                return true;
            if (IsEnabledListContainsCalledNumber(number))
                return true;
            return false;
        }

        private void PortOnPortConnectionEstablished(object sender1, object sender2)
        {
            var port1 = sender1 as Port;
            var port2 = sender2 as Port;

            if (port1 != null && port2 != null)
            {
                ActivePorts.Remove(port1);
                EnabledPorts.Remove(port2);
                CallingPorts.Add(port1);
                CallingPorts.Add(port2);

                var call = CreateCallObject(port1.Number, port2.Number);
                _currentCalls.Add(call);
            }
        }

        private Call CreateCallObject(int senderNumber, int recieverNumber)
        {
            var call = new Call();
            call.SetSenderNumber(senderNumber);
            call.SetRecieverNumber(recieverNumber);
            return call;
        }
    }
}
