namespace CityDriveManager.Models
{
    public class HistoricalMonument : PointOfInterest
    {
        public int BuildYear { get; set; }

        public override string ToString()
        {
            return $"[HISTORICAL MONUMENT]\n{base.ToString()}\nBuilt in: {BuildYear}";
        }
    }
}
