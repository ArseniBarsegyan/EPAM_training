namespace Task01_AirCompany.Interfaces
{
    public interface ISportsAircraft : IAircraft
    {
        int CrewNumber { get; }
        void Airdrop();
    }
}
