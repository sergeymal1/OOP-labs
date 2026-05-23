using System;

namespace SmartPortal.Core
{
    public abstract class BaseEntity
    {
        private string id;
        private DateTime createdDate;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public DateTime CreatedDate
        {
            get { return createdDate; }
            set { createdDate = value; }
        }

        // Конструктор за замовчуванням
        public BaseEntity()
        {
            this.id = Guid.NewGuid().ToString().Substring(0, 8);
            this.createdDate = DateTime.Now;
        }

        // Конструктор із параметрами
        public BaseEntity(string id)
        {
            this.id = id;
            this.createdDate = DateTime.Now;
        }

        // Кожен спадкоємець зобов'язаний реалізувати
        public abstract string GetDisplayInfo();

        public virtual bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(id);
        }
    }
}