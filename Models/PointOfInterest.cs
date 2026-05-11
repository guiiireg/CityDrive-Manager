using System;

namespace CityDriveManager.Models
{
    public class PointOfInterest
    {
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public string GetGoogleMapsUrl()
        {
            return $"https://www.google.com/maps?q={Latitude},{Longitude}";
        }

        public double CalculateDistance(PointOfInterest other)
        {
            return Math.Sqrt(Math.Pow(other.Latitude - Latitude, 2) + Math.Pow(other.Longitude - Longitude, 2));
        }

        public override string ToString()
        {
            return $"Name: {Name}\nCoordinates: ({Latitude}, {Longitude})\nGoogle Maps: {GetGoogleMapsUrl()}";
        }
    }
}
