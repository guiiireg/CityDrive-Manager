namespace CityDriveManager.Models
{
    public class Campus : PointOfInterest
    {
        public int Capacity { get; set; }

        public override string ToString()
        {
            return $"[CAMPUS]\n{base.ToString()}\nCapacity: {Capacity}";
        }
    }
}
