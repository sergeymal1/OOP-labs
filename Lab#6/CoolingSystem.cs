using System;

namespace Lab6_SmartFridge
{
    // ==== Клас Система охолодження (композиція) ====
    class CoolingSystem
    {
        private static Random random = new Random();

        private Compressor compressor;
        private Evaporator evaporator;
        private Condenser condenser;
        private Thermostat thermostat;
        private string refrigerant;
        private double refrigerantLevel;

        public CoolingSystem()
        {
            compressor = new Compressor();
            evaporator = new Evaporator();
            condenser = new Condenser();
            thermostat = new Thermostat();
            refrigerant = "R600a";
            refrigerantLevel = 100.0;
        }

        public Compressor Compressor
        {
            get { return compressor; }
        }

        public Evaporator Evaporator
        {
            get { return evaporator; }
        }

        public Condenser Condenser
        {
            get { return condenser; }
        }

        public Thermostat Thermostat
        {
            get { return thermostat; }
        }

        public string Refrigerant
        {
            get { return refrigerant; }
        }

        public double RefrigerantLevel
        {
            get { return refrigerantLevel; }
        }

        public void StartCooling()
        {
            Console.WriteLine("\n--- ЗАПУСК СИСТЕМИ ОХОЛОДЖЕННЯ ---");
            compressor.Start();
        }

        public void StopCooling()
        {
            Console.WriteLine("\n--- ЗУПИНКА СИСТЕМИ ОХОЛОДЖЕННЯ ---");
            compressor.Stop();
        }

        public bool CheckLeak()
        {
            // 3% шанс витоку холодоагенту
            if (random.Next(1, 101) <= 3)
            {
                refrigerantLevel -= 20;
                if (refrigerantLevel < 0) refrigerantLevel = 0;
                Console.WriteLine("!!! УВАГА: Виявлено витік холодоагенту! Рівень: " + refrigerantLevel + "%");
                return true;
            }
            return false;
        }

        public void UpdateState()
        {
            if (compressor.IsRunning)
            {
                compressor.UpdateTemperature();
                evaporator.UpdateFrostLevel();
                thermostat.UpdateCurrentTemperature(-0.5);

                if (!condenser.IsClean)
                {
                    thermostat.UpdateCurrentTemperature(0.2);
                }

                if (compressor.CheckOverheat())
                {
                    throw new FridgeException(
                        string.Format("КОМПРЕСОР ПЕРЕГРІВСЯ! Температура: {0:F1}°C (макс: {1}°C)",
                        compressor.Temperature, compressor.MaxTemperature));
                }
            }
            else
            {
                compressor.UpdateTemperature();
                thermostat.UpdateCurrentTemperature(0.3);
            }

            if (evaporator.FrostLevel >= 80 && !evaporator.IsDefrosting)
            {
                Console.WriteLine("!!! УВАГА: Критичний рівень обмерзання! Запуск авто-розморожування...");
                evaporator.Defrost();
            }
        }
    }
}