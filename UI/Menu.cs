using System;
using System.Collections.Generic;
using CityDriveManager.Models;
using CityDriveManager.Services;

namespace CityDriveManager.UI
{
    public class Menu
    {
        private readonly DataManager _dataManager;
        private readonly SearchService _searchService;
        private readonly PersistenceService _persistenceService;

        public Menu(DataManager dataManager)
        {
            _dataManager = dataManager;
            _searchService = new SearchService(dataManager);
            _persistenceService = new PersistenceService();
        }

        public void Show()
        {

            _persistenceService.Load(_dataManager);

            bool running = true;
            while (running)
            {
                Console.WriteLine();
                Console.WriteLine("==============================");
                Console.WriteLine("CITY DRIVE MANAGER - SMART CITY");
                Console.WriteLine("==============================");
                Console.WriteLine(" 1. Add a point of interest");
                Console.WriteLine(" 2. Add a vehicle");
                Console.WriteLine(" 3. Display vehicles");
                Console.WriteLine(" 4. Display places");
                Console.WriteLine(" 5. Calculate a distance");
                Console.WriteLine(" 6. Simulate acceleration / braking");
                Console.WriteLine(" 7. Create a trip");
                Console.WriteLine(" 8. Display trips");
                Console.WriteLine(" 9. Search vehicles");
                Console.WriteLine("10. Search places");
                Console.WriteLine("11. Manage fuel / battery");
                Console.WriteLine("12. Vehicle summary");
                Console.WriteLine("13. Save data");
                Console.WriteLine("14. Quit");
                Console.Write("Choice: ");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    try
                    {
                        switch (choice)
                        {
                            case 1: AddPointOfInterest(); break;
                            case 2: AddVehicle(); break;
                            case 3: DisplayVehicles(); break;
                            case 4: DisplayPlaces(); break;
                            case 5: CalculateDistance(); break;
                            case 6: SimulateMovement(); break;
                            case 7: CreateTrip(); break;
                            case 8: DisplayTrips(); break;
                            case 9: SearchVehicles(); break;
                            case 10: SearchPlaces(); break;
                            case 11: ManageFuelBattery(); break;
                            case 12: _dataManager.DisplayVehicleSummary(); break;
                            case 13: _persistenceService.Save(_dataManager); break;
                            case 14:
                                _persistenceService.Save(_dataManager);
                                running = false;
                                Console.WriteLine("Data saved. Closing program...");
                                break;
                            default: Console.WriteLine("Invalid choice. Please enter a number between 1 and 14."); break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }
        }

        // ==================== POINTS OF INTEREST ====================

        private void AddPointOfInterest()
        {
            Console.WriteLine("--- Add a point of interest ---");
            int type = InputHelper.ReadInt("Type (1=Campus, 2=Monument): ", 1, 2);
            string name = InputHelper.ReadString("Name: ");
            double lat = InputHelper.ReadDouble("Latitude: ");
            double lon = InputHelper.ReadDouble("Longitude: ");

            PointOfInterest poi;
            if (type == 1)
            {
                int cap = InputHelper.ReadInt("Capacity: ");
                poi = new Campus { Name = name, Latitude = lat, Longitude = lon, Capacity = cap };
            }
            else
            {
                int year = InputHelper.ReadInt("Build Year: ");
                poi = new HistoricalMonument { Name = name, Latitude = lat, Longitude = lon, BuildYear = year };
            }

            if (_dataManager.AddPointOfInterest(poi))
                Console.WriteLine("Point added successfully!");
        }

        // ==================== VEHICLES ====================

        private void AddVehicle()
        {
            Console.WriteLine("--- Add a vehicle ---");
            int type = InputHelper.ReadInt("Type (1=Car, 2=Truck, 3=HybridCar): ", 1, 3);
            string brand = InputHelper.ReadString("Brand: ");
            string color = InputHelper.ReadString("Color: ");

            Vehicle vehicle;
            if (type == 1)
            {
                string model = InputHelper.ReadString("Model: ");
                vehicle = new Car { Brand = brand, Color = color, Model = model };
            }
            else if (type == 2)
            {
                double tonnage = InputHelper.ReadPositiveDouble("Tonnage: ");
                vehicle = new Truck { Brand = brand, Color = color, Tonnage = tonnage };
            }
            else
            {
                double battery = InputHelper.ReadPositiveDouble("Battery level (%): ");
                double fuel = InputHelper.ReadPositiveDouble("Fuel level (L): ");
                vehicle = new HybridCar { Brand = brand, Color = color, BatteryLevel = battery, FuelLevel = fuel };
            }

            _dataManager.AddVehicle(vehicle);
            Console.WriteLine("Vehicle added!");
        }

        private void DisplayVehicles()
        {
            Console.WriteLine("--- Vehicle List ---");
            if (_dataManager.Vehicles.Count == 0)
            {
                Console.WriteLine("No vehicles registered.");
                return;
            }
            for (int i = 0; i < _dataManager.Vehicles.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] {_dataManager.Vehicles[i]}");
                Console.WriteLine("-------------------");
            }
        }

        // ==================== PLACES ====================

        private void DisplayPlaces()
        {
            Console.WriteLine("--- Points of Interest ---");
            if (_dataManager.PointsOfInterest.Count == 0)
            {
                Console.WriteLine("No points of interest registered.");
                return;
            }
            for (int i = 0; i < _dataManager.PointsOfInterest.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] {_dataManager.PointsOfInterest[i]}");
                Console.WriteLine("-------------------");
            }
        }

