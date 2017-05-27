namespace BillingSystem
{
    class SmartRate : IRate
    {
        public SmartRate(string name, int oneMinutePrice, int freeMinutes)
        {
            Name = name;
            OneMinutePrice = oneMinutePrice;
            FreeMinutes = freeMinutes;
        }

        public string Name { get; }
        public int OneMinutePrice { get; }
        public int FreeMinutes { get; }
    }
}
