using System;
using System.Collections.Generic;
using System.IO;  // для роботи з файлами

namespace SmartPortal.Core
{
    public class SmartPortal
    {
        private List<Appeal> appeals;
        private List<Citizen> citizens;
        private string cityName;
        private int appealCounter;

        // Шляхи до файлів
        private string citizensFile;
        private string appealsFile;

        public string CityName
        {
            get { return cityName; }
        }

        public List<Appeal> Appeals
        {
            get { return new List<Appeal>(appeals); }
        }

        public List<Citizen> Citizens
        {
            get { return new List<Citizen>(citizens); }
        }

        public SmartPortal(string cityName, string citizensFile, string appealsFile)
        {
            this.cityName = cityName;
            this.citizensFile = citizensFile;
            this.appealsFile = appealsFile;
            this.appeals = new List<Appeal>();
            this.citizens = new List<Citizen>();
            this.appealCounter = 0;

            // Завантажуємо дані з файлів при запуску
            LoadCitizens();
            LoadAppeals();
        }

        // Завантаження громадян із файлу
        private void LoadCitizens()
        {
            if (!File.Exists(citizensFile))
                return;  // файлу немає — працюємо з порожнім списком

            string[] lines = File.ReadAllLines(citizensFile, System.Text.Encoding.GetEncoding(1251));
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                // Розбиваємо рядок за символом ;
                string[] parts = line.Split(';');
                if (parts.Length >= 6)
                {
                    var citizen = new Citizen(parts[0], parts[1], parts[2], parts[3], parts[4], parts[5]);
                    citizens.Add(citizen);
                }
            }
            Console.WriteLine($"Завантажено {citizens.Count} громадян із файлу");
        }

        // Завантаження звернень із файлу
        private void LoadAppeals()
        {
            if (!File.Exists(appealsFile))
                return;

            string[] lines = File.ReadAllLines(appealsFile, System.Text.Encoding.GetEncoding(1251));
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

                    // Оновлюємо лічильник — витягуємо номер із ID
                    if (id.StartsWith("A") && int.TryParse(id.Substring(1), out int num))
                    {
                        if (num > appealCounter)
                            appealCounter = num;
                    }
                }
            }
            Console.WriteLine($"Завантажено {appeals.Count} звернень із файлу");
        }

        // Збереження всіх звернень у файл
        private void SaveAppeals()
        {
            List<string> lines = new List<string>();
            foreach (var a in appeals)
            {
                lines.Add(a.ToFileString());
            }
            File.WriteAllLines(appealsFile, lines, System.Text.Encoding.GetEncoding(1251));
        }

        // Пошук громадянина за ID
        public Citizen FindCitizenById(string id)
        {
            foreach (var c in citizens)
            {
                if (c.Id == id)
                    return c;
            }
            return null;
        }

        // Отримати історію звернень громадянина
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

        // Створення нового звернення (ID генерується автоматично)
        public Appeal CreateAppeal(Citizen author, string content)
        {
            appealCounter++;
            string id = $"A{appealCounter:D3}";  // A001, A002...

            var appeal = new Appeal(id, author.Id, $"{author.LastName} {author.FirstName}", content);
            appeals.Add(appeal);

            // Одразу зберігаємо у файл
            SaveAppeals();

            Console.WriteLine($"Створено: {appeal}");
            return appeal;
        }

        // Зміна статусу звернення
        public void UpdateAppealStatus(string appealId, AppealStatus newStatus, string executor)
        {
            foreach (var a in appeals)
            {
                if (a.Id == appealId)
                {
                    a.Status = newStatus;
                    a.Executor = executor;
                    SaveAppeals();  // зберігаємо зміни у файл
                    Console.WriteLine($"Статус звернення #{appealId} змінено на {newStatus}");
                    return;
                }
            }
            throw new Exception($"Звернення з ID {appealId} не знайдено");
        }

        // Реєстрація нового громадянина
        public void RegisterCitizen(Citizen citizen)
        {
            if (citizen == null)
                throw new ArgumentNullException("Громадянин не може бути null");

            citizens.Add(citizen);

            // Дописуємо в кінець файлу
            File.AppendAllText(citizensFile, citizen.ToFileString() + Environment.NewLine,
                               System.Text.Encoding.GetEncoding(1251));

            Console.WriteLine($"Зареєстровано: {citizen}");
        }

        // Додавання готового звернення (для UrgentAppeal)
        public void AddAppeal(Appeal appeal)
        {
            if (appeal == null)
                throw new ArgumentNullException("Звернення не може бути null");

            appeals.Add(appeal);
            SaveAppeals();
            Console.WriteLine($"Створено: {appeal}");
        }
    }
}