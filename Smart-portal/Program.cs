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

            portal = new SmartPortal.Core.SmartPortal(Messages.WelcomeMessage.Split(' ')[3],
                         Constants.CitizensFileName, Constants.AppealsFileName);
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
                            Console.WriteLine(Messages.Goodbye + "!");
                            return;
                        default:
                            Console.WriteLine(Messages.UnknownCommand);
                            break;
                    }
                }
                else
                {
                    switch (choice)
                    {
                        case "1": CreateAppeal(); break;
                        case "2": CreateUrgentAppeal(); break;
                        case "3": ShowMyAppeals(); break;
                        case "4": CheckAppealStatus(); break;
                        case "5": ShowAllCitizens(); break;
                        case "6": Logout(); break;
                        case "0":
                            Console.WriteLine(Messages.Goodbye + "!");
                            return;
                        default:
                            Console.WriteLine(Messages.UnknownCommand);
                            break;
                    }
                }
            }
        }

        static void ShowGuestMenu()
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine($"{Messages.WelcomeMessage} {portal.CityName}!");
            Console.WriteLine("========================================");
            Console.WriteLine("1 — Увійти в систему (за ID)");
            Console.WriteLine("2 — Подивитись усіх зареєстрованих громадян");
            Console.WriteLine("3 — Зареєструвати нового громадянина");
            Console.WriteLine("0 — Завершити роботу");
        }

        static void ShowUserMenu()
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine($"{Messages.GoodMorning}, {currentUser.FirstName}!");
            Console.WriteLine("========================================");
            Console.WriteLine("1 — Подати звичайне звернення");
            Console.WriteLine("2 — Подати термінове звернення");
            Console.WriteLine("3 — Переглянути мої звернення");
            Console.WriteLine("4 — Переглянути статус звернення");
            Console.WriteLine("5 — Подивитись усіх зареєстрованих громадян");
            Console.WriteLine("6 — Вийти з акаунту");
            Console.WriteLine("0 — Завершити роботу");
        }

        static void Login()
        {
            Console.Write("Введіть ваш ID: ");
            string id = Console.ReadLine();

            Citizen citizen = portal.FindCitizenById(id);

            if (citizen == null)
            {
                Console.WriteLine(Messages.CitizenNotFound);
                return;
            }

            currentUser = citizen;
            Console.WriteLine($"\n{Messages.GoodMorning}, {citizen.FirstName} {citizen.LastName}!");

            var history = portal.GetAppealsByCitizenId(citizen.Id);
            if (history.Count > 0)
            {
                Console.WriteLine($"У вас {history.Count} звернень.");
                Console.WriteLine($"Останнє: \"{history[history.Count - 1].Content}\"");
                Console.WriteLine($"Статус: {history[history.Count - 1].Status}");
            }
            else
            {
                Console.WriteLine(Messages.NoAppeals);
            }
        }

        static void CreateAppeal()
        {
            Console.WriteLine("Опишіть вашу проблему:");
            Console.Write("> ");
            string content = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(content) || content.Length > Constants.MaxContentLength)
            {
                Console.WriteLine(Messages.EmptyContent);
                return;
            }

            Appeal appeal = portal.CreateAppeal(currentUser, content);
            Console.WriteLine($"\n{Messages.AppealCreated} {appeal.Id}");
            Console.WriteLine(Messages.WaitResponse);
        }

        static void CreateUrgentAppeal()
        {
            Console.WriteLine("=== Термінове звернення ===");
            Console.Write("Опишіть проблему: ");
            string content = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(content))
            {
                Console.WriteLine(Messages.EmptyContent);
                return;
            }

            Console.Write("Причина терміновості: ");
            string reason = Console.ReadLine();

            Console.Write("Крайній термін (днів від сьогодні): ");
            if (!int.TryParse(Console.ReadLine(), out int days) || days < 1)
            {
                Console.WriteLine("Некоректна кількість днів");
                return;
            }

            // Створюємо термінове звернення через портал
            int counter = portal.Appeals.Count + 1;
            string id = $"A{counter:D3}";
            var urgent = new UrgentAppeal(id, currentUser.Id,
                           $"{currentUser.LastName} {currentUser.FirstName}",
                           content, DateTime.Now.AddDays(days), reason);

            // Додаємо вручну, бо CreateAppeal повертає звичайний Appeal
            portal.AddAppeal(urgent);

            Console.WriteLine($"\n{Messages.AppealCreated} {urgent.Id}");
            Console.WriteLine($"Термін виконання: {urgent.Deadline:dd.MM.yyyy}");
            Console.WriteLine(Messages.WaitResponse);
        }

        static void ShowMyAppeals()
        {
            var myAppeals = portal.GetAppealsByCitizenId(currentUser.Id);

            if (myAppeals.Count == 0)
            {
                Console.WriteLine(Messages.NoAppeals);
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
            Console.Write("Введіть номер звернення: ");
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
                Console.WriteLine(Messages.AppealNotFound);
                return;
            }

            Console.WriteLine($"\nДетальна інформація:");
            Console.WriteLine($"  Номер: {found.Id}");
            Console.WriteLine($"  Зміст: {found.Content}");
            Console.WriteLine($"  Статус: {found.Status}");
            Console.WriteLine($"  Виконавець: {(string.IsNullOrEmpty(found.Executor) ? Constants.DefaultExecutor : found.Executor)}");
            Console.WriteLine($"  Дата: {found.CreatedDate:dd.MM.yyyy}");

            // Якщо термінове — показуємо додаткову інформацію
            if (found is UrgentAppeal urgent)
            {
                Console.WriteLine($"  Тип: ТЕРМІНОВЕ");
                Console.WriteLine($"  Крайній термін: {urgent.Deadline:dd.MM.yyyy}");
                Console.WriteLine($"  Причина: {urgent.UrgencyReason}");
                if (urgent.IsOverdue())
                    Console.WriteLine("  УВАГА: Звернення прострочено!");
            }

            // Предикатні функції
            if (found.IsResolved())
                Console.WriteLine("  Це звернення вирішено");
            if (found.IsActive())
                Console.WriteLine("  Це звернення активне");
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
            Console.WriteLine($"{Messages.Goodbye}, {currentUser.FirstName}!");
            currentUser = null;
        }
    }
}