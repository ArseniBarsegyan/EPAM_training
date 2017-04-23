using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task01_AirCompany.Interfaces;

namespace Task01_AirCompany.Classes
{
    public class Airbus : ICargoPlane
    {   
        public string Name { get; private set; }
        public int Speed { get; private set; }
        public int FuelConsumption { get; private set; }
        public int FuelCapacity { get; private set; }
        public int CarryingCapacity { get; private set; }

        public Airbus(string name, int speed, int fuelConsumption, int fuelCapacity, int carryingCapacity)
        {
            Name = name;
            Speed = speed;
            FuelConsumption = fuelConsumption;
            FuelCapacity = fuelCapacity;
            CarryingCapacity = carryingCapacity;
        }

        public int GetCarryingWeight()
        {
            throw new NotImplementedException();
        }

        public decimal GetFlyDistance()
        {
            throw new NotImplementedException();
        }

        public void GetInfo()
        {
            throw new NotImplementedException();
        }
    }
}
