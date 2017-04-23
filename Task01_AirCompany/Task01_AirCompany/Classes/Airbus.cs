using System;
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
            return CarryingCapacity;
        }

        public decimal GetFlyDistance()
        {
            return (Speed * FuelCapacity / FuelConsumption) * 1000 / GetCarryingWeight();
        }

        public void GetInfo()
        {
            Console.WriteLine("Cargo airplane: {0}, flying speed: {1}, Carrying capacity:{2}",
                Name, Speed, CarryingCapacity);
        }
    }
}
