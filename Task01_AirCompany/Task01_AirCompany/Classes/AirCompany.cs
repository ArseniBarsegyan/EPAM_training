using System.Collections.Generic;
using System.Linq;
using Task01_AirCompany.Interfaces;

namespace Task01_AirCompany.Classes
{
    public class AirCompany : IAirCompany
    {
        public AirCompany(string name, ICollection<IPlane> planes)
        {
            Name = name;
            Planes = planes;
        }

        public string Name { get; private set; }
        public ICollection<IPlane> Planes { get; private set; }

        public void AddPlane(IPlane plane)
        {
            Planes.Add(plane);
        }

        public void RemovePlane(IPlane plane)
        {
            Planes.Remove(plane);
        }

        public IEnumerable<IPlane> FindPlaneByFuelConsumption(int startValue, int endValue)
        {
            return Planes.Where(p => (p.FuelConsumption > startValue && p.FuelConsumption < endValue));
        }

        public int GetTotalCapacity()
        {
            return Planes.Count;
        }

        public int GetTotalCarryingWeight()
        {
            return Planes.Sum(p => p.GetCarryingWeight());
        }        

        public IEnumerable<IPlane> SortPlanesByFlyDistance()
        {
            return Planes.OrderByDescending(p => p.GetFlyDistance());
        }
    }
}
