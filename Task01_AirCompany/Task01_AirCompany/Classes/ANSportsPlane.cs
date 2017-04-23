using System;
using Task01_AirCompany.Interfaces;

namespace Task01_AirCompany.Classes
{
    public class AnPlane : ISportsPlane
    {
        private const int OnePassengerWeight = 90;

        public string Name { get; private set; }
        public int Speed { get; private set; }
        public int FuelConsumption { get; private set; }
        public int FuelCapacity { get; private set; }
        public int CrewNumber { get; private set; }

        public AnPlane(string name, int speed, int fuelConsumption, int fuelCapacity, int crewNumber)
        {
            Name = name;
            Speed = speed;
            FuelConsumption = fuelConsumption;
            FuelCapacity = fuelCapacity;
            CrewNumber = crewNumber;
        }

        public void Airdrop()
        {
            throw new NotImplementedException();
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
