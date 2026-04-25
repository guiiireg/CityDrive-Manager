using System.Collections.Generic;
using CityDriveManager.Models;

namespace CityDriveManager.Services
{
    public class DataManager
    {
        public List<PointOfInterest> PointsOfInterest { get; } = new List<PointOfInterest>();
        public List<Vehicle> Vehicles { get; } = new List<Vehicle>();
        public List<Trip> Trips { get; } = new List<Trip>();

        public void AddPointOfInterest(PointOfInterest poi)
        {
            PointsOfInterest.Add(poi);
        }

        public void AddVehicle(Vehicle vehicle)
        {
            Vehicles.Add(vehicle);
        }

        public void AddTrip(Trip trip)
        {
            Trips.Add(trip);
        }
    }
}
