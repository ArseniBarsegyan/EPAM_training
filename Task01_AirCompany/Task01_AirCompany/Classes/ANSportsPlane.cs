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

        public string Airdrop()
        {
            return "airdrop";
        }

        public int GetCarryingWeight()
        {
            return CrewNumber * OnePassengerWeight;
        }

        public decimal GetFlyDistance()
        {
            return (Speed * FuelCapacity / FuelConsumption) * 1000 / GetCarryingWeight();
        }

        public string GetInfo()
        {
            return string.Format("Sports plane: {0}, speed:{1}, crew number:{2}, fuel consumption: {3}",
                Name, Speed, CrewNumber, FuelConsumption);
        }
    }
}
