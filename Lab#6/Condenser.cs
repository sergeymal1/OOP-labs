using System;

namespace Lab6_SmartFridge
{
    // ==== Клас Конденсатор (композиція) ====
    class Condenser
    {
        private bool isClean;
        private double airFlowRate;

        public Condenser()
        {
            isClean = true;
            airFlowRate = 100.0;
        }

        public bool IsClean
        {
            get { return isClean; }
        }

        public double AirFlowRate
        {
            get { return airFlowRate; }
        }

        public void Clean()
        {
            isClean = true;
            airFlowRate = 100.0;
            Console.WriteLine("-> Конденсатор очищено, потік повітря відновлено");
        }

        public void MakeDirty()
        {
            if (isClean)
            {
                isClean = false;
                airFlowRate = 60.0;
                Console.WriteLine("-> Конденсатор забруднився, ефективність знижено");
            }
        }

        public double CalculateEfficiency()
        {
            return airFlowRate;
        }
    }
}