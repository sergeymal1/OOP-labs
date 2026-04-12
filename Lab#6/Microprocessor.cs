using System;

namespace Lab6_SmartFridge
{
    // ==== Клас Мікропроцесор (композиція) ====
    class Microprocessor
    {
        private string model;
        private string firmwareVersion;
        private bool isConnectedToWiFi;
        private bool voiceRecognitionEnabled;

        public Microprocessor()
        {
            model = "Samsung SmartCore v2";
            firmwareVersion = "2.1.0";
            isConnectedToWiFi = false;
            voiceRecognitionEnabled = true;
        }

        public string Model
        {
            get { return model; }
        }

        public string FirmwareVersion
        {
            get { return firmwareVersion; }
        }

        public bool IsConnectedToWiFi
        {
            get { return isConnectedToWiFi; }
        }

        public bool VoiceRecognitionEnabled
        {
            get { return voiceRecognitionEnabled; }
            set { voiceRecognitionEnabled = value; }
        }

        public void ConnectToWiFi()
        {
            Console.WriteLine("-> Спроба підключення до Wi-Fi...");
            isConnectedToWiFi = true;
            Console.WriteLine("-> Підключено до мережі Wi-Fi");
        }

        public string ProcessVoiceCommand(string command)
        {
            if (!voiceRecognitionEnabled)
            {
                return "Розпізнавання голосу вимкнено";
            }

            Console.WriteLine("-> Обробка голосової команди: \"" + command + "\"");

            string lowerCommand = command.ToLower();

            if (lowerCommand.Contains("температур"))
                return "TEMP_REQUEST";
            else if (lowerCommand.Contains("відкрий") || lowerCommand.Contains("відчини"))
                return "DOOR_OPEN";
            else if (lowerCommand.Contains("закрий") || lowerCommand.Contains("зачини"))
                return "DOOR_CLOSE";
            else if (lowerCommand.Contains("продукт") || lowerCommand.Contains("вміст"))
                return "SHOW_PRODUCTS";
            else
                return "UNKNOWN";
        }

        public void UpdateFirmware()
        {
            Console.WriteLine("-> Оновлення прошивки...");
            firmwareVersion = "2.1.1";
            Console.WriteLine("-> Прошивку оновлено до версії " + firmwareVersion);
        }

        public void ShowInfo()
        {
            Console.WriteLine("   Модель: " + model);
            Console.WriteLine("   Прошивка: " + firmwareVersion);
            Console.WriteLine("   Wi-Fi: " + (isConnectedToWiFi ? "підключено" : "відключено"));
            Console.WriteLine("   Голос: " + (voiceRecognitionEnabled ? "увімкнено" : "вимкнено"));
        }
    }
}