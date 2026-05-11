using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using CityDriveManager.Models;

namespace CityDriveManager.Services
{
    public class PersistenceService
    {
        private const string DATA_FILE = "citydrivemanager_data.json";

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true
        };

        private class SaveData
        {
            public List<VehicleDto> Vehicles { get; set; } = new();
            public List<PoiDto> PointsOfInterest { get; set; } = new();
            public List<TripDto> Trips { get; set; } = new();
        }

        private class VehicleDto
        {
            public string Type { get; set; }
            public string Brand { get; set; }
            public string Color { get; set; }
            public int CurrentSpeed { get; set; }
            public string? Model { get; set; }
            public double? Tonnage { get; set; }
            public double? BatteryLevel { get; set; }
            public double? FuelLevel { get; set; }
        }

        private class PoiDto
        {
            public string Type { get; set; }
            public string Name { get; set; }
            public double Latitude { get; set; }
            public double Longitude { get; set; }
            public int? Capacity { get; set; }
            public int? BuildYear { get; set; }
        }

        private class TripDto
        {
            public int VehicleIndex { get; set; }
            public int StartPointIndex { get; set; }
            public int EndPointIndex { get; set; }
            public DateTime DepartureDate { get; set; }
            public TrafficState Traffic { get; set; }
        }

        public void Save(DataManager dataManager)
        {
            try
            {
                var saveData = new SaveData();

                foreach (var v in dataManager.Vehicles)
                {
                    var dto = new VehicleDto
                    {
                        Brand = v.Brand,
                        Color = v.Color,
                        CurrentSpeed = v.CurrentSpeed
                    };

                    switch (v)
                    {
                        case HybridCar hc:
                            dto.Type = "HybridCar";
                            dto.BatteryLevel = hc.BatteryLevel;
                            dto.FuelLevel = hc.FuelLevel;
                            break;
                        case Car car:
                            dto.Type = "Car";
                            dto.Model = car.Model;
                            break;
                        case Truck truck:
                            dto.Type = "Truck";
                            dto.Tonnage = truck.Tonnage;
                            break;
                    }

                    saveData.Vehicles.Add(dto);
                }

                foreach (var poi in dataManager.PointsOfInterest)
                {
                    var dto = new PoiDto
                    {
                        Name = poi.Name,
                        Latitude = poi.Latitude,
                        Longitude = poi.Longitude
                    };

                    switch (poi)
                    {
                        case Campus campus:
                            dto.Type = "Campus";
                            dto.Capacity = campus.Capacity;
                            break;
                        case HistoricalMonument hm:
                            dto.Type = "HistoricalMonument";
                            dto.BuildYear = hm.BuildYear;
                            break;
                        default:
                            dto.Type = "PointOfInterest";
                            break;
                    }

                    saveData.PointsOfInterest.Add(dto);
                }

                foreach (var trip in dataManager.Trips)
                {
                    var dto = new TripDto
                    {
                        VehicleIndex = dataManager.Vehicles.IndexOf(trip.Vehicle),
                        StartPointIndex = dataManager.PointsOfInterest.IndexOf(trip.StartPoint),
                        EndPointIndex = dataManager.PointsOfInterest.IndexOf(trip.EndPoint),
                        DepartureDate = trip.DepartureDate,
                        Traffic = trip.Traffic
                    };
                    saveData.Trips.Add(dto);
                }

                string json = JsonSerializer.Serialize(saveData, _jsonOptions);
                File.WriteAllText(DATA_FILE, json);
                Console.WriteLine($"Data saved successfully to {DATA_FILE}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving data: {ex.Message}");
            }
        }

        public void Load(DataManager dataManager)
        {
            if (!File.Exists(DATA_FILE))
            {
                Console.WriteLine("No save file found. Starting with empty data.");
                return;
            }

            try
            {
                string json = File.ReadAllText(DATA_FILE);
                var saveData = JsonSerializer.Deserialize<SaveData>(json, _jsonOptions);

                if (saveData == null) return;

                foreach (var dto in saveData.Vehicles)
                {
                    Vehicle vehicle = dto.Type switch
                    {
                        "Car" => new Car
                        {
                            Brand = dto.Brand,
                            Color = dto.Color,
                            CurrentSpeed = dto.CurrentSpeed,
                            Model = dto.Model ?? ""
                        },
                        "Truck" => new Truck
                        {
                            Brand = dto.Brand,
                            Color = dto.Color,
                            CurrentSpeed = dto.CurrentSpeed,
                            Tonnage = dto.Tonnage ?? 0
                        },
                        "HybridCar" => new HybridCar
                        {
                            Brand = dto.Brand,
                            Color = dto.Color,
                            CurrentSpeed = dto.CurrentSpeed,
                            BatteryLevel = dto.BatteryLevel ?? 0,
                            FuelLevel = dto.FuelLevel ?? 0
                        },
                        _ => throw new InvalidOperationException($"Unknown vehicle type: {dto.Type}")
                    };

                    dataManager.AddVehicle(vehicle);
                }

                foreach (var dto in saveData.PointsOfInterest)
                {
                    PointOfInterest poi = dto.Type switch
                    {
                        "Campus" => new Campus
                        {
                            Name = dto.Name,
                            Latitude = dto.Latitude,
                            Longitude = dto.Longitude,
                            Capacity = dto.Capacity ?? 0
                        },
                        "HistoricalMonument" => new HistoricalMonument
                        {
                            Name = dto.Name,
                            Latitude = dto.Latitude,
                            Longitude = dto.Longitude,
                            BuildYear = dto.BuildYear ?? 0
                        },
                        _ => new PointOfInterest
                        {
                            Name = dto.Name,
                            Latitude = dto.Latitude,
                            Longitude = dto.Longitude
                        }
                    };

                    dataManager.AddPointOfInterest(poi);
                }

                foreach (var dto in saveData.Trips)
                {
                    if (dto.VehicleIndex >= 0 && dto.VehicleIndex < dataManager.Vehicles.Count
                        && dto.StartPointIndex >= 0 && dto.StartPointIndex < dataManager.PointsOfInterest.Count
                        && dto.EndPointIndex >= 0 && dto.EndPointIndex < dataManager.PointsOfInterest.Count)
                    {
                        var trip = new Trip
                        {
                            Vehicle = dataManager.Vehicles[dto.VehicleIndex],
                            StartPoint = dataManager.PointsOfInterest[dto.StartPointIndex],
                            EndPoint = dataManager.PointsOfInterest[dto.EndPointIndex],
                            DepartureDate = dto.DepartureDate,
                            Traffic = dto.Traffic
                        };
                        dataManager.AddTrip(trip);
                    }
                }

                Console.WriteLine($"Data loaded successfully from {DATA_FILE}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data: {ex.Message}");
            }
        }
    }
}
