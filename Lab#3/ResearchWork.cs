using System;
using System.IO;
using System.Text;

namespace Lab3_Var14
{
    // Статичний клас ResearchWork
    static class ResearchWork
    {
        // Функція 2: сума на побічній діагоналі та нижче
        public static double CalculateSumBelowSecondaryDiagonal(double[,] matrix)
        {
            int size = matrix.GetLength(0);
            double sum = 0;

            Console.WriteLine("\nФункція 2: Сума на побічній діагоналі");

            // Прохід по всіх елементах нижче побічної діагоналі
            for (int i = 0; i < size; i++)
            {
                for (int j = size - 1 - i; j < size; j++)
                {
                    sum += matrix[i, j];
                }
            }

            Console.WriteLine("Результат: " + sum);
            return sum;
        }

        // Функція 6: середнє арифметичне першого рядка
        public static double CalculateFirstRowAverage(double[,] matrix)
        {
            int cols = matrix.GetLength(1);
            double sum = 0;

            Console.WriteLine("\nФункція 6: Середнє першого рядка");

            // Сума елементів першого рядка
            for (int j = 0; j < cols; j++)
            {
                sum += matrix[0, j];
            }

            double average = sum / cols;
            Console.WriteLine("Результат: " + average);
            return average;
        }

        // Функція 9: перевірка на прості числа
        public static bool CheckIfAllElementsPrime(double[,] matrix)
        {
            Console.WriteLine("\nФункція 9: Перевірка простих чисел");

            // Перебір всіх елементів матриці
            foreach (double element in matrix)
            {
                int number = (int)element;

                // Числа менші або рівні 1 не є простими
                if (number <= 1)
                {
                    Console.WriteLine("Число " + number + " не просте");
                    return false;
                }

                // Перевірка дільників
                bool isPrime = true;
                for (int i = 2; i <= Math.Sqrt(number); i++)
                {
                    if (number % i == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }

                if (!isPrime)
                {
                    Console.WriteLine("Число " + number + " не просте");
                    return false;
                }
            }

            Console.WriteLine("Всі числа прості!");
            return true;
        }

        // Генерація випадкової матриці
        public static double[,] CreateRandomMatrix(int size, int minValue, int maxValue)
        {
            double[,] matrix = new double[size, size];
            Random random = new Random();

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix[i, j] = random.Next(minValue, maxValue + 1);
                }
            }

            return matrix;
        }

        // Виведення матриці
        public static void PrintMatrix(double[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(matrix[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }

        // Демонстрація всіх функцій
        public static void DemonstrateAllFunctions(string fileName)
        {
            Console.WriteLine("\n=== RESEARCHWORK ===");

            // Створення випадкової матриці 5x5
            double[,] matrix = CreateRandomMatrix(5, 2, 30);

            Console.WriteLine("Матриця 5x5:");
            PrintMatrix(matrix);

            // Виклик функцій
            double sumDiagonal = CalculateSumBelowSecondaryDiagonal(matrix);
            double averageFirstRow = CalculateFirstRowAverage(matrix);
            bool allPrime = CheckIfAllElementsPrime(matrix);

            // Запис у файл
            try
            {
                StreamWriter writer = new StreamWriter(fileName, true, Encoding.GetEncoding(1251));

                writer.WriteLine(DateTime.Now + " - РЕЗУЛЬТАТИ");
                writer.WriteLine("Функція 2: " + sumDiagonal);
                writer.WriteLine("Функція 6: " + averageFirstRow);
                writer.WriteLine("Функція 9: " + allPrime);
                writer.WriteLine("==============================================\n");

                writer.Close();

                Console.WriteLine("\nРезультати збережено у файл " + fileName);
            }
            catch (Exception error)
            {
                Console.WriteLine("Помилка: " + error.Message);
            }
        }
    }
}