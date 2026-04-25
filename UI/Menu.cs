using System;
using CityDriveManager.Models;
using CityDriveManager.Services;

namespace CityDriveManager.UI
{
    public class Menu
    {
        private readonly DataManager _dataManager;

        public Menu(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public void Show()
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("==============================");
                Console.WriteLine("CITY DRIVE MANAGER - SMART CITY");
                Console.WriteLine("==============================");
                Console.WriteLine("1. Add a point of interest");
                Console.WriteLine("2. Add a vehicle");
                Console.WriteLine("3. Display vehicles");
                Console.WriteLine("4. Display places");
                Console.WriteLine("5. Calculate a distance");
                Console.WriteLine("6. Simulate acceleration / braking");
                Console.WriteLine("7. Create a trip");
                Console.WriteLine("8. Display trips");
                Console.WriteLine("9. Quit");
                Console.Write("Choice: ");

                if (int.TryParse(Console.ReadLine(), out int choice))
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
                        case 9: running = false; Console.WriteLine("Closing program..."); break;
                        default: Console.WriteLine("Invalid choice."); break;
                    }
                }
            }
        }

        private void AddPointOfInterest()
        {
            Console.WriteLine("--- Add a point of interest ---");
            Console.Write("Type (1=Campus, 2=Monument): ");
            if (int.TryParse(Console.ReadLine(), out int type))
            {
                Console.Write("Name: ");
                string name = Console.ReadLine().Trim().ToUpper();
                Console.Write("Latitude: ");
                if (double.TryParse(Console.ReadLine(), out double lat))
                {
                    Console.Write("Longitude: ");
                    if (double.TryParse(Console.ReadLine(), out double lon))
                    {
                        PointOfInterest poi = null;
                        if (type == 1)
                        {
                            Console.Write("Capacity: ");
                            if (int.TryParse(Console.ReadLine(), out int cap))
                            {
                                poi = new Campus { Name = name, Latitude = lat, Longitude = lon, Capacity = cap };
                            }
                        }
                        else if (type == 2)
                        {
                            Console.Write("Build Year: ");
                            if (int.TryParse(Console.ReadLine(), out int year))
                            {
                                poi = new HistoricalMonument { Name = name, Latitude = lat, Longitude = lon, BuildYear = year };
                            }
                        }

                        if (poi != null)
                        {
                            _dataManager.AddPointOfInterest(poi);
                            Console.WriteLine("Point added successfully!");
                        }
                    }
                }
            }
        }

        private void AddVehicle()
        {
            Console.WriteLine("--- Add a vehicle ---");
            Console.Write("Type (1=Car, 2=Truck, 3=HybridCar): ");
            if (int.TryParse(Console.ReadLine(), out int type))
            {
                Console.Write("Brand: ");
                string brand = Console.ReadLine().Trim().ToUpper();
                Console.Write("Color: ");
                string color = Console.ReadLine().Trim().ToUpper();

                Vehicle vehicle = null;
                if (type == 1)
                {
                    Console.Write("Model: ");
                    string model = Console.ReadLine().Trim().ToUpper();
                    vehicle = new Car { Brand = brand, Color = color, Model = model };
                }
                else if (type == 2)
                {
                    Console.Write("Tonnage: ");
                    if (double.TryParse(Console.ReadLine(), out double tonnage))
                    {
                        vehicle = new Truck { Brand = brand, Color = color, Tonnage = tonnage };
                    }
                }
                else if (type == 3)
                {
                    Console.Write("Battery level (%): ");
                    if (double.TryParse(Console.ReadLine(), out double battery))
                    {
                        Console.Write("Fuel level (L): ");
                        if (double.TryParse(Console.ReadLine(), out double fuel))
                        {
                            vehicle = new HybridCar { Brand = brand, Color = color, BatteryLevel = battery, FuelLevel = fuel };
                        }
                    }
                }

                if (vehicle != null)
                {
                    _dataManager.AddVehicle(vehicle);
                    Console.WriteLine("Vehicle added!");
                }
            }
        }

        private void DisplayVehicles()
        {
            Console.WriteLine("--- Vehicle List ---");
            for (int i = 0; i < _dataManager.Vehicles.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] {_dataManager.Vehicles[i]}");
                Console.WriteLine("-------------------");
            }
        }

        private void DisplayPlaces()
        {
            Console.WriteLine("--- Points of Interest ---");
            for (int i = 0; i < _dataManager.PointsOfInterest.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] {_dataManager.PointsOfInterest[i]}");
                Console.WriteLine("-------------------");
            }
        }

        private void CalculateDistance()
        {
            DisplayPlaces();
            Console.Write("Point 1 (index): ");
            if (int.TryParse(Console.ReadLine(), out int p1) && p1 > 0 && p1 <= _dataManager.PointsOfInterest.Count)
            {
                Console.Write("Point 2 (index): ");
                if (int.TryParse(Console.ReadLine(), out int p2) && p2 > 0 && p2 <= _dataManager.PointsOfInterest.Count)
                {
                    var point1 = _dataManager.PointsOfInterest[p1 - 1];
                    var point2 = _dataManager.PointsOfInterest[p2 - 1];
                    double dist = point1.CalculateDistance(point2);
                    Console.WriteLine($"Distance between {point1.Name} and {point2.Name}: {dist:F2} km");
                }
            }
        }

        private void SimulateMovement()
        {
            DisplayVehicles();
            Console.Write("Choose vehicle (index): ");
            if (int.TryParse(Console.ReadLine(), out int vIdx) && vIdx > 0 && vIdx <= _dataManager.Vehicles.Count)
            {
                var vehicle = _dataManager.Vehicles[vIdx - 1];
                Console.WriteLine("1. Accelerate");
                Console.WriteLine("2. Brake");
                Console.Write("Choice: ");
                if (int.TryParse(Console.ReadLine(), out int act))
                {
                    if (act == 1)
                    {
                        vehicle.Accelerate();
                        Console.WriteLine("Vehicle accelerating...");
                    }
                    else if (act == 2)
                    {
                        vehicle.Brake();
                        Console.WriteLine("Braking in progress...");
                    }
                    Console.WriteLine(vehicle);
                }
            }
        }

        private void CreateTrip()
        {
            Console.WriteLine("--- Create a trip ---");
            Console.Write("Vehicle (index): ");
            if (int.TryParse(Console.ReadLine(), out int vIdx) && vIdx > 0 && vIdx <= _dataManager.Vehicles.Count)
            {
                Console.Write("Start point (index): ");
                if (int.TryParse(Console.ReadLine(), out int p1) && p1 > 0 && p1 <= _dataManager.PointsOfInterest.Count)
                {
                    Console.Write("End point (index): ");
                    if (int.TryParse(Console.ReadLine(), out int p2) && p2 > 0 && p2 <= _dataManager.PointsOfInterest.Count)
                    {
                        Console.Write("Departure date (yyyy-MM-dd HH:mm): ");
                        if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
                        {
                            var trip = new Trip
                            {
                                Vehicle = _dataManager.Vehicles[vIdx - 1],
                                StartPoint = _dataManager.PointsOfInterest[p1 - 1],
                                EndPoint = _dataManager.PointsOfInterest[p2 - 1],
                                DepartureDate = date
                            };
                            _dataManager.AddTrip(trip);
                            Console.WriteLine("Trip created!");
                        }
                    }
                }
            }
        }

        private void DisplayTrips()
        {
            Console.WriteLine("--- Trip List ---");
            for (int i = 0; i < _dataManager.Trips.Count; i++)
            {
                Console.WriteLine($"Trip #{i + 1}");
                Console.WriteLine(_dataManager.Trips[i]);
                Console.WriteLine("-------------------");
            }
        }
    }
}
