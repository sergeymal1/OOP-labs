using System;

namespace Lab6_SmartFridge
{
    // ==== Клас Продукт (агрегація) ====
    class Product
    {
        private string name;
        private string type;
        private DateTime expiryDate;
        private int quantity;
        private bool isSpoiled;

        public Product()
        {
            name = "Невідомий";
            type = "Інше";
            expiryDate = DateTime.Now.AddDays(7);
            quantity = 1;
            isSpoiled = false;
        }

        public Product(string name, string type, DateTime expiryDate, int quantity)
        {
            this.name = name;
            this.type = type;
            this.expiryDate = expiryDate;
            this.quantity = quantity;
            this.isSpoiled = false;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public DateTime ExpiryDate
        {
            get { return expiryDate; }
            set { expiryDate = value; }
        }

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public bool IsSpoiled
        {
            get { return isSpoiled; }
        }

        public bool IsExpired()
        {
            return DateTime.Now > expiryDate;
        }

        public void MarkAsSpoiled()
        {
            isSpoiled = true;
            Console.WriteLine("   [!] Продукт \"" + name + "\" позначено як зіпсований!");
        }

        public void ShowInfo()
        {
            Console.WriteLine("   Назва: " + name);
            Console.WriteLine("   Тип: " + type);
            Console.WriteLine("   Термін придатності: " + expiryDate.ToShortDateString());
            Console.WriteLine("   Кількість: " + quantity);
            Console.WriteLine("   Стан: " + (isSpoiled ? "Зіпсований" : (IsExpired() ? "Прострочений" : "Нормальний")));
        }
    }
}