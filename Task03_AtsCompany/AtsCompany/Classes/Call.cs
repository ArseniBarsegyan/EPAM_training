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
        public int SenderNumber { get; }
        public int RecieverNumber { get; }

        public void FinishCall()
        {
            EndTime = DateTime.Now;
            Duration = EndTime - StartTime;
        }
    }
}
