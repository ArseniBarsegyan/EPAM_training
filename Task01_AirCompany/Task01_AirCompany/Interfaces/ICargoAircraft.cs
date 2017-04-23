namespace Task01_AirCompany.Interfaces
{
    public interface ICargoAircraft : IAircraft
    {
        int CarryingCapacity { get; }
    }
}
