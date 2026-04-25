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

        public double GetDistance()
        {
            return StartPoint.CalculateDistance(EndPoint);
        }

        public double GetDurationInMinutes()
        {
            return (GetDistance() / AVERAGE_SPEED) * 60;
        }

        public override string ToString()
        {
            return $"Vehicle: {Vehicle.Brand}\nFrom: {StartPoint.Name}\nTo: {EndPoint.Name}\nDistance: {GetDistance():F2} km\nEstimated duration: {Math.Round(GetDurationInMinutes())} minutes\nDeparture: {DepartureDate:dd/MM/yyyy HH:mm}";
        }
    }
}
