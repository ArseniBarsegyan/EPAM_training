namespace Task01_AirCompany.Interfaces
{
    public interface IPlane
    {
        string Name { get; }
        int Speed { get; }
        int FuelConsumption { get; }
        int FuelCapacity { get; }

        decimal GetFlyDistance();
        int GetCarryingWeight();
        void GetInfo();
    }
}
