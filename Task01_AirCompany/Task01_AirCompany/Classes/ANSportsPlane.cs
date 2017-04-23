using System;
using Task01_AirCompany.Interfaces;

namespace Task01_AirCompany.Classes
{
    public class ANSportsPlane : ISportsPlane
    {
        private const int OnePassengerWeight = 90;

        public string Name { get; private set; }
        public int Speed { get; private set; }
        public int FuelConsumption { get; private set; }
        public int FuelCapacity { get; private set; }
        public int CrewNumber { get; private set; }

        public ANSportsPlane(string name, int speed, int fuelConsumption, int fuelCapacity, int crewNumber)
        {
            Name = name;
            Speed = speed;
            FuelConsumption = fuelConsumption;
            FuelCapacity = fuelCapacity;
            CrewNumber = crewNumber;
        }

        public void Airdrop()
        {
            Console.WriteLine("Airdroping crew");
        }

        public int GetCarryingWeight()
        {
            return CrewNumber * OnePassengerWeight;
        }

        public decimal GetFlyDistance()
        {
            return (Speed * FuelCapacity / FuelConsumption) * 1000 / GetCarryingWeight();
        }

        public void GetInfo()
        {
            Console.WriteLine("Sports plane: {0}, flying speed: {1}, Crew number:{2}",
                Name, Speed, CrewNumber);
        }
    }
}
