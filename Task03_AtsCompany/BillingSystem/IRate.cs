namespace BillingSystem
{
    public interface IRate
    {
        string Name { get; }
        int OneMinutePrice { get; }
        int FreeMinutes { get; }
    }
}
