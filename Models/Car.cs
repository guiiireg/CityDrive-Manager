namespace CityDriveManager.Models
{
    public class Car : Vehicle
    {
        public string Model { get; set; } = string.Empty;

        public override string GetVehicleType() => "Car";

        public override string GetDetailedStatus()
        {
            return $"Type: Car | {Brand} {Model} | {CurrentSpeed} km/h";
        }

        public override string ToString()
        {
            return $"[CAR]\n{base.ToString()}\nModel: {Model}";
        }
    }
}
