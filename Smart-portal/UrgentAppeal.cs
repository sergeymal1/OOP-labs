using System;

namespace SmartPortal.Core
{
    // Успадкування: UrgentAppeal розширює Appeal
    // Термінове звернення має додаткове поле - крайній термін виконання
    public class UrgentAppeal : Appeal
    {
        private DateTime deadline;      // крайній термін
        private string urgencyReason;   // причина терміновості

        public DateTime Deadline
        {
            get { return deadline; }
            set { deadline = value; }
        }

        public string UrgencyReason
        {
            get { return urgencyReason; }
            set { urgencyReason = value; }
        }

        // Конструктор за замовчуванням
        public UrgentAppeal() : base()
        {
            this.deadline = DateTime.Now.AddDays(1);
            this.urgencyReason = "не вказано";
        }

        // Конструктор із параметрами
        public UrgentAppeal(string id, string citizenId, string authorName, string content,
                            DateTime deadline, string urgencyReason)
            : base(id, citizenId, authorName, content)
        {
            this.deadline = deadline;
            this.urgencyReason = urgencyReason;
        }

        // Конструктор копій
        public UrgentAppeal(UrgentAppeal other) : base(other)
        {
            this.deadline = other.deadline;
            this.urgencyReason = other.urgencyReason;
        }

        // Чи прострочено термін
        public bool IsOverdue()
        {
            return DateTime.Now > deadline && Status != AppealStatus.Resolved;
        }

        // Перевизначаємо ToString, додаючи термінову інформацію
        public override string ToString()
        {
            string overdue = IsOverdue() ? " [ПРОСТРОЧЕНО!]" : "";
            return base.ToString() + $" | Термін: {deadline:dd.MM.yyyy} | Причина: {urgencyReason}{overdue}";
        }
    }
}