using System;
using System.IO;
using System.Text;

namespace Lab3_Var14
{
    // Частковий клас AcademicMobility з вкладеним класом
    partial class AcademicMobility
    {
        // Вкладений клас InterProgram
        public class InterProgram
        {
            // Поля вкладеного класу
            private string programName;        // Назва програми
            private int participantsCount;      // Кількість учасників

            // Конструктор без параметрів
            public InterProgram()
            {
                programName = "";
                participantsCount = 0;
            }

            // Конструктор з параметрами
            public InterProgram(string name, int count)
            {
                programName = name;
                participantsCount = count;
            }

            // Властивості
            public string ProgramName
            {
                get { return programName; }
                set { programName = value; }
            }

            public int ParticipantsCount
            {
                get { return participantsCount; }
                set { participantsCount = value; }
            }

            // Презентація програм в різних країнах
            public void PresentProgramsInCountries()
            {
                Console.WriteLine("\n=== ПРЕЗЕНТАЦІЯ ПРОГРАМ ===");
                Console.WriteLine("Програма: " + programName);
                Console.WriteLine("Учасників: " + participantsCount);

                // Масив країн
                string[] countries = { "Німеччина", "США", "Франція", "Великобританія", "Японія", "Польща" };

                // Масив програм для кожної країни
                string[][] programsByCountry = new string[][]
                {
                    new string[] { "DAAD", "Erasmus+", "Deutschlandstipendium" },
                    new string[] { "Fulbright", "Gilman Scholarship", "Hubert Humphrey" },
                    new string[] { "Eiffel Excellence", "Charpak", "Campus France" },
                    new string[] { "Chevening", "Marshall Scholarship", "Gates Cambridge" },
                    new string[] { "MEXT", "JASSO", "Falcon" },
                    new string[] { "Banach Scholarship", "CEEPUS", "Visegrad Fund" }
                };

                // Виведення інформації для кожної країни
                for (int i = 0; i < countries.Length; i++)
                {
                    Console.WriteLine("\nКраїна: " + countries[i]);
                    Console.WriteLine("Доступні програми:");

                    for (int j = 0; j < programsByCountry[i].Length; j++)
                    {
                        Console.WriteLine("  • " + programsByCountry[i][j]);
                    }
                }
            }

            // Проведення конкурсу
            public void ConductCompetition(Student[] students, int winnersCount, string fileName)
            {
                Console.WriteLine("\n=== КОНКУРС НА ПРОГРАМУ '" + programName + "' ===");
                Console.WriteLine("Претендентів: " + students.Length);
                Console.WriteLine("Переможців: " + winnersCount);

                // Копіювання масиву для сортування
                Student[] sortedStudents = new Student[students.Length];
                Array.Copy(students, sortedStudents, students.Length);

                // Сортування бульбашкою
                for (int i = 0; i < sortedStudents.Length - 1; i++)
                {
                    for (int j = 0; j < sortedStudents.Length - i - 1; j++)
                    {
                        if (sortedStudents[j].Rating < sortedStudents[j + 1].Rating)
                        {
                            Student temp = sortedStudents[j];
                            sortedStudents[j] = sortedStudents[j + 1];
                            sortedStudents[j + 1] = temp;
                        }
                    }
                }

                // Виведення відсортованого списку
                Console.WriteLine("\nРейтинг претендентів:");
                for (int i = 0; i < sortedStudents.Length; i++)
                {
                    Console.WriteLine((i + 1) + ". " + sortedStudents[i].LastName + " " +
                                    sortedStudents[i].FirstName + " - " + sortedStudents[i].Rating);
                }

                // Вибір переможців
                Console.WriteLine("\nПЕРЕМОЖЦІ:");
                for (int i = 0; i < winnersCount && i < sortedStudents.Length; i++)
                {
                    Console.WriteLine((i + 1) + ". " + sortedStudents[i].LastName + " " +
                                    sortedStudents[i].FirstName + " (" + sortedStudents[i].Rating + ")");
                }

                // Оновлення кількості учасників
                participantsCount += winnersCount;
                Console.WriteLine("Загальна кількість учасників: " + participantsCount);

                // Запис результатів у файл
                try
                {
                    StreamWriter writer = new StreamWriter(fileName, true, Encoding.GetEncoding(1251));

                    writer.WriteLine(DateTime.Now + " - КОНКУРС НА ПРОГРАМУ '" + programName + "'");
                    writer.WriteLine("Кількість претендентів: " + students.Length);
                    writer.WriteLine("ПЕРЕМОЖЦІ:");

                    for (int i = 0; i < winnersCount && i < sortedStudents.Length; i++)
                    {
                        writer.WriteLine((i + 1) + ". " + sortedStudents[i].LastName + " " +
                                       sortedStudents[i].FirstName + " - рейтинг: " + sortedStudents[i].Rating);
                    }

                    writer.WriteLine("---------------------------");
                    writer.Close();

                    Console.WriteLine("Результати збережено у файл " + fileName);
                }
                catch (Exception error)
                {
                    Console.WriteLine("Помилка: " + error.Message);
                }
            }
        }
    }
}