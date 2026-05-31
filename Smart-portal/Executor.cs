using System;
using System.Collections.Generic;

namespace SmartPortal.Core
{
    // Клас виконавця — людина, яка обробляє звернення
    public class Executor
    {
        private string id;                  // унікальний ID виконавця
        private string name;                // ПІБ
        private string department;          // відділ або організація
        private List<string> assignedAppeals;  // список ID звернень, які виконує

        // Тільки для читання
        public string Id
        {
            get { return id; }
        }

        public string Name
        {
            get { return name; }
        }

        public string Department
        {
            get { return department; }
        }

        // Повертає копію списку — захист від змін ззовні
        public List<string> AssignedAppeals
        {
            get { return new List<string>(assignedAppeals); }
        }

        public Executor(string id, string name, string department)
        {
            this.id = id;
            this.name = name;
            this.department = department;
            this.assignedAppeals = new List<string>();  // композиція
        }

        // Додати звернення до списку виконавця
        public void AssignAppeal(string appealId)
        {
            if (!assignedAppeals.Contains(appealId))
                assignedAppeals.Add(appealId);
        }

        // Скільки звернень зараз у роботі
        public int GetActiveAppealCount()
        {
            return assignedAppeals.Count;
        }

        // Чи може взяти ще (максимум 5 звернень на одного)
        public bool CanTakeMoreAppeals()
        {
            return assignedAppeals.Count < 5;
        }

        public override string ToString()
        {
            return $"{Name} ({Department}) — звернень: {assignedAppeals.Count}";
        }
    }
}