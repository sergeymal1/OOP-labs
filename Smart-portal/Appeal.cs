using System;

namespace SmartPortal.Core
{
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
        private string citizenId;      // зберігаємо ID, а не посилання — простіше для файлів
        private string authorName;     // ім'я для швидкого виводу
        private string content;
        private DateTime createdDate;
        private AppealStatus status;
        private string executor;

        public string Id
        {
            get { return id; }
        }

        public string CitizenId
        {
            get { return citizenId; }
        }

        public string AuthorName
        {
            get { return authorName; }
        }

        public string Content
        {
            get { return content; }
        }

        public DateTime CreatedDate
        {
            get { return createdDate; }
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

        // Конструктор для нового звернення
        public Appeal(string id, string citizenId, string authorName, string content)
        {
            this.id = id;
            this.citizenId = citizenId;
            this.authorName = authorName;
            this.content = content;
            this.createdDate = DateTime.Now;
            this.status = AppealStatus.New;
        }

        // Конструктор для завантаження з файлу (з датою)
        public Appeal(string id, string citizenId, string authorName, string content,
                      DateTime createdDate, AppealStatus status, string executor)
        {
            this.id = id;
            this.citizenId = citizenId;
            this.authorName = authorName;
            this.content = content;
            this.createdDate = createdDate;
            this.status = status;
            this.executor = executor;
        }

        // Конструктор за замовчуванням
        public Appeal()
        {
            this.id = Guid.NewGuid().ToString().Substring(0, 8);
            this.content = "";
            this.createdDate = DateTime.Now;
            this.status = AppealStatus.New;
        }

        // Конструктор копій
        public Appeal(Appeal other)
        {
            this.id = other.id;
            this.citizenId = other.citizenId;
            this.authorName = other.authorName;
            this.content = other.content;
            this.createdDate = other.createdDate;
            this.status = other.status;
            this.executor = other.executor;
        }

        // Перетворює звернення в рядок для запису у файл
        public string ToFileString()
        {
            return $"{Id};{CitizenId};{AuthorName};{Content};{CreatedDate:yyyy-MM-dd HH:mm};{Status};{Executor}";
        }

        public override string ToString()
        {
            string exec = string.IsNullOrEmpty(Executor) ? "не призначено" : Executor;
            return $"Звернення #{Id} | Автор: {AuthorName} | Статус: {Status} | Виконавець: {exec} | Дата: {CreatedDate:dd.MM.yyyy}";
        }

        // Чи звернення вирішено (предикатні функції)
        public bool IsResolved()
        {
            return Status == AppealStatus.Resolved;
        }

        // Чи звернення активне (ще не закрите)
        public bool IsActive()
        {
            return Status == AppealStatus.New || Status == AppealStatus.InProgress;
        }

        // Перевантаження оператора == (порівняння за ID)
        public static bool operator ==(Appeal a, Appeal b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (a is null || b is null) return false;
            return a.Id == b.Id;
        }

        // Перевантаження оператора != (має бути парою з ==)
        public static bool operator !=(Appeal a, Appeal b)
        {
            return !(a == b);
        }

        // Перевантаження Equals — обов'язково разом із ==
        public override bool Equals(object obj)
        {
            if (obj is Appeal other)
                return this == other;
            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}