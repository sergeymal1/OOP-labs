using System;
using System.Text;

namespace Lab6_SmartFridge
{
    // ==== ГОЛОВНА ПРОГРАМА ====
    class Program
    {
        // Обробник події критичної температури
        static void OnTemperatureCritical(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n!!! ТРИВОГА: " + message + " !!!");
            Console.ResetColor();
        }

        // Обробник події простроченого продукту
        static void OnProductExpired(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n!!! ПОПЕРЕДЖЕННЯ: " + message + " !!!");
            Console.ResetColor();
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.GetEncoding(1251);
            Console.WriteLine("=== ЛАБОРАТОРНА РОБОТА №6 ===");
            Console.WriteLine("Варіант 1: Розумний холодильник\n");

            try
            {
                // Створення холодильника
                SmartFridge fridge = new SmartFridge("Samsung", "SmartCool X1", 10);

                // Підписка на події
                fridge.TemperatureCritical += OnTemperatureCritical;
                fridge.ProductExpired += OnProductExpired;

                // Підключення до Wi-Fi
                fridge.Processor.ConnectToWiFi();
                fridge.Processor.ShowInfo();

                // Додавання продуктів
                Console.WriteLine("\n=== ДОДАВАННЯ ПРОДУКТІВ ===");

                Product milk = new Product("Молоко", "Молочне", DateTime.Now.AddDays(-2), 1);
                Product cheese = new Product("Сир", "Молочне", DateTime.Now.AddDays(5), 1);
                Product meat = new Product("М'ясо", "М'ясне", DateTime.Now.AddDays(1), 2);
                Product vegetables = new Product("Овочі", "Овочі", DateTime.Now.AddDays(7), 3);

                fridge.AddProduct(milk);
                fridge.AddProduct(cheese);
                fridge.AddProduct(meat);
                fridge.AddProduct(vegetables);

                // Перевірка зіпсованих продуктів
                fridge.CheckSpoiledProducts();

                // Показ вмісту
                fridge.ShowInventory();

                // Регулювання температури
                Console.WriteLine("\n=== РЕГУЛЮВАННЯ ТЕМПЕРАТУРИ ===");
                fridge.AdjustTemperature(3.0);

                // Голосові команди
                Console.WriteLine("\n=== ГОЛОСОВІ КОМАНДИ ===");
                fridge.VoiceCommand("Яка температура в холодильнику?");
                fridge.VoiceCommand("Відкрий двері");

                // Видалення прострочених продуктів
                Console.WriteLine("\n=== ВИДАЛЕННЯ ПРОСТРОЧЕНИХ ПРОДУКТІВ ===");
                fridge.RemoveExpiredProducts();
                fridge.ShowInventory();

                // Закриття дверей
                fridge.Cabinet.CloseDoor();

                // Показ стану
                fridge.ShowStatus();

                // Демонстрація роботи з кондиціонером
                Console.WriteLine("\n\n=== РОБОТА З КОНДИЦІОНЕРОМ ===");
                SmartAirConditioner ac = new SmartAirConditioner("LG", "CoolAir 3000", 0);

                ac.TemperatureCritical += OnTemperatureCritical;
                ac.ProductExpired += OnProductExpired;

                ac.SetFanSpeed(3);
                ac.SetAirFlow("Вгору");
                ac.SetHumidity(45.0);
                ac.AdjustTemperature(22.0);
                ac.ShowACStatus();

                ac.TurnOffOutdoorUnit();

                // Демонстрація обробки винятків
                Console.WriteLine("\n=== ДЕМОНСТРАЦІЯ ОБРОБКИ ВИНЯТКІВ ===");

                // Спроба встановити некоректну температуру
                try
                {
                    Console.WriteLine("\nСпроба встановити температуру 50°C:");
                    fridge.AdjustTemperature(50.0);
                }
                catch (FridgeException ex)
                {
                    Console.WriteLine("Виняток перехоплено: " + ex.Message);
                    Console.WriteLine("Час помилки: " + ex.ErrorTime);
                }

                // Спроба додати продукт у заповнений холодильник
                try
                {
                    Console.WriteLine("\nСпроба переповнити холодильник:");
                    SmartFridge smallFridge = new SmartFridge("Mini", "Tiny", 2);
                    smallFridge.AddProduct(new Product("Яйця", "Інше", DateTime.Now.AddDays(10), 10));
                    smallFridge.AddProduct(new Product("Масло", "Молочне", DateTime.Now.AddDays(14), 1));
                    smallFridge.AddProduct(new Product("Сік", "Напої", DateTime.Now.AddDays(20), 1));
                }
                catch (IndexOutOfRangeException ex)
                {
                    Console.WriteLine("Виняток перехоплено: " + ex.Message);
                }
                catch (FridgeException ex)
                {
                    Console.WriteLine("Виняток перехоплено: " + ex.Message);
                }

                // Демонстрація блоку finally
                Console.WriteLine("\n=== ДЕМОНСТРАЦІЯ БЛОКУ FINALLY ===");
                try
                {
                    Console.WriteLine("Спроба встановити швидкість вентилятора 10:");
                    ac.SetFanSpeed(10);
                }
                catch (FridgeException ex)
                {
                    Console.WriteLine("Помилка: " + ex.Message);
                }
                finally
                {
                    Console.WriteLine("-> [finally] Вентилятор повернуто до безпечного режиму (швидкість 1)");
                    ac.SetFanSpeed(1);
                    Console.WriteLine("-> [finally] Кондиціонер вимкнено");
                    ac.TurnOffOutdoorUnit();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nКРИТИЧНА ПОМИЛКА: " + ex.Message);
            }

            Console.WriteLine("\n\n=== РОБОТУ ЗАВЕРШЕНО ===");
            Console.ReadKey();
        }
    }
}