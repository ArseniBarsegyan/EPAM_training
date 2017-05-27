using System;

namespace AtsCompany.Classes
{
    public class Call
    {
        public Call(int senderNumber, int recieverNumber)
        {
            SenderNumber = senderNumber;
            RecieverNumber = recieverNumber;
        }

        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public TimeSpan Duration { get; private set; }
        public int SenderNumber { get; private set; }
        public int RecieverNumber { get; private set; }

        public void Start()
        {
            StartTime = DateTime.Now;
        }

        public void Finish()
        {
            EndTime = DateTime.Now;
            Duration = EndTime - StartTime;
        }
    }
}
