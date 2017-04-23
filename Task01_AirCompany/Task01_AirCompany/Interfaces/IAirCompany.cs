using System.Collections.Generic;

namespace Task01_AirCompany.Interfaces
{
    public interface IAirCompany
    {
        string Name { get; }
        ICollection<IPlane> Planes { get; }

        void AddPlane(IPlane plane);
        void RemovePlane(IPlane plane);

        int GetTotalCapacity();
        int GetTotalCarryingWeight();
        void SortPlanesByFlyDistance();
        void FindPlaneByFuelConsumption(int startValue, int endValue);
    }
}