        // ==================== DISTANCE ====================

        private void CalculateDistance()
        {
            if (_dataManager.PointsOfInterest.Count < 2)
            {
                Console.WriteLine("You need at least 2 points of interest to calculate a distance.");
                return;
            }

            DisplayPlaces();
            int p1 = InputHelper.ReadInt("Point 1 (index): ", 1, _dataManager.PointsOfInterest.Count);
            int p2 = InputHelper.ReadInt("Point 2 (index): ", 1, _dataManager.PointsOfInterest.Count);

            var point1 = _dataManager.PointsOfInterest[p1 - 1];
            var point2 = _dataManager.PointsOfInterest[p2 - 1];

            double haversine = point1.CalculateDistance(point2);
            double euclidean = point1.CalculateSimplifiedDistance(point2);

            Console.WriteLine($"\nDistance between {point1.Name} and {point2.Name}:");
            Console.WriteLine($"  Haversine (realistic): {haversine:F2} km");
            Console.WriteLine($"  Euclidean (simplified): {euclidean:F2} units");
        }

        // ==================== MOVEMENT SIMULATION ====================

        private void SimulateMovement()
        {
            if (_dataManager.Vehicles.Count == 0)
            {
                Console.WriteLine("No vehicles available.");
                return;
            }

            DisplayVehicles();
            int vIdx = InputHelper.ReadInt("Choose vehicle (index): ", 1, _dataManager.Vehicles.Count);
            var vehicle = _dataManager.Vehicles[vIdx - 1];

            int act = InputHelper.ReadInt("1. Accelerate  2. Brake\nChoice: ", 1, 2);
            if (act == 1)
            {
                vehicle.Accelerate();
                Console.WriteLine("Vehicle accelerating...");
            }
            else
            {
                vehicle.Brake();
                Console.WriteLine("Braking in progress...");
            }
            Console.WriteLine(vehicle);

            // Show detailed status via polymorphism
            Console.WriteLine($"Status: {vehicle.GetDetailedStatus()}");
        }

        // ==================== TRIPS ====================

        private void CreateTrip()
        {
            Console.WriteLine("--- Create a trip ---");

            if (_dataManager.Vehicles.Count == 0 || _dataManager.PointsOfInterest.Count < 2)
            {
                Console.WriteLine("You need at least 1 vehicle and 2 points of interest to create a trip.");
                return;
            }

            DisplayVehicles();
            int vIdx = InputHelper.ReadInt("Vehicle (index): ", 1, _dataManager.Vehicles.Count);

            DisplayPlaces();
            int p1 = InputHelper.ReadInt("Start point (index): ", 1, _dataManager.PointsOfInterest.Count);
            int p2 = InputHelper.ReadInt("End point (index): ", 1, _dataManager.PointsOfInterest.Count);

            if (p1 == p2)
            {
                Console.WriteLine("Error: start and end points must be different.");
                return;
            }

            DateTime date = InputHelper.ReadDateTime("Departure date (yyyy-MM-dd HH:mm): ");

            // Traffic selection
            Console.WriteLine("Traffic conditions:");
            Console.WriteLine("  0. Fluid");
            Console.WriteLine("  1. Dense");
            Console.WriteLine("  2. Congested");
            Console.WriteLine("  3. Blocked");
            int trafficChoice = InputHelper.ReadInt("Traffic state: ", 0, 3);
            TrafficState traffic = (TrafficState)trafficChoice;

            var trip = new Trip
            {
                Vehicle = _dataManager.Vehicles[vIdx - 1],
                StartPoint = _dataManager.PointsOfInterest[p1 - 1],
                EndPoint = _dataManager.PointsOfInterest[p2 - 1],
                DepartureDate = date,
                Traffic = traffic
            };
            _dataManager.AddTrip(trip);
            Console.WriteLine("Trip created!");
            Console.WriteLine(trip);
        }

        private void DisplayTrips()
        {
            Console.WriteLine("--- Trip List ---");
            if (_dataManager.Trips.Count == 0)
            {
                Console.WriteLine("No trips registered.");
                return;
            }
            for (int i = 0; i < _dataManager.Trips.Count; i++)
            {
                Console.WriteLine($"Trip #{i + 1}");
                Console.WriteLine(_dataManager.Trips[i]);
                Console.WriteLine("-------------------");
            }
        }

        // ==================== ADVANCED SEARCH ====================

