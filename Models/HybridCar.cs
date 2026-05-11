    using System;

namespace CityDriveManager.Models
{
    public class HybridCar : Vehicle, IThermalCar, IElectricCar
    {
        public double BatteryLevel { get; set; }
        public double FuelLevel { get; set; }

        public void Refuel(double amount)
        {
            if (amount < 0) throw new ArgumentException("Refuel amount cannot be negative.");
            FuelLevel += amount;
        }

        public void Recharge(double amount)
        {
            if (amount < 0) throw new ArgumentException("Recharge amount cannot be negative.");
            BatteryLevel = Math.Min(100, BatteryLevel + amount);
        }

        public override void Accelerate()
        {
            if (BatteryLevel <= 0 && FuelLevel <= 0)
            {
                Console.WriteLine("Cannot accelerate: battery and fuel are both empty!");
                return;
            }

            base.Accelerate();
            if (BatteryLevel > 0)
            {
                BatteryLevel = Math.Max(0, BatteryLevel - 2);
            }
            else
            {
                FuelLevel = Math.Max(0, FuelLevel - 1);
            }
        }

        public override string GetVehicleType() => "HybridCar";

        public override string GetDetailedStatus()
        {
            string energySource = BatteryLevel > 0 ? "Electric" : (FuelLevel > 0 ? "Thermal" : "Empty");
            return $"Type: HybridCar | {Brand} | Battery: {BatteryLevel}% | Fuel: {FuelLevel}L | Mode: {energySource} | {CurrentSpeed} km/h";
        }

        public override string ToString()
        {
            return $"[HYBRID CAR]\n{base.ToString()}\nBattery: {BatteryLevel}%\nFuel: {FuelLevel}L";
        }
    }
}
