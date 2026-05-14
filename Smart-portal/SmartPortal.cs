using System;
using System.Collections.Generic;

namespace SmartPortal.Core
{
    public class SmartPortal
    {
        // Списки належать порталу, створюються в конструкторі
        private List<Appeal> appeals;
        private List<Citizen> citizens;
        private string cityName;

        public string CityName
        {
            get { return cityName; }
        }

        // Повертаємо копію, захист від зміни ззовні
        public List<Appeal> Appeals
        {
            get { return new List<Appeal>(appeals); }
        }

        public List<Citizen> Citizens
        {
            get { return new List<Citizen>(citizens); }
        }

        public SmartPortal(string cityName)
        {
            this.cityName = cityName;
            this.appeals = new List<Appeal>();
            this.citizens = new List<Citizen>();
        }

        // Citizen створюється ззовні, існує незалежно від порталу
        public void RegisterCitizen(Citizen citizen)
        {
            if (citizen == null)
                throw new ArgumentNullException("Громадянин не може бути null");

            citizens.Add(citizen);
            Console.WriteLine($"Зареєстровано: {citizen}");
        }

        // Appeal створюється всередині методу
        public Appeal CreateAppeal(string id, Citizen author, string content)
        {
            if (!citizens.Contains(author))
                throw new InvalidOperationException("Громадянин не зареєстрований у системі");

            var appeal = new Appeal(id, author, content);
            appeals.Add(appeal);
            Console.WriteLine($"Створено: {appeal}");
            return appeal;
        }

        // Пошук через посилання на об'єкт Citizen
        public List<Appeal> GetAppealsByCitizen(Citizen citizen)
        {
            List<Appeal> result = new List<Appeal>();
            foreach (var a in appeals)
            {
                if (a.Author == citizen)
                    result.Add(a);
            }
            return result;
        }

        public void UpdateAppealStatus(string appealId, AppealStatus newStatus, string executor)
        {
            foreach (var a in appeals)
            {
                if (a.Id == appealId)
                {
                    a.Status = newStatus;
                    a.Executor = executor;
                    Console.WriteLine($"Статус звернення #{appealId} змінено на {newStatus}");
                    return;
                }
            }
            throw new Exception($"Звернення з ID {appealId} не знайдено");
        }
    }
}