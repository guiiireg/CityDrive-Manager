using System;

namespace CityDriveManager.Models
{
    public class HybridCar : Vehicle, IThermalCar, IElectricCar
    {
        public double BatteryLevel { get; set; }
        public double FuelLevel { get; set; }

        public void Refuel(double amount)
        {
            FuelLevel += amount;
        }

        public void Recharge(double amount)
        {
            BatteryLevel += amount;
        }

        public override void Accelerate()
        {
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

        public override string ToString()
        {
            return $"[HYBRID CAR]\n{base.ToString()}\nBattery: {BatteryLevel}%\nFuel: {FuelLevel}L";
        }
    }
}
