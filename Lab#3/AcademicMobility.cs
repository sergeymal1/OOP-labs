using System;
using System.IO;
using System.Text;

namespace Lab3_Var14
{
    // Частковий клас AcademicMobility
    partial class AcademicMobility
    {
        // Закриті поля
        private string[] internationalPrograms;  // Масив міжнародних програм
        private string[] partnerUniversities;    // Масив університетів-партнерів
        private string contacts;                  // Контактна інформація
        private int programsCount;                 // Кількість програм
        private int universitiesCount;             // Кількість університетів

        // Конструктор без параметрів
        public AcademicMobility()
        {
            internationalPrograms = new string[100];
            partnerUniversities = new string[100];
            contacts = "";
            programsCount = 0;
            universitiesCount = 0;
        }

        // Конструктор з параметрами
        public AcademicMobility(string[] programs, string[] universities, string contacts)
        {
            this.internationalPrograms = programs;
            this.partnerUniversities = universities;
            this.contacts = contacts;
            this.programsCount = programs.Length;
            this.universitiesCount = universities.Length;
        }

        // Властивості
        public string[] InternationalPrograms
        {
            get { return internationalPrograms; }
            set { internationalPrograms = value; }
        }

        public string[] PartnerUniversities
        {
            get { return partnerUniversities; }
            set { partnerUniversities = value; }
        }

        public string Contacts
        {
            get { return contacts; }
            set { contacts = value; }
        }

        public int ProgramsCount
        {
            get { return programsCount; }
        }

        public int UniversitiesCount
        {
            get { return universitiesCount; }
        }

        // Введення даних з консолі
        public void InputFromConsole()
        {
            Console.WriteLine("\n=== Введення даних ===");

            Console.Write("Контакти: ");
            contacts = Console.ReadLine();

            Console.Write("Кількість програм: ");
            programsCount = Convert.ToInt32(Console.ReadLine());

            internationalPrograms = new string[programsCount];
            for (int i = 0; i < programsCount; i++)
            {
                Console.Write("Програма " + (i + 1) + ": ");
                internationalPrograms[i] = Console.ReadLine();
            }

            Console.Write("Кількість університетів: ");
            universitiesCount = Convert.ToInt32(Console.ReadLine());

            partnerUniversities = new string[universitiesCount];
            for (int i = 0; i < universitiesCount; i++)
            {
                Console.Write("Університет " + (i + 1) + ": ");
                partnerUniversities[i] = Console.ReadLine();
            }
        }

        // Виведення даних на консоль
        public void OutputToConsole()
        {
            Console.WriteLine("\n=== Відділ академічної мобільності ===");
            Console.WriteLine("Контакти: " + contacts);

            Console.WriteLine("\nМіжнародні програми:");
            for (int i = 0; i < programsCount; i++)
            {
                Console.WriteLine("  " + (i + 1) + ". " + internationalPrograms[i]);
            }

            Console.WriteLine("\nУніверситети-партнери:");
            for (int i = 0; i < universitiesCount; i++)
            {
                Console.WriteLine("  " + (i + 1) + ". " + partnerUniversities[i]);
            }
        }

        // Запис даних у файл
        public void WriteToFile(string fileName)
        {
            try
            {
                StreamWriter writer = new StreamWriter(fileName, true, Encoding.GetEncoding(1251));

                writer.WriteLine("=== ВІДДІЛ АКАДЕМІЧНОЇ МОБІЛЬНОСТІ ===");
                writer.WriteLine("Контакти: " + contacts);
                writer.WriteLine("\nМіжнародні програми:");

                for (int i = 0; i < programsCount; i++)
                {
                    writer.WriteLine("  " + (i + 1) + ". " + internationalPrograms[i]);
                }

                writer.WriteLine("\nУніверситети-партнери:");
                for (int i = 0; i < universitiesCount; i++)
                {
                    writer.WriteLine("  " + (i + 1) + ". " + partnerUniversities[i]);
                }

                writer.WriteLine("==========================================\n");
                writer.Close();

                Console.WriteLine("Дані збережено у файл " + fileName);
            }
            catch (Exception error)
            {
                Console.WriteLine("Помилка: " + error.Message);
            }
        }

        // Реєстрація студента на програму
        public void RegisterStudent(Student student, string programName, string fileName)
        {
            Console.WriteLine("\n=== Реєстрація студента ===");

            string registrationInfo = "Студент " + student.LastName + " " + student.FirstName +
                                     " зареєстрований на програму '" + programName + "'";

            Console.WriteLine(registrationInfo);
            Console.WriteLine("Рейтинг: " + student.Rating);

            try
            {
                StreamWriter writer = new StreamWriter(fileName, true, Encoding.GetEncoding(1251));
                writer.WriteLine(DateTime.Now + " - " + registrationInfo);
                writer.WriteLine("Рейтинг: " + student.Rating);
                writer.WriteLine("---------------------------");
                writer.Close();

                Console.WriteLine("Інформацію збережено у файл " + fileName);
            }
            catch (Exception error)
            {
                Console.WriteLine("Помилка: " + error.Message);
            }
        }

        // Бінарний пошук університету
        public int FindUniversity(string universityName)
        {
            Console.WriteLine("\n=== Пошук університету '" + universityName + "' ===");

            // Створення копії масиву для сортування
            string[] sortedUniversities = new string[universitiesCount];
            Array.Copy(partnerUniversities, sortedUniversities, universitiesCount);

            // Сортування масиву
            Array.Sort(sortedUniversities);

            int left = 0;
            int right = universitiesCount - 1;

            // Бінарний пошук
            while (left <= right)
            {
                int middle = (left + right) / 2;
                int comparison = string.Compare(sortedUniversities[middle], universityName, true);

                if (comparison == 0)
                {
                    Console.WriteLine("Університет знайдено!");

                    // Пошук оригінального індексу
                    for (int i = 0; i < universitiesCount; i++)
                    {
                        if (string.Compare(partnerUniversities[i], universityName, true) == 0)
                        {
                            return i;
                        }
                    }
                }

                if (comparison < 0)
                    left = middle + 1;
                else
                    right = middle - 1;
            }

            Console.WriteLine("Університет не знайдено.");
            return -1;
        }
    }
}