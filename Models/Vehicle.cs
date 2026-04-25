using System;

namespace CityDriveManager.Models
{
    public abstract class Vehicle
    {
        public string Brand { get; set; }
        public string Color { get; set; }
        public int CurrentSpeed { get; set; }

        public virtual void Accelerate()
        {
            CurrentSpeed += 10;
        }

        public virtual void Brake()
        {
            CurrentSpeed = Math.Max(0, CurrentSpeed - 10);
        }

        public override string ToString()
        {
            return $"Brand: {Brand}\nColor: {Color}\nCurrent speed: {CurrentSpeed} km/h";
        }
    }
}
