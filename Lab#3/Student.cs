using System;
using System.IO;
using System.Text;

namespace Lab3_Var14
{
    // Клас Student - представляє студента
    class Student
    {
        // Закриті поля класу
        private string lastName;               // Прізвище
        private string firstName;               // Ім'я
        private int course;                      // Курс
        private string speciality;               // Спеціальність
        private string university;                // Університет
        private double rating;                    // Рейтинг
        private int scientificAchievements;       // Наукові здобутки

        // Конструктор без параметрів
        public Student()
        {
            lastName = "";
            firstName = "";
            course = 0;
            speciality = "";
            university = "";
            rating = 0;
            scientificAchievements = 0;
        }

        // Конструктор з параметрами
        public Student(string lastName, string firstName, int course,
                      string speciality, string university, double rating,
                      int scientificAchievements)
        {
            this.lastName = lastName;
            this.firstName = firstName;
            this.course = course;
            this.speciality = speciality;
            this.university = university;
            this.rating = rating;
            this.scientificAchievements = scientificAchievements;
        }

        // Властивості для доступу до полів
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public int Course
        {
            get { return course; }
            set { course = value; }
        }

        public string Speciality
        {
            get { return speciality; }
            set { speciality = value; }
        }

        public string University
        {
            get { return university; }
            set { university = value; }
        }

        public double Rating
        {
            get { return rating; }
            set { rating = value; }
        }

        public int ScientificAchievements
        {
            get { return scientificAchievements; }
            set { scientificAchievements = value; }
        }

        // Введення даних з консолі
        public void InputFromConsole()
        {
            Console.WriteLine("=== Введення даних студента ===");

            Console.Write("Прізвище: ");
            lastName = Console.ReadLine();

            Console.Write("Ім'я: ");
            firstName = Console.ReadLine();

            Console.Write("Курс: ");
            course = Convert.ToInt32(Console.ReadLine());

            Console.Write("Спеціальність: ");
            speciality = Console.ReadLine();

            Console.Write("Університет: ");
            university = Console.ReadLine();

            Console.Write("Рейтинг: ");
            rating = Convert.ToDouble(Console.ReadLine());

            Console.Write("Наукові здобутки: ");
            scientificAchievements = Convert.ToInt32(Console.ReadLine());
        }

        // Виведення даних на консоль
        public void OutputToConsole()
        {
            Console.WriteLine("\n=== Інформація про студента ===");
            Console.WriteLine("Прізвище: " + lastName);
            Console.WriteLine("Ім'я: " + firstName);
            Console.WriteLine("Курс: " + course);
            Console.WriteLine("Спеціальність: " + speciality);
            Console.WriteLine("Університет: " + university);
            Console.WriteLine("Рейтинг: " + rating);
            Console.WriteLine("Наукові здобутки: " + scientificAchievements);
        }

        // Запис даних у файл
        public void WriteToFile(string fileName)
        {
            try
            {
                // Створення потоку для запису
                StreamWriter writer = new StreamWriter(fileName, true, Encoding.GetEncoding(1251));

                writer.WriteLine("=== СТУДЕНТ ===");
                writer.WriteLine("Прізвище: " + lastName);
                writer.WriteLine("Ім'я: " + firstName);
                writer.WriteLine("Курс: " + course);
                writer.WriteLine("Спеціальність: " + speciality);
                writer.WriteLine("Університет: " + university);
                writer.WriteLine("Рейтинг: " + rating);
                writer.WriteLine("Наукові здобутки: " + scientificAchievements);
                writer.WriteLine("---------------------------");

                writer.Close();

                Console.WriteLine("Дані збережено у файл " + fileName);
            }
            catch (Exception error)
            {
                Console.WriteLine("Помилка при запису у файл: " + error.Message);
            }
        }

        // Розрахунок рейтингу студента
        public double CalculateRating()
        {
            // Генератор випадкових чисел
            Random random = new Random();
            double sum = 0;

            Console.WriteLine("\n=== Розрахунок рейтингу студента ===");

            Console.WriteLine("Оцінки за 10 дисциплінами:");

            // Генерація 10 випадкових оцінок
            for (int i = 0; i < 10; i++)
            {
                int grade = random.Next(40, 101);
                Console.WriteLine("Дисципліна " + (i + 1) + ": " + grade);
                sum += grade;
            }

            // Обчислення середнього балу
            double averageGrade = sum / 10;
            Console.WriteLine("Середній бал: " + averageGrade);

            // Бали за наукову діяльність
            double sciencePoints = scientificAchievements * 5;
            Console.WriteLine("Бали за наукову діяльність: " + sciencePoints);

            // Випадкові бали за знання мови
            int languageScore = random.Next(0, 21);
            Console.WriteLine("Бали за знання мови: " + languageScore);

            // Загальний рейтинг
            rating = averageGrade + sciencePoints + languageScore;
            Console.WriteLine("Загальний рейтинг: " + rating);

            return rating;
        }

        // Імітація IQ тестування
        public void SimulateIQTest(string fileName)
        {
            Random random = new Random();

            Console.WriteLine("\n=== Тестування IQ ===");
            Console.WriteLine("Тестування студента " + lastName + " " + firstName + "...");

            // Затримка для імітації тестування
            System.Threading.Thread.Sleep(1000);

            // Генерація випадкового IQ
            int iq = random.Next(70, 151);

            string result = "IQ = " + iq;
            Console.WriteLine(result);

            try
            {
                // Запис результату у файл
                StreamWriter writer = new StreamWriter(fileName, true, Encoding.GetEncoding(1251));
                writer.WriteLine(DateTime.Now + " - " + lastName + " " + firstName + ": IQ = " + iq);
                writer.Close();

                Console.WriteLine("Результат збережено у файл " + fileName);
            }
            catch (Exception error)
            {
                Console.WriteLine("Помилка при запису у файл: " + error.Message);
            }
        }
    }
}