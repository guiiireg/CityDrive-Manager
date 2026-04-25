namespace CityDriveManager.Models
{
    public class Car : Vehicle
    {
        public string Model { get; set; }

        public override string ToString()
        {
            return $"[CAR]\n{base.ToString()}\nModel: {Model}";
        }
    }
}
