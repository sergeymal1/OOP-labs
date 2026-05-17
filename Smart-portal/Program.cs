using System;
using System.Collections.Generic;
using System.Text;
using SmartPortal.Core;

namespace SmartPortalApp
{
    class Program
    {
        static SmartPortal.Core.SmartPortal portal;
        static Citizen currentUser = null;  // той, хто зараз увійшов

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.GetEncoding(1251);
            Console.InputEncoding = Encoding.GetEncoding(1251);

            // Запуск порталу — файли лежать поруч із програмою
            portal = new SmartPortal.Core.SmartPortal("Київ", "citizens.txt", "appeals.txt");
            Console.WriteLine($"=== Smart-портал міста {portal.CityName} ===\n");

            // Головне меню
            while (true)
            {
                Console.WriteLine("\nОберіть дію:");
                Console.WriteLine("1 — Увійти в систему (за ID)");
                Console.WriteLine("2 — Подати нове звернення");
                Console.WriteLine("3 — Переглянути мої звернення");
                Console.WriteLine("4 — Вийти з акаунту");
                Console.WriteLine("0 — Завершити роботу");
                Console.Write("Ваш вибір: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Login();
                        break;
                    case "2":
                        CreateAppeal();
                        break;
                    case "3":
                        ShowMyAppeals();
                        break;
                    case "4":
                        Logout();
                        break;
                    case "0":
                        Console.WriteLine("До побачення!");
                        return;
                    default:
                        Console.WriteLine("Невідома команда, спробуйте ще раз");
                        break;
                }
            }
        }

        // Вхід за ID — ідентифікація громадянина
        static void Login()
        {
            Console.Write("Введіть ваш ID: ");
            string id = Console.ReadLine();

            Citizen citizen = portal.FindCitizenById(id);

            if (citizen == null)
            {
                Console.WriteLine("Громадянина з таким ID не знайдено");
                return;
            }

            currentUser = citizen;
            Console.WriteLine($"Добрий день, {citizen.FirstName} {citizen.LastName}!");

            // Показуємо історію звернень
            var history = portal.GetAppealsByCitizenId(citizen.Id);
            if (history.Count > 0)
            {
                Console.WriteLine($"\nВаша історія звернень ({history.Count}):");
                foreach (var a in history)
                {
                    Console.WriteLine($"  {a}");
                }
            }
            else
            {
                Console.WriteLine("\nУ вас поки немає звернень");
            }
        }

        // Подання нового звернення
        static void CreateAppeal()
        {
            if (currentUser == null)
            {
                Console.WriteLine("Спочатку увійдіть у систему (пункт 1)");
                return;
            }

            Console.Write("Опишіть вашу проблему: ");
            string content = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(content))
            {
                Console.WriteLine("Текст звернення не може бути порожнім");
                return;
            }

            portal.CreateAppeal(currentUser, content);
            Console.WriteLine("Ваше звернення прийнято!");
        }

        // Перегляд своїх звернень
        static void ShowMyAppeals()
        {
            if (currentUser == null)
            {
                Console.WriteLine("Спочатку увійдіть у систему (пункт 1)");
                return;
            }

            var myAppeals = portal.GetAppealsByCitizenId(currentUser.Id);

            if (myAppeals.Count == 0)
            {
                Console.WriteLine("У вас немає звернень");
                return;
            }

            Console.WriteLine($"\nВаші звернення ({myAppeals.Count}):");
            foreach (var a in myAppeals)
            {
                Console.WriteLine($"  {a}");
            }
        }

        // Вихід із акаунту
        static void Logout()
        {
            if (currentUser != null)
            {
                Console.WriteLine($"До побачення, {currentUser.FirstName}!");
                currentUser = null;
            }
            else
            {
                Console.WriteLine("Ви не входили в систему");
            }
        }
    }
}