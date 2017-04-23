using System;
using Task01_AirCompany.Interfaces;

namespace Task01_AirCompany.Classes
{
    public class Boeing : IPassengerPlane
    {
        private const int OnePassengerWeight = 80;

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
            return PassengersNumber * OnePassengerWeight;
        }

        public decimal GetFlyDistance()
        {
            return (Speed * FuelCapacity / FuelConsumption) * 1000 / GetCarryingWeight();
        }

        public void GetInfo()
        {
            Console.WriteLine("Passenger plane: {0}, flying speed: {1}, Passengers number:{2}",
                Name, Speed, PassengersNumber);
        }
    }
}
