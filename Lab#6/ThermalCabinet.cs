using System;

namespace Lab6_SmartFridge
{
    // ==== Клас Ізотермічна шафа (композиція) ====
    class ThermalCabinet
    {
        private string material;
        private double volume;
        private bool doorIsOpen;
        private int shelfCount;
        private bool lightIsOn;

        public ThermalCabinet()
        {
            material = "Метал";
            volume = 350.0;
            doorIsOpen = false;
            shelfCount = 4;
            lightIsOn = false;
        }

        public ThermalCabinet(string material, double volume, int shelfCount)
        {
            this.material = material;
            this.volume = volume;
            this.shelfCount = shelfCount;
            doorIsOpen = false;
            lightIsOn = false;
        }

        public string Material
        {
            get { return material; }
            set { material = value; }
        }

        public double Volume
        {
            get { return volume; }
        }

        public bool DoorIsOpen
        {
            get { return doorIsOpen; }
        }

        public int ShelfCount
        {
            get { return shelfCount; }
        }

        public bool LightIsOn
        {
            get { return lightIsOn; }
        }

        public void OpenDoor()
        {
            doorIsOpen = true;
            lightIsOn = true;
            Console.WriteLine("-> Двері холодильника відкрито, освітлення увімкнено");
        }

        public void CloseDoor()
        {
            doorIsOpen = false;
            lightIsOn = false;
            Console.WriteLine("-> Двері холодильника закрито, освітлення вимкнено");
        }
    }
}