using System;

namespace Lab6_SmartFridge
{
    // ==== Клас Випарник (композиція) ====
    class Evaporator
    {
        private int frostLevel;
        private bool isDefrosting;

        public Evaporator()
        {
            frostLevel = 0;
            isDefrosting = false;
        }

        public int FrostLevel
        {
            get { return frostLevel; }
        }

        public bool IsDefrosting
        {
            get { return isDefrosting; }
        }

        public void UpdateFrostLevel()
        {
            if (!isDefrosting)
            {
                frostLevel += 5;
                if (frostLevel > 100) frostLevel = 100;
                Console.WriteLine("-> Рівень обмерзання випарника: " + frostLevel + "%");
            }
        }

        public void Defrost()
        {
            isDefrosting = true;
            Console.WriteLine("-> Запущено процес розморожування випарника...");

            for (int i = frostLevel; i >= 0; i -= 20)
            {
                frostLevel = i;
                if (frostLevel < 0) frostLevel = 0;
                Console.WriteLine("   Обмерзання: " + frostLevel + "%");
            }

            isDefrosting = false;
            Console.WriteLine("-> Розморожування завершено");
        }
    }
}