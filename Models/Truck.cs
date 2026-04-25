namespace CityDriveManager.Models
{
    public class Truck : Vehicle
    {
        public double Tonnage { get; set; }

        public override string ToString()
        {
            return $"[TRUCK]\n{base.ToString()}\nTonnage: {Tonnage}";
        }
    }
}
