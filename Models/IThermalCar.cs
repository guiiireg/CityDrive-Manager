namespace CityDriveManager.Models
{
    public interface IThermalCar
    {
        double FuelLevel { get; set; }
        void Refuel(double amount);
    }
}
