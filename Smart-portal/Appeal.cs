using System;

namespace SmartPortal.Core
{
    // Перелічення статусів, набір значень обмежено
    public enum AppealStatus
    {
        New,
        InProgress,
        Resolved,
        Rejected
    }

    public class Appeal
    {
        private string id;
        private Citizen author;      // Посилання на автора
        private string content;
        private DateTime createdDate;
        private AppealStatus status;
        private string executor;

        // Властивості тільки для читання
        public string Id
        {
            get { return id; }
        }

        public Citizen Author
        {
            get { return author; }
        }

        public DateTime CreatedDate
        {
            get { return createdDate; }
        }

        // Змінні властивості
        public string Content
        {
            get { return content; }
            set { content = value; }
        }

        public AppealStatus Status
        {
            get { return status; }
            set { status = value; }
        }

        public string Executor
        {
            get { return executor; }
            set { executor = value; }
        }

        // Статус завжди New, дата поточна
        public Appeal(string id, Citizen author, string content)
        {
            this.id = id;
            this.author = author;
            this.content = content;
            this.createdDate = DateTime.Now;
            this.status = AppealStatus.New;
        }

        public override string ToString()
        {
            return $"Звернення #{Id} | Автор: {Author} | Статус: {Status} | Дата: {CreatedDate:dd.MM.yyyy}";
        }
    }
}