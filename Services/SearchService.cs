using System;
using System.Collections.Generic;
using System.Linq;
using CityDriveManager.Models;

namespace CityDriveManager.Services
{
    public class SearchService
    {
        private readonly DataManager _dataManager;

        public SearchService(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public List<Vehicle> SearchByType(string type)
        {
            return _dataManager.Vehicles
                .Where(v => v.GetVehicleType().Equals(type, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public List<Vehicle> SearchByBrand(string brand)
        {
            return _dataManager.Vehicles
                .Where(v => v.Brand.Contains(brand, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public List<Vehicle> FilterByMinSpeed(int minSpeed)
        {
            return _dataManager.Vehicles
                .Where(v => v.CurrentSpeed >= minSpeed)
                .ToList();
        }

        public List<Vehicle> FilterByMaxSpeed(int maxSpeed)
        {
            return _dataManager.Vehicles
                .Where(v => v.CurrentSpeed <= maxSpeed)
                .ToList();
        }

        public List<HybridCar> FilterByMinBattery(double minBattery)
        {
            return _dataManager.Vehicles
                .OfType<HybridCar>()
                .Where(h => h.BatteryLevel >= minBattery)
                .ToList();
        }

        public List<HybridCar> FilterByMinFuel(double minFuel)
        {
            return _dataManager.Vehicles
                .OfType<HybridCar>()
                .Where(h => h.FuelLevel >= minFuel)
                .ToList();
        }

        public List<PointOfInterest> SearchPoiByName(string name)
        {
            return _dataManager.PointsOfInterest
                .Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public List<PointOfInterest> FindNearby(PointOfInterest reference, double radiusKm)
        {
            return _dataManager.PointsOfInterest
                .Where(p => p != reference && reference.CalculateDistance(p) <= radiusKm)
                .ToList();
        }
    }
}
