using System;

namespace AtsCompany.Classes
{
    class Call
    {
        public Call()
        {
            StartTime = DateTime.Now;
        }

        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public TimeSpan Duration { get; private set; }
        public int SenderNumber { get; private set; }
        public int RecieverNumber { get; private set; }

        public void SetSenderNumber(int number)
        {
            SenderNumber = number;
        }

        public void SetRecieverNumber(int number)
        {
            RecieverNumber = number;
        }

        public void FinishCall()
        {
            EndTime = DateTime.Now;
            Duration = EndTime - StartTime;
        }
    }
}
