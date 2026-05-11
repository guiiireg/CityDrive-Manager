using System;

namespace CityDriveManager.Models
{
    public class Trip
    {
        public const double AVERAGE_SPEED = 50;

        public Vehicle Vehicle { get; set; }
        public PointOfInterest StartPoint { get; set; }
        public PointOfInterest EndPoint { get; set; }
        public DateTime DepartureDate { get; set; }
        public TrafficState Traffic { get; set; } = TrafficState.Fluid;

        public double GetTrafficMultiplier()
        {
            return Traffic switch
            {
                TrafficState.Fluid => 1.0,
                TrafficState.Dense => 0.7,
                TrafficState.Congested => 0.4,
                TrafficState.Blocked => 0.1,
                _ => 1.0
            };
        }

        public double GetEffectiveSpeed()
        {
            return AVERAGE_SPEED * GetTrafficMultiplier();
        }

        public double GetDistance()
        {
            return StartPoint.CalculateDistance(EndPoint);
        }

        public double GetDurationInMinutes()
        {
            double effectiveSpeed = GetEffectiveSpeed();
            if (effectiveSpeed <= 0) return double.MaxValue;
            return (GetDistance() / effectiveSpeed) * 60;
        }

        public TimeSpan GetElapsedTime()
        {
            return DateTime.Now - DepartureDate;
        }

        public override string ToString()
        {
            TimeSpan elapsed = GetElapsedTime();
            string elapsedStr = elapsed.TotalSeconds >= 0
                ? $"{(int)elapsed.TotalHours}h {elapsed.Minutes}m ago"
                : $"in {(int)Math.Abs(elapsed.TotalHours)}h {Math.Abs(elapsed.Minutes)}m";

            return $"Vehicle: {Vehicle.Brand}\n" +
                   $"From: {StartPoint.Name}\n" +
                   $"To: {EndPoint.Name}\n" +
                   $"Distance: {GetDistance():F2} km\n" +
                   $"Traffic: {Traffic}\n" +
                   $"Effective speed: {GetEffectiveSpeed():F0} km/h\n" +
                   $"Estimated duration: {Math.Round(GetDurationInMinutes())} minutes\n" +
                   $"Departure: {DepartureDate:dd/MM/yyyy HH:mm}\n" +
                   $"Elapsed: {elapsedStr}";
        }
    }
}
