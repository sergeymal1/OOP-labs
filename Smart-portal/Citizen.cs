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

        // Id тільки для читання
        public string Id
        {
            get { return id; }
        }

        // Не дозволяє порожнє ім'я
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

        // Поля через конструктор
        public Citizen(string id, string firstName, string lastName)
        {
            this.id = id;
            FirstName = firstName;
            LastName = lastName;
        }

        // Вивід в консоль
        public override string ToString()
        {
            return $"{LastName} {FirstName} (ID: {Id})";
        }
    }
}