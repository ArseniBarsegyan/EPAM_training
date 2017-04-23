namespace Task01_AirCompany.Interfaces
{
    public interface ICargoPlane : IPlane
    {
        int CarryingCapacity { get; }
    }
}
