using System;

namespace Lab6_SmartFridge
{
    // ==== Оголошення делегата для подій ====
    public delegate void FridgeAlertHandler(string message);

    // ==== Головний клас Розумний холодильник ====
    class SmartFridge
    {
        // Композиція
        private ThermalCabinet cabinet;
        private CoolingSystem cooling;
        private Microprocessor processor;

        // Агрегація
        private Product[] products;

        private string brand;
        private string model;
        private int capacity;
        private int currentProductCount;

        // Події
        public event FridgeAlertHandler TemperatureCritical;
        public event FridgeAlertHandler ProductExpired;

        // Конструктор
        public SmartFridge(string brand, string model, int capacity)
        {
            this.brand = brand;
            this.model = model;
            this.capacity = capacity;

            // Композиція - створюються всередині
            cabinet = new ThermalCabinet();
            cooling = new CoolingSystem();
            processor = new Microprocessor();

            // Агрегація - створюється порожній масив
            products = new Product[capacity];
            currentProductCount = 0;

            Console.WriteLine("=== Створено розумний холодильник " + brand + " " + model + " ===");
            Console.WriteLine("Місткість: " + capacity + " продуктів\n");
        }

        // Властивості
        public string Brand
        {
            get { return brand; }
        }

        public string Model
        {
            get { return model; }
        }

        public int Capacity
        {
            get { return capacity; }
        }

        public int CurrentProductCount
        {
            get { return currentProductCount; }
        }

        public ThermalCabinet Cabinet
        {
            get { return cabinet; }
        }

        public CoolingSystem Cooling
        {
            get { return cooling; }
        }

        public Microprocessor Processor
        {
            get { return processor; }
        }

        // Методи для роботи з продуктами
        public void AddProduct(Product p)
        {
            Console.WriteLine("\n--- ДОДАВАННЯ ПРОДУКТУ ---");

            if (currentProductCount >= capacity)
            {
                throw new IndexOutOfRangeException("Холодильник заповнений! Неможливо додати продукт.");
            }

            products[currentProductCount] = p;
            currentProductCount++;

            Console.WriteLine("Продукт \"" + p.Name + "\" додано до холодильника");
            Console.WriteLine("Заповненість: " + currentProductCount + "/" + capacity);

            if (p.IsExpired())
            {
                if (ProductExpired != null)
                {
                    ProductExpired("Продукт \"" + p.Name + "\" прострочений!");
                }
            }
        }

        public int RemoveExpiredProducts()
        {
            Console.WriteLine("\n--- ВИДАЛЕННЯ ПРОСТРОЧЕНИХ ПРОДУКТІВ ---");

            int removedCount = 0;
            Product[] newProducts = new Product[capacity];
            int newIndex = 0;

            for (int i = 0; i < currentProductCount; i++)
            {
                if (products[i].IsExpired() || products[i].IsSpoiled)
                {
                    Console.WriteLine("Видалено: " + products[i].Name);
                    removedCount++;
                }
                else
                {
                    newProducts[newIndex] = products[i];
                    newIndex++;
                }
            }

            products = newProducts;
            currentProductCount = newIndex;

            Console.WriteLine("Видалено продуктів: " + removedCount);
            Console.WriteLine("Залишилось: " + currentProductCount);

            return removedCount;
        }

        public void CheckSpoiledProducts()
        {
            Console.WriteLine("\n--- ПЕРЕВІРКА ЗІПСОВАНИХ ПРОДУКТІВ ---");

            bool foundSpoiled = false;

            for (int i = 0; i < currentProductCount; i++)
            {
                if (products[i].IsExpired() && !products[i].IsSpoiled)
                {
                    products[i].MarkAsSpoiled();
                    foundSpoiled = true;

                    if (ProductExpired != null)
                    {
                        ProductExpired("Знайдено зіпсований продукт: " + products[i].Name);
                    }
                }
            }

            if (!foundSpoiled)
            {
                Console.WriteLine("Зіпсованих продуктів не знайдено");
            }
        }

        public void ShowInventory()
        {
            Console.WriteLine("\n--- ВМІСТ ХОЛОДИЛЬНИКА ---");
            Console.WriteLine("Бренд: " + brand + " " + model);
            Console.WriteLine("Продуктів: " + currentProductCount + "/" + capacity);

            if (currentProductCount == 0)
            {
                Console.WriteLine("Холодильник порожній");
                return;
            }

            for (int i = 0; i < currentProductCount; i++)
            {
                Console.WriteLine("\nПродукт #" + (i + 1) + ":");
                products[i].ShowInfo();
            }
        }

        // Методи для керування температурою
        public void AdjustTemperature(double targetTemp)
        {
            Console.WriteLine("\n--- РЕГУЛЮВАННЯ ТЕМПЕРАТУРИ ---");

            try
            {
                cooling.Thermostat.SetTarget(targetTemp);

                if (cooling.Thermostat.NeedsCooling())
                {
                    cooling.StartCooling();
                }

                // Імітація роботи
                for (int i = 0; i < 3; i++)
                {
                    cooling.UpdateState();
                    Console.WriteLine("   Поточна температура: " + cooling.Thermostat.CurrentTemperature.ToString("F1") + "°C");

                    if (cooling.Thermostat.CurrentTemperature <= cooling.Thermostat.TargetTemperature)
                    {
                        cooling.StopCooling();
                        break;
                    }
                }

                // Перевірка на критичну температуру
                if (cooling.Thermostat.CurrentTemperature > 10.0)
                {
                    if (TemperatureCritical != null)
                    {
                        TemperatureCritical("Критична температура в холодильнику: " +
                            cooling.Thermostat.CurrentTemperature.ToString("F1") + "°C");
                    }
                }
            }
            catch (FridgeException ex)
            {
                Console.WriteLine("!!! " + ex.Message);
                cooling.StopCooling();
                throw;
            }
        }

        public void VoiceCommand(string command)
        {
            Console.WriteLine("\n--- ГОЛОСОВА КОМАНДА ---");

            string result = processor.ProcessVoiceCommand(command);

            switch (result)
            {
                case "TEMP_REQUEST":
                    Console.WriteLine("Поточна температура: " + cooling.Thermostat.CurrentTemperature.ToString("F1") + "°C");
                    Console.WriteLine("Цільова температура: " + cooling.Thermostat.TargetTemperature + "°C");
                    break;
                case "DOOR_OPEN":
                    cabinet.OpenDoor();
                    break;
                case "DOOR_CLOSE":
                    cabinet.CloseDoor();
                    break;
                case "SHOW_PRODUCTS":
                    ShowInventory();
                    break;
                default:
                    Console.WriteLine("Команду не розпізнано");
                    break;
            }
        }

        public void ShowStatus()
        {
            Console.WriteLine("\n=== СТАН ХОЛОДИЛЬНИКА ===");
            Console.WriteLine("Бренд: " + brand + " " + model);
            Console.WriteLine("Температура: " + cooling.Thermostat.CurrentTemperature.ToString("F1") + "°C");
            Console.WriteLine("Компресор: " + (cooling.Compressor.IsRunning ? "працює" : "вимкнено"));
            Console.WriteLine("Двері: " + (cabinet.DoorIsOpen ? "відкриті" : "закриті"));
            Console.WriteLine("Продуктів: " + currentProductCount + "/" + capacity);
        }
    }
}