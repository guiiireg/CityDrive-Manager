# City Drive Manager

[cite-start]City Drive Manager is a Smart City internal tool designed to manage a vehicle fleet, register urban points of interest, and simulate trips between locations via a console interface[cite: 776, 778, 782].

---

## Getting Started

### Prerequisites

- A .NET 10.0 SDK.
- A C# IDE (VS Code, Sublime Text, Neovim, etc.)

### Installation & Execution

1. Clone the repository.
2. Navigate to the project root.
3. Run the application:

```bash
dotnet run
```

or

```bash
dotnet run --project CityDrive-Manager/CityDriveManager.csproj
```

---

### Project Architecture

The project follows a layered Architecture to ensure a clean separation of concerns:

### Models

Contains the core business logic and domain entities:

- Vehicle.cs: Abstract base class for all transport types.
- PointOfInterest.cs: Base class for geographic locations.
- Specialized Classes: Car, Truck, HybridCar, Campus and HistoricalMonument.
- Trip.cs: Logic for calculating distances and trip durations.

### Services

- DataManager.cs: Centralizes data persistance using specialized collections like `List<T>` to manage vehicles, places and trips.

### UI

- Menu.cs: Handles the interactive console loop, user input validation (via `TryPars`), and data display.

---

### Curent Features

- Vehicle Management: Add and monitor Thermal, Electric or Hybrid vehicles.
- Smart POI: Register Campuses or Monuments with automatic Google Maps URL generation.
- Physics Simulation: Real-time acceleration and braking simulation affecting speed, battery levels, and fuel consumption.
- Trip Planning: Create tips between points with estimated travel time based on average speed of 50km/h.
- Data Sanitization: Automatic string trimming and uppercase conversion for all user inputs.

---

### Tech Stack

- Language: C# / .NET 10
- Paradigme: Object-Oriented Programming (Inheritance, Interfaces, Polymorphism)
- Data Structures: Generic Collections (`List<T>`)
