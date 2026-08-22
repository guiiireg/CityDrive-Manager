using System;

namespace CityDriveManager.Models
{
    public abstract class Vehicle
    {
        public int Id { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public int CurrentSpeed { get; set; }

        public virtual void Accelerate()
        {
            CurrentSpeed += 10;
        }

        public virtual void Brake()
        {
            CurrentSpeed = Math.Max(0, CurrentSpeed - 10);
        }

        public abstract string GetVehicleType();

        public abstract string GetDetailedStatus();

        public override string ToString()
        {
            return $"Brand: {Brand}\nColor: {Color}\nCurrent speed: {CurrentSpeed} km/h";
        }
    }
}
