﻿namespace Task01_AirCompany.Interfaces
{
    public interface ISportsPlane : IPlane
    {
        int CrewNumber { get; }
        string Airdrop();
    }
}