        private void SearchVehicles()
        {
            Console.WriteLine("--- Search Vehicles ---");
            Console.WriteLine("1. By type (Car/Truck/HybridCar)");
            Console.WriteLine("2. By brand");
            Console.WriteLine("3. By minimum speed");
            Console.WriteLine("4. By minimum battery (HybridCar only)");
            Console.WriteLine("5. By minimum fuel (HybridCar only)");
            int option = InputHelper.ReadInt("Search option: ", 1, 5);

            List<Vehicle> results;
            switch (option)
            {
                case 1:
                    string type = InputHelper.ReadString("Vehicle type (Car/Truck/HybridCar): ");
                    results = _searchService.SearchByType(type);
                    DisplaySearchResults(results);
                    break;
                case 2:
                    string brand = InputHelper.ReadString("Brand to search: ");
                    results = _searchService.SearchByBrand(brand);
                    DisplaySearchResults(results);
                    break;
                case 3:
                    int speed = InputHelper.ReadInt("Minimum speed (km/h): ");
                    results = _searchService.FilterByMinSpeed(speed);
                    DisplaySearchResults(results);
                    break;
                case 4:
                    double battery = InputHelper.ReadPositiveDouble("Minimum battery (%): ");
                    var batteryResults = _searchService.FilterByMinBattery(battery);
                    DisplaySearchResults(new List<Vehicle>(batteryResults));
                    break;
                case 5:
                    double fuel = InputHelper.ReadPositiveDouble("Minimum fuel (L): ");
                    var fuelResults = _searchService.FilterByMinFuel(fuel);
                    DisplaySearchResults(new List<Vehicle>(fuelResults));
                    break;
            }
        }

        private void DisplaySearchResults(List<Vehicle> results)
        {
            if (results.Count == 0)
            {
                Console.WriteLine("No vehicles match your criteria.");
                return;
            }
            Console.WriteLine($"Found {results.Count} result(s):");
            foreach (var v in results)
            {
                Console.WriteLine(v.GetDetailedStatus());
                Console.WriteLine("---");
            }
        }

        private void SearchPlaces()
        {
            Console.WriteLine("--- Search Places ---");
            Console.WriteLine("1. By name");
            Console.WriteLine("2. Find nearby (within radius)");
            int option = InputHelper.ReadInt("Search option: ", 1, 2);

            if (option == 1)
            {
                string name = InputHelper.ReadString("Name to search: ");
                var results = _searchService.SearchPoiByName(name);
                if (results.Count == 0)
                {
                    Console.WriteLine("No places match your search.");
                    return;
                }
                foreach (var p in results)
                {
                    Console.WriteLine(p);
                    Console.WriteLine("---");
                }
            }
            else
            {
                if (_dataManager.PointsOfInterest.Count == 0)
                {
                    Console.WriteLine("No points of interest registered.");
                    return;
                }
                DisplayPlaces();
                int idx = InputHelper.ReadInt("Reference point (index): ", 1, _dataManager.PointsOfInterest.Count);
                double radius = InputHelper.ReadPositiveDouble("Radius (km): ");
                var reference = _dataManager.PointsOfInterest[idx - 1];
                var results = _searchService.FindNearby(reference, radius);
                if (results.Count == 0)
                {
                    Console.WriteLine($"No places found within {radius} km of {reference.Name}.");
                    return;
                }
                Console.WriteLine($"Places within {radius} km of {reference.Name}:");
                foreach (var p in results)
                {
                    double dist = reference.CalculateDistance(p);
                    Console.WriteLine($"  {p.Name} ({dist:F2} km)");
                }
            }
        }

        // ==================== FUEL / BATTERY MANAGEMENT ====================

        private void ManageFuelBattery()
        {
            // Filter hybrid cars
            var hybrids = new List<HybridCar>();
            for (int i = 0; i < _dataManager.Vehicles.Count; i++)
            {
                if (_dataManager.Vehicles[i] is HybridCar hc)
                    hybrids.Add(hc);
            }

            if (hybrids.Count == 0)
            {
                Console.WriteLine("No hybrid vehicles available.");
                return;
            }

            Console.WriteLine("--- Hybrid Vehicles ---");
            for (int i = 0; i < hybrids.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] {hybrids[i]}");
                Console.WriteLine("---");
            }

            int idx = InputHelper.ReadInt("Choose hybrid vehicle (index): ", 1, hybrids.Count);
            var selected = hybrids[idx - 1];

            int action = InputHelper.ReadInt("1. Refuel  2. Recharge battery\nChoice: ", 1, 2);
            if (action == 1)
            {
                double amount = InputHelper.ReadPositiveDouble("Fuel to add (L): ");
                selected.Refuel(amount);
                Console.WriteLine($"Refueled! Fuel level: {selected.FuelLevel}L");
            }
            else
            {
                double amount = InputHelper.ReadPositiveDouble("Battery to add (%): ");
                selected.Recharge(amount);
                Console.WriteLine($"Recharged! Battery level: {selected.BatteryLevel}%");
            }
        }
    }
}
