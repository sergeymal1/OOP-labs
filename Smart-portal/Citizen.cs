using System;

namespace SmartPortal.Core
{
    public class Citizen
    {
        private string firstName;
        private string lastName;
        private string address;
        private string phone;
        private string email;
        private string id;

        public string Id
        {
            get { return id; }
        }

        public string FirstName
        {
            get { return firstName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Ім'я не може бути порожнім");
                firstName = value;
            }
        }

        public string LastName
        {
            get { return lastName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Прізвище не може бути порожнім");
                lastName = value;
            }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public Citizen(string id, string firstName, string lastName)
        {
            this.id = id;
            FirstName = firstName;
            LastName = lastName;
        }

        // Новий конструктор — створює громадянина одразу з усіма даними з файлу
        public Citizen(string id, string firstName, string lastName,
                       string address, string phone, string email)
        {
            this.id = id;
            FirstName = firstName;
            LastName = lastName;
            this.address = address;
            this.phone = phone;
            this.email = email;
        }

        // Конструктор за замовчуванням
        public Citizen()
        {
            this.id = Guid.NewGuid().ToString().Substring(0, 8);
            this.firstName = "Невідомо";
            this.lastName = "Невідомо";
        }

        // Перетворює громадянина в рядок для запису у файл
        public string ToFileString()
        {
            return $"{Id};{FirstName};{LastName};{Address};{Phone};{Email}";
        }

        public override string ToString()
        {
            return $"{LastName} {FirstName} (ID: {Id})";
        }

        // Предикатна функція — чи є контактний телефон
        public bool HasPhone()
        {
            return !string.IsNullOrWhiteSpace(Phone);
        }

        // Предикатна функція — чи є email
        public bool HasEmail()
        {
            return !string.IsNullOrWhiteSpace(Email);
        }

        // Перевантаження оператора == (порівняння за ID)
        public static bool operator ==(Citizen a, Citizen b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (a is null || b is null) return false;
            return a.Id == b.Id;
        }
        public static bool operator !=(Citizen a, Citizen b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (obj is Citizen other)
                return this == other;
            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}