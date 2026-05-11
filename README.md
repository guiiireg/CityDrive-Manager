<p align="center">
  <img src="https://img.icons8.com/color/96/c-sharp-logo-2.png" alt="C# Logo" width="80"/>
</p>

<h1 align="center">🏙️ City Drive Manager</h1>

<p align="center">
  <strong>A smart city console application for managing urban mobility.</strong>
</p>

<p align="center">
  <img src="https://img.shields.io/badge/.NET-10.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt=".NET 10"/>
  <img src="https://img.shields.io/badge/C%23-13-239120?style=for-the-badge&logo=csharp&logoColor=white" alt="C# 13"/>
  <img src="https://img.shields.io/badge/Architecture-Layered-blue?style=for-the-badge" alt="Layered"/>
  <img src="https://img.shields.io/badge/License-MIT-yellow?style=for-the-badge" alt="MIT"/>
</p>

<p align="center">
  Manage a vehicle fleet · Register urban POIs · Simulate trips with traffic
</p>

---

## 📋 Table of Contents

| Section                                       | Description                                      |
| --------------------------------------------- | ------------------------------------------------ |
| [🚀 Getting Started](docs/getting-started.md) | Installation, prerequisites & first run          |
| [🏗️ Architecture](docs/architecture.md)       | Layered architecture, diagrams & design patterns |
| [📦 Models](docs/models.md)                   | Class hierarchy, interfaces & UML diagrams       |
| [⚙️ Services](docs/services.md)               | DataManager, SearchService, PersistenceService   |
| [✨ Features](docs/features.md)               | Base features + all bonus implementations        |

---

## ⚡ Quick Start

```bash
git clone <repo-url>
cd CityDrive-Manager
dotnet run --project CityDriveManager.csproj
```

---

## 🗂️ Project Structure

```
CityDrive-Manager/
├── Models/                    # Domain entities & business logic
│   ├── Vehicle.cs             # Abstract base (polymorphism)
│   ├── Car.cs
│   ├── Truck.cs
│   ├── HybridCar.cs           # IThermalCar + IElectricCar
│   ├── IThermalCar.cs
│   ├── IElectricCar.cs
│   ├── PointOfInterest.cs     # Haversine distance
│   ├── Campus.cs
│   ├── HistoricalMonument.cs
│   ├── Trip.cs                # Traffic simulation
│   ├── TrafficState.cs        # Enum
│   └── DistanceCalculator.cs  # Haversine & Euclidean
├── Services/                  # Business logic layer
│   ├── DataManager.cs         # List<T>, Dictionary, HashSet
│   ├── SearchService.cs       # LINQ filtering
│   └── PersistenceService.cs  # JSON save/load
├── UI/                        # Presentation layer
│   ├── Menu.cs                # 14-option interactive menu
│   └── InputHelper.cs         # Robust input validation
├── docs/                      # Documentation
├── Program.cs                 # Entry point
└── README.md
```

---

## 🛠️ Tech Stack

<p align="center">

|      Technology      |                   Usage                    |
| :------------------: | :----------------------------------------: |
| **C# 13 / .NET 10**  |             Language & Runtime             |
|       **OOP**        | Inheritance, Interfaces, Abstract Classes  |
|   **Collections**    | `List<T>`, `Dictionary<K,V>`, `HashSet<T>` |
|       **LINQ**       |        Advanced search & filtering         |
| **System.Text.Json** |              Data persistence              |
|    **Haversine**     |       Realistic geographic distance        |

</p>

---

## 🎯 Bonus Features

All advanced bonus features have been implemented:

| Bonus                          | Status |
| ------------------------------ | :----: |
| Haversine distance (realistic) |   ✅   |
| Traffic simulation system      |   ✅   |
| JSON save & load               |   ✅   |
| Advanced search & filtering    |   ✅   |
| Robust error handling          |   ✅   |
| Layered architecture           |   ✅   |
| Advanced polymorphism          |   ✅   |

> 📖 See [Features Documentation](docs/features.md) for full details.

---

<p align="center">
  <sub>Built with ❤️ for Ynov B2 — Smart City Project</sub>
</p>
