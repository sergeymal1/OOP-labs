using System;

namespace Lab6_SmartFridge
{
    // ==== Клас Терморегулятор (композиція) ====
    class Thermostat
    {
        private double currentTemperature;
        private double targetTemperature;
        private double minTemperature;
        private double maxTemperature;

        public Thermostat()
        {
            currentTemperature = 5.0;
            targetTemperature = 4.0;
            minTemperature = -2.0;
            maxTemperature = 8.0;
        }

        public double CurrentTemperature
        {
            get { return currentTemperature; }
            set { currentTemperature = value; }
        }

        public double TargetTemperature
        {
            get { return targetTemperature; }
        }

        public double MinTemperature
        {
            get { return minTemperature; }
        }

        public double MaxTemperature
        {
            get { return maxTemperature; }
        }

        public void SetTemperatureLimits(double min, double max)
        {
            minTemperature = min;
            maxTemperature = max;
            Console.WriteLine("-> Межі температури змінено: [" + min + "°C .. " + max + "°C]");
        }

        public void SetTarget(double temp)
        {
            if (temp < minTemperature || temp > maxTemperature)
            {
                throw new FridgeException(
                    string.Format("Температура {0}°C поза допустимим діапазоном [{1}°C .. {2}°C]",
                    temp, minTemperature, maxTemperature));
            }
            targetTemperature = temp;
            Console.WriteLine("-> Цільову температуру встановлено на " + targetTemperature + "°C");
        }

        public double GetCurrent()
        {
            return currentTemperature;
        }

        public bool NeedsCooling()
        {
            return currentTemperature > targetTemperature;
        }

        public void UpdateCurrentTemperature(double change)
        {
            currentTemperature += change;
            if (currentTemperature > 15.0) currentTemperature = 15.0;
            if (currentTemperature < -5.0) currentTemperature = -5.0;
        }
    }
}