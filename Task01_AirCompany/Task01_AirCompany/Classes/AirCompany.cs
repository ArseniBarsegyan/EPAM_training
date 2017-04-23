using System.Collections.Generic;
using System.Linq;
using Task01_AirCompany.Interfaces;

namespace Task01_AirCompany.Classes
{
    public class AirCompany : IAirCompany
    {
        public string Name { get; private set; }
        public ICollection<IPlane> Planes { get; private set; }

        public AirCompany(string name, ICollection<IPlane> planes)
        {
            Name = name;
            Planes = planes;
        }

        public void AddPlane(IPlane plane)
        {
            Planes.Add(plane);
        }

        public void RemovePlane(IPlane plane)
        {
            Planes.Remove(plane);
        }

        public List<IPlane> FindPlaneByFuelConsumption(int startValue, int endValue)
        {
            List<IPlane> planes = Planes.Where(p => (p.FuelConsumption > startValue && p.FuelConsumption < endValue)).ToList();
            return planes;
        }

        public int GetTotalCapacity()
        {
            return Planes.Count;
        }

        public int GetTotalCarryingWeight()
        {
            return Planes.Sum(p => p.GetCarryingWeight());
        }        

        public List<IPlane> SortPlanesByFlyDistance()
        {
            List<IPlane> sortedPlanes = Planes.OrderByDescending(p => p.GetFlyDistance()).ToList();
            return sortedPlanes;
        }
    }
}
