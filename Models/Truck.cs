namespace CityDriveManager.Models
{
    public class Truck : Vehicle
    {
        public double Tonnage { get; set; }

        public override string GetVehicleType() => "Truck";

        public override string GetDetailedStatus()
        {
            return $"Type: Truck | {Brand} | {Tonnage}T | {CurrentSpeed} km/h";
        }

        public override string ToString()
        {
            return $"[TRUCK]\n{base.ToString()}\nTonnage: {Tonnage}";
        }
    }
}
