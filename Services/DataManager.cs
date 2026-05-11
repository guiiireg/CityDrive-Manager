using System;
using System.Collections.Generic;
using System.Linq;
using CityDriveManager.Models;

namespace CityDriveManager.Services
{
    public class DataManager
    {
        public List<PointOfInterest> PointsOfInterest { get; } = new List<PointOfInterest>();
        public List<Vehicle> Vehicles { get; } = new List<Vehicle>();
        public List<Trip> Trips { get; } = new List<Trip>();

        public Dictionary<string, int> VehicleTypeCounts { get; } = new Dictionary<string, int>();

        public HashSet<string> PoiNames { get; } = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        public bool AddPointOfInterest(PointOfInterest poi)
        {
            if (!PoiNames.Add(poi.Name))
            {
                Console.WriteLine($"A point of interest named '{poi.Name}' already exists (duplicate prevented).");
                return false;
            }
            PointsOfInterest.Add(poi);
            return true;
        }

        public void AddVehicle(Vehicle vehicle)
        {
            Vehicles.Add(vehicle);

            string type = vehicle.GetVehicleType();
            if (VehicleTypeCounts.ContainsKey(type))
                VehicleTypeCounts[type]++;
            else
                VehicleTypeCounts[type] = 1;
        }

        public void AddTrip(Trip trip)
        {
            Trips.Add(trip);
        }

        public void DisplayVehicleSummary()
        {
            Console.WriteLine("--- Vehicle Summary ---");
            foreach (var kvp in VehicleTypeCounts)
            {
                Console.WriteLine($"  {kvp.Key}: {kvp.Value}");
            }
            Console.WriteLine($"  Total: {Vehicles.Count}");
        }
    }
}
