using Xunit;
using CityDriveManager.Models;

namespace CityDriveManager.Tests.Models
{
    public class ModelsTests
    {
        [Fact]
        public void Vehicle_AccelerateAndBrake_UpdatesSpeedCorrectly()
        {
            var car = new Car { Brand = "Tesla", Model = "Model 3", CurrentSpeed = 0 };
            
            car.Accelerate();
            Assert.Equal(10, car.CurrentSpeed);

            car.Brake();
            Assert.Equal(0, car.CurrentSpeed);

            car.Brake(); // Should not go below 0
            Assert.Equal(0, car.CurrentSpeed);
        }

        [Fact]
        public void HybridCar_Accelerate_ConsumesEnergyCorrectly()
        {
            var hybrid = new HybridCar
            {
                Brand = "Toyota",
                BatteryLevel = 10,
                FuelLevel = 50,
                CurrentSpeed = 0
            };

            hybrid.Accelerate();
            Assert.Equal(8, hybrid.BatteryLevel);
            Assert.Equal(50, hybrid.FuelLevel);

            // Deplete battery
            hybrid.BatteryLevel = 0;
            hybrid.Accelerate();
            Assert.Equal(0, hybrid.BatteryLevel);
            Assert.Equal(49, hybrid.FuelLevel);
        }

        [Fact]
        public void DistanceCalculator_Haversine_CalculatesCorrectDistance()
        {
            // Paris to Lyon distance approx ~390 km
            double lat1 = 48.8566, lon1 = 2.3522;
            double lat2 = 45.7640, lon2 = 4.8357;

            double distance = DistanceCalculator.Haversine(lat1, lon1, lat2, lon2);

            Assert.InRange(distance, 385, 395);
        }

        [Fact]
        public void Trip_GetDurationInMinutes_CalculatesWithTraffic()
        {
            var start = new PointOfInterest { Name = "Start", Latitude = 48.8566, Longitude = 2.3522 };
            var end = new PointOfInterest { Name = "End", Latitude = 48.8566, Longitude = 2.4522 }; // ~7.4 km
            var vehicle = new Car { Brand = "Peugeot", Model = "208" };

            var tripFluid = new Trip
            {
                Vehicle = vehicle,
                StartPoint = start,
                EndPoint = end,
                Traffic = TrafficState.Fluid,
                DepartureDate = DateTime.Now
            };

            var tripBlocked = new Trip
            {
                Vehicle = vehicle,
                StartPoint = start,
                EndPoint = end,
                Traffic = TrafficState.Blocked,
                DepartureDate = DateTime.Now
            };

            Assert.True(tripBlocked.GetDurationInMinutes() > tripFluid.GetDurationInMinutes());
        }
    }
}
