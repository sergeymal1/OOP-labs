using System;
using System.Collections.Generic;
using System.Text;
using SmartPortal.Core;

namespace SmartPortalApp
{
    class Program
    {
        static SmartPortal.Core.SmartPortal portal;
        static Citizen currentUser = null;

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.GetEncoding(1251);
            Console.InputEncoding = Encoding.GetEncoding(1251);

            portal = new SmartPortal.Core.SmartPortal("Київ", "citizens.txt", "appeals.txt");
            Console.WriteLine($"=== Smart-портал міста {portal.CityName} ===\n");

            while (true)
            {
                if (currentUser == null)
                    ShowGuestMenu();
                else
                    ShowUserMenu();

                Console.Write("Ваш вибір: ");
                string choice = Console.ReadLine();
                Console.WriteLine();

                if (currentUser == null)
                {
                    switch (choice)
                    {
                        case "1": Login(); break;
                        case "2": ShowAllCitizens(); break;
                        case "3": RegisterNewCitizen(); break;
                        case "0":
                            Console.WriteLine("До побачення!");
                            return;
                        default:
                            Console.WriteLine("Невідома команда");
                            break;
                    }
                }
                else
                {
                    switch (choice)
                    {
                        case "1": CreateAppeal(); break;
                        case "2": ShowMyAppeals(); break;
                        case "3": CheckAppealStatus(); break;
                        case "4": ShowAllCitizens(); break;
                        case "5": Logout(); break;
                        case "0":
                            Console.WriteLine("До побачення!");
                            return;
                        default:
                            Console.WriteLine("Невідома команда");
                            break;
                    }
                }
            }
        }

        static void ShowGuestMenu()
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine($"Вітаємо на порталі міста {portal.CityName}!");
            Console.WriteLine("========================================");
            Console.WriteLine("1 — Увійти в систему (за ID)");
            Console.WriteLine("2 — Подивитись усіх зареєстрованих громадян");
            Console.WriteLine("3 — Зареєструвати нового громадянина");
            Console.WriteLine("0 — Завершити роботу");
        }

        static void ShowUserMenu()
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine($"Добрий день, {currentUser.FirstName}!");
            Console.WriteLine("========================================");
            Console.WriteLine("1 — Подати нове звернення");
            Console.WriteLine("2 — Переглянути мої звернення");
            Console.WriteLine("3 — Переглянути статус конкретного звернення");
            Console.WriteLine("4 — Подивитись усіх зареєстрованих громадян");
            Console.WriteLine("5 — Вийти з акаунту");
            Console.WriteLine("0 — Завершити роботу");
        }

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
            Console.WriteLine($"\nДобрий день, {citizen.FirstName} {citizen.LastName}!");

            var history = portal.GetAppealsByCitizenId(citizen.Id);
            if (history.Count > 0)
            {
                Console.WriteLine($"У вас {history.Count} звернень.");
                Console.WriteLine($"Останнє: \"{history[history.Count - 1].Content}\"");
                Console.WriteLine($"Статус: {history[history.Count - 1].Status}");
            }
            else
            {
                Console.WriteLine("У вас поки немає звернень");
            }
        }

        static void CreateAppeal()
        {
            Console.WriteLine("Вас вітає система подання звернень!");
            Console.WriteLine("Опишіть вашу проблему (одним рядком):");
            Console.Write("> ");
            string content = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(content))
            {
                Console.WriteLine("Текст звернення не може бути порожнім");
                return;
            }

            Appeal appeal = portal.CreateAppeal(currentUser, content);
            Console.WriteLine($"\nВаше звернення зареєстровано під номером {appeal.Id}");
            Console.WriteLine("Очікуйте на відповідь. Статус можна перевірити в меню (пункт 3)");
        }

        static void ShowMyAppeals()
        {
            var myAppeals = portal.GetAppealsByCitizenId(currentUser.Id);

            if (myAppeals.Count == 0)
            {
                Console.WriteLine("У вас немає звернень");
                return;
            }

            Console.WriteLine($"=== Ваші звернення ({myAppeals.Count}) ===");
            foreach (var a in myAppeals)
            {
                Console.WriteLine($"  {a}");
            }
        }

        static void CheckAppealStatus()
        {
            Console.Write("Введіть номер звернення (наприклад A001): ");
            string id = Console.ReadLine();

            var myAppeals = portal.GetAppealsByCitizenId(currentUser.Id);
            Appeal found = null;

            foreach (var a in myAppeals)
            {
                if (a.Id == id)
                {
                    found = a;
                    break;
                }
            }

            if (found == null)
            {
                Console.WriteLine("Звернення з таким номером не знайдено серед ваших");
                return;
            }

            Console.WriteLine($"\nДетальна інформація:");
            Console.WriteLine($"  Номер: {found.Id}");
            Console.WriteLine($"  Зміст: {found.Content}");
            Console.WriteLine($"  Статус: {found.Status}");
            Console.WriteLine($"  Виконавець: {(string.IsNullOrEmpty(found.Executor) ? "не призначено" : found.Executor)}");
            Console.WriteLine($"  Дата: {found.CreatedDate:dd.MM.yyyy}");
        }

        static void RegisterNewCitizen()
        {
            Console.WriteLine("=== Реєстрація нового громадянина ===");
            Console.Write("ID (наприклад C006): ");
            string id = Console.ReadLine();

            if (portal.FindCitizenById(id) != null)
            {
                Console.WriteLine("Громадянин із таким ID вже існує");
                return;
            }

            Console.Write("Ім'я: ");
            string firstName = Console.ReadLine();
            Console.Write("Прізвище: ");
            string lastName = Console.ReadLine();
            Console.Write("Адреса: ");
            string address = Console.ReadLine();
            Console.Write("Телефон: ");
            string phone = Console.ReadLine();
            Console.Write("Email: ");
            string email = Console.ReadLine();

            try
            {
                var citizen = new Citizen(id, firstName, lastName, address, phone, email);
                portal.RegisterCitizen(citizen);
                Console.WriteLine($"Громадянина {citizen} успішно зареєстровано!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
        }

        static void ShowAllCitizens()
        {
            var citizens = portal.Citizens;
            Console.WriteLine($"=== Зареєстровані громадяни ({citizens.Count}) ===");
            foreach (var c in citizens)
            {
                Console.WriteLine($"  {c} | Адреса: {c.Address} | Тел: {c.Phone}");
            }
        }

        static void Logout()
        {
            Console.WriteLine($"До побачення, {currentUser.FirstName}!");
            currentUser = null;
        }
    }
}