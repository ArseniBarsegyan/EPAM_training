using System;
using Task01_AirCompany.Interfaces;

namespace Task01_AirCompany.Classes
{
    public class Boeing : IPassengerPlane
    {
        public string Name { get; private set; }
        public int Speed { get; private set; }
        public int FuelConsumption { get; private set; }
        public int FuelCapacity { get; private set; }
        public int PassengersNumber { get; private set; }

        public Boeing(string name, int speed, int fuelConsumption, int fuelCapacity, int passengersNumber)
        {
            Name = name;
            Speed = speed;
            FuelConsumption = fuelConsumption;
            FuelCapacity = fuelCapacity;
            PassengersNumber = passengersNumber;
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
