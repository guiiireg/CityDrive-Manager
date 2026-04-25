namespace CityDriveManager.Models
{
    public interface IElectricCar
    {
        double BatteryLevel { get; set; }
        void Recharge(double amount);
    }
}
