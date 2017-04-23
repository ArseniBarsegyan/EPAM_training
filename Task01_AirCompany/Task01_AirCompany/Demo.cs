using System;
using System.Collections.Generic;
using Task01_AirCompany.Classes;
using Task01_AirCompany.Interfaces;

namespace Task01_AirCompany
{
    public class Demo
    {
        public static void Main(string[] args)
        {
            //Creating collection of planes
            List<IPlane> planes = new List<IPlane>()
            {
                new Airbus("Airbus01", 900, 6000, 130000, 12000),
                new Boeing("Boeing01", 1000, 5000, 120000, 400),
                new ANSportsPlane("AN01", 300, 300, 600, 2)
            };

            //Get info about every plane in list
            foreach (IPlane p in planes)
            {
                p.GetInfo();
            }

            //Creating new company
            AirCompany airCompany = new AirCompany("Airlines", planes);

            //Show all planes sorted by fly distance
            Console.WriteLine("Sorted planes");
            airCompany.SortPlanesByFlyDistance();
            Console.WriteLine("--------------------");

            //Show total capacity of the company and total carrying weight
            Console.WriteLine("Info about company");
            Console.WriteLine("{0} total capacity: {1}",airCompany.Name, airCompany.GetTotalCapacity());
            Console.WriteLine("{0} total carrying weight of all planes: {1}", 
                airCompany.Name, airCompany.GetTotalCarryingWeight());
            Console.WriteLine("--------------------");

            //Find all planes with fuel consumption between 0 and 1000 l/h
            Console.WriteLine("Founded planes by fuel consumption");
            airCompany.FindPlaneByFuelConsumption(0, 1000);
        }
    }
}
