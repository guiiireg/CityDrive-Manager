# 📦 Models

## Class Hierarchy

```mermaid
classDiagram
    class Vehicle {
        <<abstract>>
        +string Brand
        +string Color
        +int CurrentSpeed
        +Accelerate()* void
        +Brake()* void
        +GetVehicleType()* string
        +GetDetailedStatus()* string
    }

    class Car {
        +string Model
        +GetVehicleType() string
        +GetDetailedStatus() string
    }

    class Truck {
        +double Tonnage
        +GetVehicleType() string
        +GetDetailedStatus() string
    }

    class HybridCar {
        +double BatteryLevel
        +double FuelLevel
        +Refuel(double) void
        +Recharge(double) void
        +Accelerate() void
        +GetVehicleType() string
        +GetDetailedStatus() string
    }

    class IThermalCar {
        <<interface>>
        +double FuelLevel
        +Refuel(double) void
    }

    class IElectricCar {
        <<interface>>
        +double BatteryLevel
        +Recharge(double) void
    }

    Vehicle <|-- Car
    Vehicle <|-- Truck
    Vehicle <|-- HybridCar
    IThermalCar <|.. HybridCar
    IElectricCar <|.. HybridCar
```

---

## Points of Interest Hierarchy

```mermaid
classDiagram
    class PointOfInterest {
        +string Name
        +double Latitude
        +double Longitude
        +GetGoogleMapsUrl() string
        +CalculateDistance(PointOfInterest) double
        +CalculateSimplifiedDistance(PointOfInterest) double
    }

    class Campus {
        +int Capacity
    }

    class HistoricalMonument {
        +int BuildYear
    }

    PointOfInterest <|-- Campus
    PointOfInterest <|-- HistoricalMonument
```

---

## Trip & Traffic

```mermaid
classDiagram
    class Trip {
        +Vehicle Vehicle
        +PointOfInterest StartPoint
        +PointOfInterest EndPoint
        +DateTime DepartureDate
        +TrafficState Traffic
        +GetDistance() double
        +GetDurationInMinutes() double
        +GetEffectiveSpeed() double
        +GetTrafficMultiplier() double
        +GetElapsedTime() TimeSpan
    }

    class TrafficState {
        <<enumeration>>
        Fluid
        Dense
        Congested
        Blocked
    }

    Trip --> Vehicle
    Trip --> PointOfInterest
    Trip --> TrafficState
```

---

## Distance Calculator

```mermaid
classDiagram
    class DistanceCalculator {
        <<static>>
        -double EARTH_RADIUS_KM = 6371.0
        +Haversine(lat1, lon1, lat2, lon2) double
        +Euclidean(lat1, lon1, lat2, lon2) double
        -ToRadians(degrees) double
    }

    PointOfInterest ..> DistanceCalculator : uses
```

### Haversine Formula

The Haversine formula calculates the great-circle distance between two points on a sphere:

```
a = sin²(Δlat/2) + cos(lat1) · cos(lat2) · sin²(Δlon/2)
c = 2 · atan2(√a, √(1−a))
d = R · c
```

Where **R = 6,371 km** (Earth's radius).

---

## Polymorphism in Action

The `Vehicle` class defines two **abstract methods** that each subclass must override:

| Method                | Car                 | Truck                 | HybridCar                                  |
| --------------------- | ------------------- | --------------------- | ------------------------------------------ |
| `GetVehicleType()`    | `"Car"`             | `"Truck"`             | `"HybridCar"`                              |
| `GetDetailedStatus()` | Shows brand + model | Shows brand + tonnage | Shows brand + battery + fuel + energy mode |

This allows the `SearchService` and `Menu` to call these methods polymorphically without type checking.

---

## HybridCar Energy Logic

```mermaid
flowchart TD
    A[Accelerate Called] --> B{Battery > 0?}
    B -->|Yes| C[Speed +10 km/h]
    C --> D[Battery -2%]
    B -->|No| E{Fuel > 0?}
    E -->|Yes| F[Speed +10 km/h]
    F --> G[Fuel -1L]
    E -->|No| H[Cannot accelerate!]
```

---

<p align="center">
  <a href="../README.md">← Back to Main README</a>
</p>
