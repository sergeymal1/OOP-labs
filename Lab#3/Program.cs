using System;
using System.IO;
using System.Text;

namespace Lab3_Var14
{
    // Головний клас програми
    class Program
    {
        // Точка входу в програму
        static void Main(string[] args)
        {
            // Встановлення кодування для української мови
            Console.OutputEncoding = Encoding.GetEncoding(1251);
            Console.InputEncoding = Encoding.GetEncoding(1251);

            Console.WriteLine("==================================================");
            Console.WriteLine("ЛАБОРАТОРНА РОБОТА №3");
            Console.WriteLine("Варіант 14: Академічна мобільність студентів");
            Console.WriteLine("==================================================\n");

            // Видалення старих файлів перед початком роботи
            DeleteOldFiles();

            // Демонстрація всіх класів
            DemonstrateAllClasses();

            Console.WriteLine("\nНатисніть Enter для завершення програми...");
            Console.ReadLine();
        }

        // Видалення старих файлів
        static void DeleteOldFiles()
        {
            // Масив назв файлів, які створює програма
            string[] files = {
                "students.txt",
                "academic_mobility.txt",
                "registrations.txt",
                "iq_results.txt",
                "competition_results.txt",
                "research_results.txt"
            };

            // Перебір всіх файлів
            foreach (string file in files)
            {
                // Перевірка на існування файлу
                if (File.Exists(file))
                {
                    File.Delete(file); // Видалення файлу
                }
            }
        }

        // Демонстрація всіх класів покроково
        static void DemonstrateAllClasses()
        {
            // Демонстрація класу Student
            Console.WriteLine("\n--- Демонстрація класу Student ---");
            DemonstrateStudentClass();
            Console.WriteLine("\nНатисніть Enter...");
            Console.ReadLine();
            Console.Clear();

            // Демонстрація класу AcademicMobility
            Console.WriteLine("\n--- Демонстрація класу AcademicMobility ---");
            DemonstrateAcademicMobilityClass();
            Console.WriteLine("\nНатисніть Enter...");
            Console.ReadLine();
            Console.Clear();

            // Демонстрація вкладеного класу InterProgram
            Console.WriteLine("\n--- Демонстрація вкладеного класу InterProgram ---");
            DemonstrateInterProgramClass();
            Console.WriteLine("\nНатисніть Enter...");
            Console.ReadLine();
            Console.Clear();

            // Демонстрація статичного класу ResearchWork
            Console.WriteLine("\n--- Демонстрація статичного класу ResearchWork ---");
            ResearchWork.DemonstrateAllFunctions("research_results.txt");
            Console.WriteLine("\nНатисніть Enter...");
            Console.ReadLine();
            Console.Clear();

            // Показ вмісту всіх створених файлів
            Console.WriteLine("\n--- Вміст створених файлів ---");
            ShowAllResultFiles();
        }

        // Демонстрація класу Student
        static void DemonstrateStudentClass()
        {
            // Створення студента через конструктор з параметрами
            Student student1 = new Student(
                "Шевченко",
                "Тарас",
                3,
                "Комп'ютерні науки",
                "КНУ",
                85.5,
                2
            );

            // Виведення на консоль
            student1.OutputToConsole();

            // Розрахунок рейтингу
            student1.CalculateRating();

            // Запис у файл
            student1.WriteToFile("students.txt");

            // IQ тестування
            student1.SimulateIQTest("iq_results.txt");

            // Створення студента через конструктор без параметрів
            Student student2 = new Student();

            // Заповнення полів через властивості
            student2.LastName = "Франко";
            student2.FirstName = "Іван";
            student2.Course = 4;
            student2.Speciality = "Філологія";
            student2.University = "ЛНУ";
            student2.Rating = 92.0;
            student2.ScientificAchievements = 3;

            // Виведення на консоль
            student2.OutputToConsole();

            // Розрахунок рейтингу
            student2.CalculateRating();

            // Запис у файл
            student2.WriteToFile("students.txt");

            // IQ тестування
            student2.SimulateIQTest("iq_results.txt");
        }

        // Демонстрація класу AcademicMobility
        static void DemonstrateAcademicMobilityClass()
        {
            // Масив міжнародних програм
            string[] programs = {
                "Erasmus+",
                "DAAD",
                "Fulbright",
                "MEXT",
                "Eiffel Excellence"
            };

            // Масив університетів-партнерів
            string[] universities = {
                "Technical University of Munich",
                "Sorbonne University",
                "University of Oxford",
                "MIT",
                "Tokyo University",
                "Heidelberg University"
            };

            // Створення студента для реєстрації
            Student student = new Student(
                "Коваленко",
                "Олена",
                2,
                "Міжнародні відносини",
                "КНЕУ",
                0,
                3
            );

            // Розрахунок рейтингу студента
            student.CalculateRating();

            // Створення відділу академічної мобільності
            AcademicMobility mobility = new AcademicMobility(programs, universities,
                                                            "academic.mobility@univ.edu.ua");

            // Виведення на консоль
            mobility.OutputToConsole();

            // Запис у файл
            mobility.WriteToFile("academic_mobility.txt");

            // Реєстрація студента на програму
            mobility.RegisterStudent(student, "Erasmus+", "registrations.txt");

            // Пошук університетів (бінарний пошук)
            mobility.FindUniversity("MIT");
            mobility.FindUniversity("Harvard");
        }

        // Демонстрація вкладеного класу InterProgram
        static void DemonstrateInterProgramClass()
        {
            // Створення об'єкта вкладеного класу
            AcademicMobility.InterProgram interProgram =
                new AcademicMobility.InterProgram("Erasmus+", 0);

            // Презентація програм в різних країнах
            interProgram.PresentProgramsInCountries();

            // Створення масиву студентів для конкурсу
            Student[] students = new Student[5];
            string[] firstNames = { "Андрій", "Марія", "Іван", "Ольга", "Петро" };
            string[] lastNames = { "Мельник", "Шевченко", "Коваленко", "Бондаренко", "Ткаченко" };

            // Генератор випадкових чисел
            Random rand = new Random();

            // Заповнення масиву студентів
            for (int i = 0; i < students.Length; i++)
            {
                // Створення студента з випадковими даними
                students[i] = new Student(
                    lastNames[i],
                    firstNames[i],
                    rand.Next(2, 5),      // Випадковий курс від 2 до 4
                    "Економіка",
                    "КНЕУ",
                    0,
                    rand.Next(0, 4)        // Випадкова кількість статей від 0 до 3
                );

                // Розрахунок рейтингу
                students[i].CalculateRating();
            }

            // Проведення конкурсу
            interProgram.ConductCompetition(students, 3, "competition_results.txt");
        }

        // Показ вмісту всіх файлів
        static void ShowAllResultFiles()
        {
            // Масив файлів для показу
            string[] files = {
                "students.txt",
                "iq_results.txt",
                "academic_mobility.txt",
                "registrations.txt",
                "competition_results.txt",
                "research_results.txt"
            };

            // Перебір всіх файлів
            foreach (string file in files)
            {
                Console.WriteLine($"\n--- {file} ---");

                // Перевірка на існування файлу
                if (File.Exists(file))
                {
                    // Читання вмісту файлу
                    string content = File.ReadAllText(file, Encoding.GetEncoding(1251));
                    Console.WriteLine(content);
                }
                else
                {
                    Console.WriteLine("Файл не знайдено.");
                }
            }
        }
    }
}