using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void GetPlaneByFuelConsumption(int startValue, int endValue)
        {
            throw new NotImplementedException();
        }

        public int GetTotalCapacity()
        {
            throw new NotImplementedException();
        }

        public int GetTotalCarryingWeight()
        {
            throw new NotImplementedException();
        }        

        public void SortPlanesByFlyDistance()
        {
            throw new NotImplementedException();
        }
    }
}
