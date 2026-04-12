using System;

namespace Lab6_SmartFridge
{
    // ==== Клас Компресор (композиція) ====
    class Compressor
    {
        private static Random random = new Random();

        private int power;
        private bool isRunning;
        private double temperature;
        private double maxTemperature;

        public Compressor()
        {
            power = 150;
            isRunning = false;
            temperature = 20.0;
            maxTemperature = 80.0;
        }

        public Compressor(int power, double maxTemperature)
        {
            this.power = power;
            this.maxTemperature = maxTemperature;
            isRunning = false;
            temperature = 20.0;
        }

        public int Power
        {
            get { return power; }
        }

        public bool IsRunning
        {
            get { return isRunning; }
        }

        public double Temperature
        {
            get { return temperature; }
        }

        public double MaxTemperature
        {
            get { return maxTemperature; }
        }

        public void Start()
        {
            if (!isRunning)
            {
                isRunning = true;
                Console.WriteLine("-> Компресор запущено");
            }
        }

        public void Stop()
        {
            if (isRunning)
            {
                isRunning = false;
                Console.WriteLine("-> Компресор зупинено");
            }
        }

        public void UpdateTemperature()
        {
            if (isRunning)
            {
                temperature += 2.5;
                if (temperature > 100) temperature = 100;
            }
            else
            {
                temperature -= 1.0;
                if (temperature < 20) temperature = 20;
            }
        }

        public bool CheckOverheat()
        {
            return temperature >= maxTemperature;
        }

        public void CheckFailure()
        {
            // 5% шанс збою при роботі
            if (isRunning && random.Next(1, 101) <= 5)
            {
                throw new FridgeException("Компресор вийшов з ладу! Потрібен ремонт.");
            }
        }
    }
}