using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SmartPortal.Core.Exceptions;

namespace SmartPortal.Core
{
    // Головний клас системи — керує всіма даними порталу
    public class SmartPortal
    {
        private List<Appeal> appeals;       // композиція: список звернень
        private List<Citizen> citizens;     // композиція: список громадян
        private List<Executor> executors;   // композиція: список виконавців
        private string cityName;            // назва міста
        private int appealCounter;          // лічильник для генерації ID звернень

        // Шляхи до файлів даних
        private string citizensFile;
        private string appealsFile;

        public string CityName
        {
            get { return cityName; }
        }

        // Повертає копії списків — захист від змін ззовні
        public List<Appeal> Appeals
        {
            get { return new List<Appeal>(appeals); }
        }

        public List<Citizen> Citizens
        {
            get { return new List<Citizen>(citizens); }
        }

        public List<Executor> Executors
        {
            get { return new List<Executor>(executors); }
        }

        public SmartPortal(string cityName, string citizensFile, string appealsFile)
        {
            this.cityName = cityName;
            this.citizensFile = citizensFile;
            this.appealsFile = appealsFile;
            this.appeals = new List<Appeal>();
            this.citizens = new List<Citizen>();
            this.executors = new List<Executor>();
            this.appealCounter = 0;

            // Завантажуємо всі дані з файлів при запуску
            LoadCitizens();
            LoadAppeals();
            LoadExecutors();
        }

        // ========== ЗАВАНТАЖЕННЯ ДАНИХ ==========

        private void LoadCitizens()
        {
            if (!File.Exists(citizensFile))
                return;

            string[] lines = File.ReadAllLines(citizensFile, Encoding.GetEncoding(1251));
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string[] parts = line.Split(';');
                if (parts.Length >= 6)
                {
                    var citizen = new Citizen(parts[0], parts[1], parts[2], parts[3], parts[4], parts[5]);
                    citizens.Add(citizen);
                }
            }
            Console.WriteLine($"Завантажено {citizens.Count} громадян із файлу");
        }

        private void LoadAppeals()
        {
            if (!File.Exists(appealsFile))
                return;

            string[] lines = File.ReadAllLines(appealsFile, Encoding.GetEncoding(1251));
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string[] parts = line.Split(';');
                if (parts.Length >= 7)
                {
                    string id = parts[0];
                    string citizenId = parts[1];
                    string authorName = parts[2];
                    string content = parts[3];
                    DateTime date = DateTime.Parse(parts[4]);
                    AppealStatus status = (AppealStatus)Enum.Parse(typeof(AppealStatus), parts[5]);
                    string executor = parts[6];

                    var appeal = new Appeal(id, citizenId, authorName, content, date, status, executor);
                    appeals.Add(appeal);

                    // Оновлюємо лічильник
                    if (id.StartsWith("A") && int.TryParse(id.Substring(1), out int num))
                    {
                        if (num > appealCounter)
                            appealCounter = num;
                    }
                }
            }
            Console.WriteLine($"Завантажено {appeals.Count} звернень із файлу");
        }

        // Завантаження виконавців із файлу executors.txt
        private void LoadExecutors()
        {
            string fileName = "executors.txt";
            if (!File.Exists(fileName))
                return;

            string[] lines = File.ReadAllLines(fileName, Encoding.GetEncoding(1251));
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string[] parts = line.Split(';');
                if (parts.Length >= 3)
                {
                    var executor = new Executor(parts[0], parts[1], parts[2]);
                    executors.Add(executor);
                }
            }
            Console.WriteLine($"Завантажено {executors.Count} виконавців із файлу");
        }

        // ========== ЗБЕРЕЖЕННЯ ==========

        private void SaveAppeals()
        {
            List<string> lines = new List<string>();
            foreach (var a in appeals)
            {
                lines.Add(a.ToFileString());
            }
            File.WriteAllLines(appealsFile, lines, Encoding.GetEncoding(1251));
        }

        // ========== ПОШУК ==========

        public Citizen FindCitizenById(string id)
        {
            foreach (var c in citizens)
            {
                if (c.Id == id)
                    return c;
            }
            throw new CitizenNotFoundException(id);
        }

        public List<Appeal> GetAppealsByCitizenId(string citizenId)
        {
            List<Appeal> result = new List<Appeal>();
            foreach (var a in appeals)
            {
                if (a.CitizenId == citizenId)
                    result.Add(a);
            }
            return result;
        }

        // ========== ВИКОНАВЦІ ==========

        // Автоматичний пошук вільного виконавця
        public Executor AutoAssignExecutor()
        {
            foreach (var e in executors)
            {
                if (e.CanTakeMoreAppeals())
                    return e;
            }
            return null;  // усі зайняті
        }

        // ========== ЗВЕРНЕННЯ ==========

        public Appeal CreateAppeal(Citizen author, string content)
        {
            if (author == null)
                throw new ArgumentNullException(nameof(author));

            if (string.IsNullOrWhiteSpace(content))
                throw new EmptyContentException();

            // Перевірка ліміту звернень
            var existingAppeals = GetAppealsByCitizenId(author.Id);
            if (existingAppeals.Count >= Constants.MaxAppealsPerCitizen)
                throw new MaxAppealsExceededException(author.Id, Constants.MaxAppealsPerCitizen);

            appealCounter++;
            string id = $"A{appealCounter:D3}";

            var appeal = new Appeal(id, author.Id, $"{author.LastName} {author.FirstName}", content);
            appeals.Add(appeal);

            // Автоматичне призначення виконавця
            Executor executor = AutoAssignExecutor();
            if (executor != null)
            {
                appeal.Status = AppealStatus.InProgress;
                appeal.Executor = executor.Name;
                executor.AssignAppeal(appeal.Id);
                Console.WriteLine($"{Messages.ExecutorAssigned}: {executor.Name}");
            }

            SaveAppeals();
            Console.WriteLine($"Створено: {appeal}");
            return appeal;
        }

        public void UpdateAppealStatus(string appealId, AppealStatus newStatus, string executor)
        {
            foreach (var a in appeals)
            {
                if (a.Id == appealId)
                {
                    a.Status = newStatus;
                    a.Executor = executor;
                    SaveAppeals();
                    Console.WriteLine($"Статус звернення #{appealId} змінено на {newStatus}");
                    return;
                }
            }
            throw new AppealNotFoundException(appealId);
        }

        // Додавання готового звернення (для UrgentAppeal)
        public void AddAppeal(Appeal appeal)
        {
            if (appeal == null)
                throw new ArgumentNullException("Звернення не може бути null");

            appeals.Add(appeal);

            // Також пробуємо призначити виконавця
            Executor executor = AutoAssignExecutor();
            if (executor != null)
            {
                appeal.Status = AppealStatus.InProgress;
                appeal.Executor = executor.Name;
                executor.AssignAppeal(appeal.Id);
                Console.WriteLine($"{Messages.ExecutorAssigned}: {executor.Name}");
            }

            SaveAppeals();
            Console.WriteLine($"Створено: {appeal}");
        }

        // ========== РЕЄСТРАЦІЯ ==========

        public void RegisterCitizen(Citizen citizen)
        {
            if (citizen == null)
                throw new ArgumentNullException(nameof(citizen));

            foreach (var c in citizens)
            {
                if (c.Id == citizen.Id)
                    throw new DuplicateCitizenException(citizen.Id);
            }

            citizens.Add(citizen);
            File.AppendAllText(citizensFile, citizen.ToFileString() + Environment.NewLine,
                               Encoding.GetEncoding(1251));
            Console.WriteLine($"Зареєстровано: {citizen}");
        }
    }
}