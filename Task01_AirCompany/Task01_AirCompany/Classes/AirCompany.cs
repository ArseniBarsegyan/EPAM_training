using System;
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

        public void FindPlaneByFuelConsumption(int startValue, int endValue)
        {
            var planes = Planes.Where(p => (p.FuelConsumption > startValue && p.FuelConsumption < endValue));
            foreach (var p in planes)
            {
                Console.WriteLine("Airplane: {0}, fuel consumption: {1}", p.Name, p.FuelConsumption);
            }
        }

        public int GetTotalCapacity()
        {
            return Planes.Count;
        }

        public int GetTotalCarryingWeight()
        {
            return Planes.Sum(p => p.GetCarryingWeight());
        }        

        public void SortPlanesByFlyDistance()
        {
            var sortedPlanes = Planes.OrderByDescending(p => p.GetFlyDistance());
            foreach (var p in sortedPlanes)
            {
                Console.WriteLine("Plane: {0}, distance: {1}", p.Name, p.GetFlyDistance());
            }
        }
    }
}
