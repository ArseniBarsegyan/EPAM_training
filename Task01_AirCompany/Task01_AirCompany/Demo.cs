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
                Console.WriteLine(p.GetInfo());
            }

            //Creating new company
            AirCompany airCompany = new AirCompany("Airlines", planes);

            //Show all planes sorted by fly distance
            Console.WriteLine("--------------------");
            Console.WriteLine("Sorted planes");
            List<IPlane> sortedPlanes = airCompany.SortPlanesByFlyDistance();
            foreach (IPlane p in sortedPlanes)
            {
                Console.WriteLine("Plane: {0}, distance:{1}", p.Name, p.GetFlyDistance());
            }
            Console.WriteLine("--------------------");

            //Show total capacity of the company and total carrying weight
            Console.WriteLine("Info about company");
            Console.WriteLine("{0} total capacity: {1}, total carrying weight: {2}",
                airCompany.Name, airCompany.GetTotalCapacity(), airCompany.GetTotalCarryingWeight());
            Console.WriteLine("--------------------");

            //Find all planes with fuel consumption between 0 and 1000 l/h
            Console.WriteLine("Founded planes by fuel consumption between 0 and 1000");
            List<IPlane> planes2 = airCompany.FindPlaneByFuelConsumption(0, 1000);
            foreach (IPlane p in planes2)
            {
                Console.WriteLine(p.GetInfo());
            }
            Console.ReadLine();
        }
    }
}
