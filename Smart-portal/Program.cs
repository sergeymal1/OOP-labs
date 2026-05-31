using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SmartPortal.Core;
using SmartPortal.Core.Exceptions;

namespace SmartPortalApp
{
    class Program
    {
        static SmartPortal.Core.SmartPortal portal;
        static Citizen currentUser = null;

        static void Main(string[] args)
        {
            try
            {
                Console.OutputEncoding = Encoding.GetEncoding(1251);
                Console.InputEncoding = Encoding.GetEncoding(1251);

                if (!File.Exists(Constants.CitizensFileName))
                {
                    Console.WriteLine($"Файл {Constants.CitizensFileName} не знайдено. Створюю порожній...");
                    File.Create(Constants.CitizensFileName).Close();
                }

                portal = new SmartPortal.Core.SmartPortal("Київ",
                             Constants.CitizensFileName, Constants.AppealsFileName);
                Console.WriteLine($"=== Smart-портал міста {portal.CityName} ===\n");

                while (true)
                {
                    if (currentUser == null)
                        ShowGuestMenu();
                    else
                        ShowUserMenu();

                    Console.Write("Ваш вибір: ");
                    string choice = SafeReadLine();
                    Console.WriteLine();

                    if (currentUser == null)
                    {
                        switch (choice)
                        {
                            case "1": Login(); break;
                            case "2": ShowAllCitizens(); break;
                            case "3": RegisterNewCitizen(); break;
                            case "4": ShowDeputies(); break;
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
                            case "6": ShowDeputies(); break;
                            case "7": Logout(); break;
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
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Помилка файлу: {ex.Message}");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Критична помилка: {ex.Message}");
                Console.ReadLine();
            }
            finally
            {
                Console.WriteLine("\nПрограма завершує роботу...");
            }
        }

        // ========== БЕЗПЕЧНЕ ВВЕДЕННЯ ==========

        static string SafeReadLine()
        {
            try
            {
                string input = Console.ReadLine();
                return input ?? "";
            }
            catch
            {
                return "";
            }
        }

        static int SafeReadInt(int defaultValue = 1)
        {
            try
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out int result))
                    return result;
                return defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }

        // ========== МЕНЮ ==========

        static void ShowGuestMenu()
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine($"{Messages.WelcomeMessage} {portal.CityName}!");
            Console.WriteLine("========================================");
            Console.WriteLine("1 — Увійти в систему (за ID)");
            Console.WriteLine("2 — Подивитись усіх зареєстрованих громадян");
            Console.WriteLine("3 — Зареєструвати нового громадянина");
            Console.WriteLine("4 — Подивитись депутатів");
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
            Console.WriteLine("6 — Подивитись депутатів");
            Console.WriteLine("7 — Вийти з акаунту");
            Console.WriteLine("0 — Завершити роботу");
        }

        // ========== АВТОРИЗАЦІЯ ==========

        static void Login()
        {
            while (true)
            {
                try
                {
                    Console.Write("Введіть ваш ID (або 0 для виходу в меню): ");
                    string id = SafeReadLine();

                    if (id == "0") return;

                    Citizen citizen = portal.FindCitizenById(id);
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
                    return;  // успішний вхід — виходимо з циклу
                }
                catch (CitizenNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Спробуйте ще раз або введіть 0 для виходу.\n");
                }
            }
        }

        static void Logout()
        {
            Console.WriteLine($"{Messages.Goodbye}, {currentUser.FirstName}!");
            currentUser = null;
        }

        // ========== ЗВЕРНЕННЯ ==========

        static void CreateAppeal()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Опишіть вашу проблему (або 0 для виходу в меню):");
                    Console.Write("> ");
                    string content = SafeReadLine();

                    if (content == "0") return;

