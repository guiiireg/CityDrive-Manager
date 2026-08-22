using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using CityDriveManager.Data;
using CityDriveManager.Controllers;
using CityDriveManager.Models;

namespace CityDriveManager.Tests.Controllers
{
    public class ControllersTests
    {
        private AppDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task VehiclesController_GetAndPost_WorksCorrectly()
        {
            using var context = GetInMemoryDbContext();
            var controller = new VehiclesController(context);

            var car = new Car { Brand = "Tesla", Model = "Model S", Color = "Red", CurrentSpeed = 0 };
            var result = await controller.PostCar(car);

            var createdAtResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnedCar = Assert.IsType<Car>(createdAtResult.Value);
            Assert.Equal("Tesla", returnedCar.Brand);

            var getResult = await controller.GetVehicles();
            Assert.Single(getResult.Value!);
        }

        [Fact]
        public async Task PointsOfInterestController_GetAndPost_WorksCorrectly()
        {
            using var context = GetInMemoryDbContext();
            var controller = new PointsOfInterestController(context);

            var campus = new Campus { Name = "Ynov Campus", Latitude = 45.75, Longitude = 4.85, Capacity = 1000 };
            var result = await controller.PostCampus(campus);

            var createdAtResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnedCampus = Assert.IsType<Campus>(createdAtResult.Value);
            Assert.Equal("Ynov Campus", returnedCampus.Name);

            var getResult = await controller.GetPointsOfInterest();
            Assert.Single(getResult.Value!);
        }

        [Fact]
        public async Task TripsController_GetAndPost_WorksCorrectly()
        {
            using var context = GetInMemoryDbContext();
            
            var car = new Car { Brand = "BMW", Model = "M3" };
            var startPoi = new Campus { Name = "Start Campus", Latitude = 45.0, Longitude = 4.0 };
            var endPoi = new HistoricalMonument { Name = "Monument", Latitude = 45.1, Longitude = 4.1, BuildYear = 1800 };
            
            context.Vehicles.Add(car);
            context.PointsOfInterest.AddRange(startPoi, endPoi);
            await context.SaveChangesAsync();

            var controller = new TripsController(context);
            var trip = new Trip
            {
                VehicleId = car.Id,
                StartPointId = startPoi.Id,
                EndPointId = endPoi.Id,
                DepartureDate = DateTime.Now,
                Traffic = TrafficState.Fluid
            };

            var result = await controller.PostTrip(trip);
            Assert.IsType<CreatedAtActionResult>(result.Result);

            var getResult = await controller.GetTrips();
            Assert.Single(getResult.Value!);
        }
    }
}
