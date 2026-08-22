using Microsoft.EntityFrameworkCore;
using CityDriveManager.Models;

namespace CityDriveManager.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; } = null!;
        public DbSet<Car> Cars { get; set; } = null!;
        public DbSet<Truck> Trucks { get; set; } = null!;
        public DbSet<HybridCar> HybridCars { get; set; } = null!;

        public DbSet<PointOfInterest> PointsOfInterest { get; set; } = null!;
        public DbSet<Campus> Campuses { get; set; } = null!;
        public DbSet<HistoricalMonument> HistoricalMonuments { get; set; } = null!;

        public DbSet<Trip> Trips { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure TPH (Table Per Hierarchy) inheritance for Vehicles
            modelBuilder.Entity<Vehicle>()
                .HasDiscriminator<string>("VehicleType")
                .HasValue<Car>("Car")
                .HasValue<Truck>("Truck")
                .HasValue<HybridCar>("HybridCar");

            // Configure TPH inheritance for PointsOfInterest
            modelBuilder.Entity<PointOfInterest>()
                .HasDiscriminator<string>("PoiType")
                .HasValue<Campus>("Campus")
                .HasValue<HistoricalMonument>("HistoricalMonument");

            // Configure relationships for Trip
            modelBuilder.Entity<Trip>()
                .HasOne(t => t.StartPoint)
                .WithMany()
                .HasForeignKey(t => t.StartPointId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Trip>()
                .HasOne(t => t.EndPoint)
                .WithMany()
                .HasForeignKey(t => t.EndPointId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Trip>()
                .HasOne(t => t.Vehicle)
                .WithMany()
                .HasForeignKey(t => t.VehicleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