                    Appeal appeal = portal.CreateAppeal(currentUser, content);
                    Console.WriteLine($"\n{Messages.AppealCreated} {appeal.Id}");
                    Console.WriteLine(Messages.WaitResponse);
                    return;
                }
                catch (EmptyContentException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Спробуйте ще раз або введіть 0 для виходу.\n");
                }
                catch (MaxAppealsExceededException ex)
                {
                    Console.WriteLine(ex.Message);
                    return;  // ліміт вичерпано — повертаємо в меню
                }
            }
        }

        static void CreateUrgentAppeal()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("=== Термінове звернення ===");
                    Console.Write("Опишіть проблему (або 0 для виходу): ");
                    string content = SafeReadLine();

                    if (content == "0") return;

                    if (string.IsNullOrWhiteSpace(content))
                    {
                        Console.WriteLine(Messages.EmptyContent);
                        continue;
                    }

                    Console.Write("Причина терміновості: ");
                    string reason = SafeReadLine();

                    // Окремий цикл для введення терміну — не стирає попередні дані
                    int days;
                    while (true)
                    {
                        Console.Write("Крайній термін (днів від сьогодні): ");
                        days = SafeReadInt(-1);
                        if (days >= 1) break;
                        Console.WriteLine("Некоректна кількість днів. Введіть число більше 0.");
                    }

                    int counter = portal.Appeals.Count + 1;
                    string id = $"A{counter:D3}";
                    var urgent = new UrgentAppeal(id, currentUser.Id,
                                   $"{currentUser.LastName} {currentUser.FirstName}",
                                   content, DateTime.Now.AddDays(days), reason);

                    portal.AddAppeal(urgent);
                    Console.WriteLine($"\n{Messages.AppealCreated} {urgent.Id}");
                    Console.WriteLine($"Термін виконання: {urgent.Deadline:dd.MM.yyyy}");
                    Console.WriteLine(Messages.WaitResponse);
                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Помилка: {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.\n");
                }
            }
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
            Console.Write("Введіть номер звернення (або 0 для виходу): ");
            string id = SafeReadLine();

            if (id == "0") return;

            try
            {
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
                Console.WriteLine($"  Виконавець: {(string.IsNullOrEmpty(found.Executor) ? Messages.NoExecutor : found.Executor)}");
                Console.WriteLine($"  Дата: {found.CreatedDate:dd.MM.yyyy}");

                if (found is UrgentAppeal urgent)
                {
                    Console.WriteLine($"  Тип: ТЕРМІНОВЕ");
                    Console.WriteLine($"  Крайній термін: {urgent.Deadline:dd.MM.yyyy}");
                    Console.WriteLine($"  Причина: {urgent.UrgencyReason}");
                    if (urgent.IsOverdue())
                        Console.WriteLine($"  {Messages.OverdueWarning}");
                }

                if (found.IsResolved())
                    Console.WriteLine($"  {Messages.ResolvedInfo}");
                if (found.IsActive())
                    Console.WriteLine($"  {Messages.ActiveInfo}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
        }

        // ========== РЕЄСТРАЦІЯ ==========

        static void RegisterNewCitizen()
        {
            try
            {
                Console.WriteLine("=== Реєстрація нового громадянина ===");
                Console.Write("ID (наприклад C006): ");
                string id = SafeReadLine();

                try
                {
                    portal.FindCitizenById(id);
                    Console.WriteLine("Громадянин із таким ID вже існує");
                    return;
                }
                catch (CitizenNotFoundException) { }

                Console.Write("Ім'я: ");
                string firstName = SafeReadLine();
                Console.Write("Прізвище: ");
                string lastName = SafeReadLine();
                Console.Write("Адреса: ");
                string address = SafeReadLine();
                Console.Write("Телефон: ");
                string phone = SafeReadLine();
                Console.Write("Email: ");
                string email = SafeReadLine();

                var citizen = new Citizen(id, firstName, lastName, address, phone, email);
                portal.RegisterCitizen(citizen);
                Console.WriteLine($"Громадянина {citizen} успішно зареєстровано!");
            }
            catch (DuplicateCitizenException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Помилка даних: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка реєстрації: {ex.Message}");
            }
        }

        // ========== ПЕРЕГЛЯД ==========

        static void ShowAllCitizens()
        {
            var citizens = portal.Citizens;
            Console.WriteLine($"=== Зареєстровані громадяни ({citizens.Count}) ===");
            foreach (var c in citizens)
            {
                Console.WriteLine($"  {c} | Адреса: {c.Address} | Тел: {c.Phone}");
            }
        }

        static void ShowDeputies()
        {
            var deputies = portal.Deputies;
            Console.WriteLine($"=== Депутати ({deputies.Count}) ===");
            foreach (var d in deputies)
            {
                Console.WriteLine($"  {d}");
            }
        }
    }
}