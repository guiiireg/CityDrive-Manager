# 🏗️ Architecture

## Overview

The project follows a **3-layer architecture** that separates concerns cleanly:

```mermaid
graph TB
    subgraph UI ["🖥️ UI Layer"]
        Menu["Menu.cs"]
        Input["InputHelper.cs"]
    end

    subgraph Services ["⚙️ Services Layer"]
        DM["DataManager"]
        SS["SearchService"]
        PS["PersistenceService"]
    end

    subgraph Models ["📦 Models Layer"]
        V["Vehicle (abstract)"]
        POI["PointOfInterest"]
        T["Trip"]
        DC["DistanceCalculator"]
        TS["TrafficState"]
    end

    Menu --> Input
    Menu --> DM
    Menu --> SS
    Menu --> PS
    SS --> DM
    PS --> DM
    DM --> V
    DM --> POI
    DM --> T
    T --> POI
    T --> V
    T --> TS
    POI --> DC
```

---

## Layer Responsibilities

|    Layer     | Role                                              | Files                                                         |
| :----------: | ------------------------------------------------- | ------------------------------------------------------------- |
|    **UI**    | User interaction, input validation, display       | `Menu.cs`, `InputHelper.cs`                                   |
| **Services** | Business logic, data storage, search, persistence | `DataManager.cs`, `SearchService.cs`, `PersistenceService.cs` |
|  **Models**  | Domain entities, interfaces, enums, calculations  | All model classes                                             |

---

## Data Flow

```mermaid
sequenceDiagram
    participant U as User
    participant M as Menu
    participant IH as InputHelper
    participant DM as DataManager
    participant PS as PersistenceService

    U->>M: Selects action (1-14)
    M->>IH: Validates input
    IH-->>M: Clean, typed value
    M->>DM: Add/Read data
    DM-->>M: Result
    M-->>U: Display output

    Note over M,PS: On startup
    PS->>DM: Load from JSON

    Note over M,PS: On exit (option 14)
    M->>PS: Save to JSON
```

---

## Design Patterns Used

| Pattern                   | Where                                 | Why                                                                        |
| ------------------------- | ------------------------------------- | -------------------------------------------------------------------------- |
| **Abstract Class**        | `Vehicle`                             | Force subclasses to implement `GetVehicleType()` and `GetDetailedStatus()` |
| **Interface Segregation** | `IThermalCar`, `IElectricCar`         | `HybridCar` composes both capabilities                                     |
| **DTO Pattern**           | `PersistenceService`                  | Clean serialization without exposing domain internals                      |
| **Service Layer**         | `SearchService`, `PersistenceService` | Isolate logic from UI                                                      |
| **Input Sanitization**    | `InputHelper`                         | Centralized validation with retry loops                                    |

---

## Collection Strategy

```mermaid
graph LR
    subgraph DataManager
        L["List&lt;T&gt;"]
        D["Dictionary&lt;string, int&gt;"]
        H["HashSet&lt;string&gt;"]
    end

    L -->|Ordered storage| V["Vehicles, POIs, Trips"]
    D -->|Type counting| C["Vehicle type → count"]
    H -->|Duplicate prevention| N["POI names uniqueness"]
```

|        Collection         | Purpose                                        |
| :-----------------------: | ---------------------------------------------- |
|      `List<Vehicle>`      | Ordered, indexed access to vehicles            |
|  `List<PointOfInterest>`  | Ordered, indexed access to POIs                |
|       `List<Trip>`        | Ordered trip history                           |
| `Dictionary<string, int>` | Vehicle count per type (Car, Truck, HybridCar) |
|     `HashSet<string>`     | Prevents duplicate POI names                   |

---

<p align="center">
  <a href="../README.md">← Back to Main README</a>
</p>
