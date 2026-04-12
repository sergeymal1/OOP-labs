using System;

namespace Lab6_SmartFridge
{
    // ==== Похідний клас Розумний кондиціонер ====
    class SmartAirConditioner : SmartFridge
    {
        private int fanSpeed;
        private string airFlowDirection;
        private double targetHumidity;
        private bool isOutdoorUnitOn;

        public SmartAirConditioner(string brand, string model, int capacity)
            : base(brand, model, capacity)
        {
            fanSpeed = 1;
            airFlowDirection = "Прямо";
            targetHumidity = 50.0;
            isOutdoorUnitOn = false;

            // Змінюємо межі температур для кондиціонера
            Cooling.Thermostat.SetTemperatureLimits(16.0, 30.0);
        }

        public int FanSpeed
        {
            get { return fanSpeed; }
        }

        public string AirFlowDirection
        {
            get { return airFlowDirection; }
        }

        public double TargetHumidity
        {
            get { return targetHumidity; }
        }

        public bool IsOutdoorUnitOn
        {
            get { return isOutdoorUnitOn; }
        }

        public void SetFanSpeed(int speed)
        {
            if (speed < 1 || speed > 5)
            {
                throw new FridgeException("Швидкість вентилятора має бути від 1 до 5");
            }
            fanSpeed = speed;
            Console.WriteLine("-> Швидкість вентилятора встановлено на " + fanSpeed);
        }

        public void SetAirFlow(string direction)
        {
            airFlowDirection = direction;
            Console.WriteLine("-> Напрямок потоку повітря: " + airFlowDirection);
        }

        public void SetHumidity(double humidity)
        {
            if (humidity < 30 || humidity > 80)
            {
                throw new FridgeException("Вологість має бути в межах 30-80%");
            }
            targetHumidity = humidity;
            Console.WriteLine("-> Цільову вологість встановлено на " + targetHumidity + "%");
        }

        public void TurnOnOutdoorUnit()
        {
            isOutdoorUnitOn = true;
            Console.WriteLine("-> Зовнішній блок кондиціонера увімкнено");
        }

        public void TurnOffOutdoorUnit()
        {
            isOutdoorUnitOn = false;
            Console.WriteLine("-> Зовнішній блок кондиціонера вимкнено");
        }

        // Перевизначення методу регулювання температури
        public new void AdjustTemperature(double targetTemp)
        {
            Console.WriteLine("\n--- РЕГУЛЮВАННЯ ТЕМПЕРАТУРИ КОНДИЦІОНЕРА ---");

            if (targetTemp < 16 || targetTemp > 30)
            {
                throw new FridgeException("Температура кондиціонера має бути в межах 16-30°C");
            }

            TurnOnOutdoorUnit();
            base.AdjustTemperature(targetTemp);

            Console.WriteLine("-> Кондиціонер працює в режимі охолодження");
            Console.WriteLine("   Швидкість вентилятора: " + fanSpeed);
            Console.WriteLine("   Напрямок потоку: " + airFlowDirection);
        }

        public void ShowACStatus()
        {
            Console.WriteLine("\n=== СТАН КОНДИЦІОНЕРА ===");
            ShowStatus();
            Console.WriteLine("Швидкість вентилятора: " + fanSpeed);
            Console.WriteLine("Напрямок потоку: " + airFlowDirection);
            Console.WriteLine("Цільова вологість: " + targetHumidity + "%");
            Console.WriteLine("Зовнішній блок: " + (isOutdoorUnitOn ? "увімкнено" : "вимкнено"));
        }
    }
}