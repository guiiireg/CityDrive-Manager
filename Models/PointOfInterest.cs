using System;

namespace CityDriveManager.Models
{
    public class PointOfInterest
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public string GetGoogleMapsUrl()
        {
            return $"https://www.google.com/maps?q={Latitude},{Longitude}";
        }

        public double CalculateDistance(PointOfInterest other)
        {
            return DistanceCalculator.Haversine(Latitude, Longitude, other.Latitude, other.Longitude);
        }

        public double CalculateSimplifiedDistance(PointOfInterest other)
        {
            return DistanceCalculator.Euclidean(Latitude, Longitude, other.Latitude, other.Longitude);
        }

        public override string ToString()
        {
            return $"Name: {Name}\nCoordinates: ({Latitude}, {Longitude})\nGoogle Maps: {GetGoogleMapsUrl()}";
        }
    }
}
