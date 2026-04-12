using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_variant11
{
    class Program
    {
        // Глобальні змінні для збереження масиву та матриці між завданнями
        static int[] array;
        static int[,] matrix;
        static Random random = new Random();

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.GetEncoding(1251);
            Console.InputEncoding = Encoding.GetEncoding(1251);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("==========================================");
                Console.WriteLine("Лабораторна робота №2");
                Console.WriteLine("Маленький Сергій ІПЗ-12(4)");
                Console.WriteLine("Варіант 11");
                Console.WriteLine("==========================================");
                Console.WriteLine("Меню:");
                Console.WriteLine("1. Завдання 1 - Генерація та сортування масиву");
                Console.WriteLine("2. Завдання 2 - Пошук простих чисел (Решето Ератосфена)");
                Console.WriteLine("3. Завдання 3 - Обмін мінімального та максимального елементів");
                Console.WriteLine("4. Завдання 4 - Пошук семикутних чисел");
                Console.WriteLine("5. Завдання 5 - Бінарний пошук елемента в масиві");
                Console.WriteLine("6. Завдання 6 - Генерація та аналіз матриці");
                Console.WriteLine("7. Завдання 7 - Модифікація матриці");
                Console.WriteLine("8. Завдання 8 - Розв'язання нелінійного рівняння");
                Console.WriteLine("9. Завдання 9 - Обробка рядка");
                Console.WriteLine("0. Вихід");
                Console.WriteLine("==========================================");
                Console.Write("Виберіть опцію: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Task1();
                        break;
                    case "2":
                        Task2();
                        break;
                    case "3":
                        Task3();
                        break;
                    case "4":
                        Task4();
                        break;
                    case "5":
                        Task5();
                        break;
                    case "6":
                        Task6();
                        break;
                    case "7":
                        Task7();
                        break;
                    case "8":
                        Task8();
                        break;
                    case "9":
                        Task9();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Невірний вибір. Натисніть будь-яку клавішу...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        // Допоміжні методи (розташовані перед їх викликом)
        static int[] GenerateArray(int size, int min, int max)
        {
            int[] arr = new int[size];
            for (int i = 0; i < size; i++)
                arr[i] = random.Next(min, max + 1);
            return arr;
        }

        static int[,] GenerateMatrix(int rows, int cols, int min, int max)
        {
            int[,] mat = new int[rows, cols];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    mat[i, j] = random.Next(min, max + 1);
            return mat;
        }

        static void PrintArray(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
                Console.Write(arr[i] + " ");
            Console.WriteLine();
        }

        static void PrintMatrix(int[,] mat)
        {
            int rows = mat.GetLength(0);
            int cols = mat.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                    Console.Write($"{mat[i, j],5} ");
                Console.WriteLine();
            }
        }

        static void BubbleSort(int[] arr)
        {
            int n = arr.Length;
            for (int i = 0; i < n - 1; i++)
                for (int j = 0; j < n - i - 1; j++)
                    if (arr[j] > arr[j + 1])
                    {
                        int temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                    }
        }

        static bool[] SieveOfEratosthenes(int n)
        {
            bool[] isPrime = new bool[n + 1];
            for (int i = 2; i <= n; i++)
                isPrime[i] = true;

            for (int i = 2; i * i <= n; i++)
                if (isPrime[i])
                    for (int j = i * i; j <= n; j += i)
                        isPrime[j] = false;

            return isPrime;
        }

        static double F8(double x)
        {
            return 3 * x - Math.Cos(x) - 1;
        }

        static double BisectionMethod(double a, double b, double eps)
        {
            double c;
            while ((b - a) > eps)
            {
                c = (a + b) / 2;
                if (F8(a) * F8(c) < 0)
                    b = c;
                else
                    a = c;
            }
            return (a + b) / 2;
        }

        // Завдання 1: Генерація масиву та бульбашкове сортування
        static void Task1()
        {
            Console.Clear();
            Console.WriteLine("Завдання 1: Генерація масиву та бульбашкове сортування");

            // Введення параметрів масиву
            Console.Write("Введіть кількість елементів масиву: ");
            int size = int.Parse(Console.ReadLine());

            Console.Write("Введіть мінімальне значення: ");
            int min = int.Parse(Console.ReadLine());

            Console.Write("Введіть максимальне значення: ");
            int max = int.Parse(Console.ReadLine());

            // Генерація масиву
            array = GenerateArray(size, min, max);

            Console.WriteLine("\nМасив до сортування:");
            PrintArray(array);

            // Бульбашкове сортування
            BubbleSort(array);

            Console.WriteLine("\nМасив після сортування:");
            PrintArray(array);

            Console.WriteLine("\nНатисніть будь-яку клавішу...");
            Console.ReadKey();
        }

        // Завдання 2: Пошук простих чисел (Решето Ератосфена)
        static void Task2()
        {
            Console.Clear();
            Console.WriteLine("Завдання 2: Пошук простих чисел (Решето Ератосфена)");

            if (array == null)
            {
                Console.WriteLine("Спочатку виконайте завдання 1 для генерації масиву!");
                Console.WriteLine("\nНатисніть будь-яку клавішу...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Поточний масив:");
            PrintArray(array);

            // Знаходимо максимальне значення в масиві для решета Ератосфена
            int maxValue = array.Max();

            // Знаходимо прості числа за допомогою решета Ератосфена
            bool[] isPrime = SieveOfEratosthenes(maxValue);

            // Вибираємо прості числа з масиву
            List<int> primes = new List<int>();
            foreach (int num in array)
            {
                if (num >= 2 && isPrime[num] && !primes.Contains(num))
                {
                    primes.Add(num);
                }
            }

            if (primes.Count > 0)
            {
                Console.WriteLine("\nПрості числа в масиві:");
                foreach (int prime in primes)
                {
                    Console.Write(prime + " ");
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("\nУ масиві немає простих чисел.");
            }

            Console.WriteLine("\nНатисніть будь-яку клавішу...");
            Console.ReadKey();
        }

        // Завдання 3: Обмін мінімального та максимального елементів
        static void Task3()
        {
            Console.Clear();
            Console.WriteLine("Завдання 3: Обмін мінімального та максимального елементів");

            if (array == null)
            {
                Console.WriteLine("Спочатку виконайте завдання 1 для генерації масиву!");
                Console.WriteLine("\nНатисніть будь-яку клавішу...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Поточний масив:");
            PrintArray(array);

            // Знаходимо індекси мінімального та максимального елементів
            int minIndex = 0;
            int maxIndex = 0;

            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] < array[minIndex])
                    minIndex = i;
                if (array[i] > array[maxIndex])
                    maxIndex = i;
            }

            Console.WriteLine($"\nМінімальний елемент: array[{minIndex}] = {array[minIndex]}");
            Console.WriteLine($"Максимальний елемент: array[{maxIndex}] = {array[maxIndex]}");

            // Якщо індекси різні, виконуємо обмін
            if (minIndex != maxIndex)
            {
                // Обмін елементів
                int temp = array[minIndex];
                array[minIndex] = array[maxIndex];
                array[maxIndex] = temp;

                Console.WriteLine("\nМасив після обміну:");
                PrintArray(array);
            }
            else
            {
                Console.WriteLine("\nМінімальний та максимальний елементи збігаються (масив з однакових чисел). Обмін не потрібен.");
            }

            Console.WriteLine("\nНатисніть будь-яку клавішу...");
            Console.ReadKey();
        }

        // Завдання 4: Пошук семикутних чисел
        static void Task4()
        {
            Console.Clear();
            Console.WriteLine("Завдання 4: Пошук семикутних чисел");
            Console.WriteLine("Формула: t_n = (1/2)(5n² - 3n)");

            if (array == null)
            {
                Console.WriteLine("Спочатку виконайте завдання 1 для генерації масиву!");
                Console.WriteLine("\nНатисніть будь-яку клавішу...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Поточний масив:");
            PrintArray(array);

            // Знаходимо максимальне значення в масиві
            int maxValue = array.Max();

            // Генеруємо семикутні числа за формулою t_n = (1/2)(5n² - 3n)
            List<int> heptagonalNumbers = new List<int>();
            int n = 1;
            while (true)
            {
                // Обчислюємо за формулою: t_n = (5n² - 3n) / 2
                double heptagonalDouble = (5.0 * n * n - 3.0 * n) / 2.0;

                // Перевіряємо, чи є число цілим
                if (Math.Abs(heptagonalDouble - Math.Round(heptagonalDouble)) < 0.0001)
                {
                    int heptagonal = (int)Math.Round(heptagonalDouble);

                    if (heptagonal > maxValue)
                        break;

                    heptagonalNumbers.Add(heptagonal);
                    Console.WriteLine($"n={n}: t_n = {heptagonal}");
                }
                n++;
            }

            Console.WriteLine("\nСемикутні числа в послідовності (до " + maxValue + "):");
            foreach (int num in heptagonalNumbers)
            {
                Console.Write(num + " ");
            }
            Console.WriteLine();

            // Лінійний пошук семикутних чисел в масиві
            Console.WriteLine("\nРезультати лінійного пошуку:");
            bool found = false;
            for (int i = 0; i < array.Length; i++)
            {
                if (heptagonalNumbers.Contains(array[i]))
                {
                    Console.WriteLine($"Елемент {array[i]} знайдено за індексом {i}");
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("Семикутні числа не знайдено в масиві.");
            }

            Console.WriteLine("\nНатисніть будь-яку клавішу...");
            Console.ReadKey();
        }

        // Завдання 5: Бінарний пошук з використанням Array.BinarySearch
        static void Task5()
        {
            Console.Clear();
            Console.WriteLine("Завдання 5: Бінарний пошук елемента в масиві");

            if (array == null)
            {
                Console.WriteLine("Спочатку виконайте завдання 1 для генерації масиву!");
                Console.WriteLine("\nНатисніть будь-яку клавішу...");
                Console.ReadKey();
                return;
            }

            // Сортуємо масив для бінарного пошуку
            int[] sortedArray = (int[])array.Clone();
            Array.Sort(sortedArray);

            Console.WriteLine("Відсортований масив:");
            PrintArray(sortedArray);

            Console.Write("\nВведіть елемент для пошуку: ");
            int searchKey = int.Parse(Console.ReadLine());

            // Використання Array.BinarySearch
            int index = Array.BinarySearch(sortedArray, searchKey);

            if (index >= 0)
            {
                Console.WriteLine($"\nЕлемент {searchKey} знайдено за індексом {index} у відсортованому масиві");

                // Пошук всіх входжень (ліворуч і праворуч)
                List<int> allIndices = new List<int>();
                allIndices.Add(index);

                // Пошук ліворуч
                int left = index - 1;
                while (left >= 0 && sortedArray[left] == searchKey)
                {
                    allIndices.Add(left);
                    left--;
                }

                // Пошук праворуч
                int right = index + 1;
                while (right < sortedArray.Length && sortedArray[right] == searchKey)
                {
                    allIndices.Add(right);
                    right++;
                }

                allIndices.Sort();
                Console.WriteLine("Всі індекси входження у відсортованому масиві:");
                foreach (int idx in allIndices)
                {
                    Console.WriteLine($"Індекс {idx}: значення {sortedArray[idx]}");
                }

                // Показуємо оригінальний масив та де знаходяться ці елементи
                Console.WriteLine("\nВідповідні позиції в оригінальному масиві:");
                List<int> originalIndices = new List<int>();
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i] == searchKey)
                    {
                        originalIndices.Add(i);
                    }
                }

                if (originalIndices.Count > 0)
                {
                    Console.WriteLine($"Елемент {searchKey} знайдено в оригінальному масиві на позиціях:");
                    foreach (int idx in originalIndices)
                    {
                        Console.Write(idx + " ");
                    }
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine($"\nЕлемент {searchKey} не знайдено в масиві.");
            }

            Console.WriteLine("\nНатисніть будь-яку клавішу...");
            Console.ReadKey();
        }

        // Завдання 6: Генерація та аналіз матриці
        static void Task6()
        {
            Console.Clear();
            Console.WriteLine("Завдання 6: Генерація та аналіз матриці (студради факультетів)");

            // Введення параметрів матриці
            Console.Write("Введіть кількість факультетів (рядків): ");
            int rows = int.Parse(Console.ReadLine());

            Console.Write("Введіть кількість навчальних років (стовпців): ");
            int cols = int.Parse(Console.ReadLine());

            Console.Write("Введіть мінімальне значення: ");
            int min = int.Parse(Console.ReadLine());

            Console.Write("Введіть максимальне значення: ");
            int max = int.Parse(Console.ReadLine());

            // Генерація матриці
            matrix = GenerateMatrix(rows, cols, min, max);

            Console.WriteLine("\nМатриця (рядки - факультети, стовпці - навчальні роки):");
            PrintMatrix(matrix);

            // Знаходимо факультет і рік з найбільшою кількістю студентів
            int maxStudents = int.MinValue;
            int maxFaculty = -1, maxYear = -1;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (matrix[i, j] > maxStudents)
                    {
                        maxStudents = matrix[i, j];
                        maxFaculty = i;
                        maxYear = j;
                    }
                }
            }

            Console.WriteLine($"\n1. Найбільша кількість студентів: {maxStudents}");
            Console.WriteLine($"   Факультет: {maxFaculty + 1}, Навчальний рік: {maxYear + 1}");

            // Сумарна кількість студентів на заданому факультеті
            Console.Write("\n2. Введіть номер факультету для підрахунку суми (1-" + rows + "): ");
            int facultyNum = int.Parse(Console.ReadLine()) - 1;

            if (facultyNum >= 0 && facultyNum < rows)
            {
                int facultySum = 0;
                for (int j = 0; j < cols; j++)
                {
                    facultySum += matrix[facultyNum, j];
                }
                Console.WriteLine($"   Сумарна кількість студентів на факультеті {facultyNum + 1}: {facultySum}");
            }
            else
            {
                Console.WriteLine("   Невірний номер факультету!");
            }

            // Рік з найменшою сумарною кількістю студентів
            int minYearSum = int.MaxValue;
            int minYear = -1;

            for (int j = 0; j < cols; j++)
            {
                int yearSum = 0;
                for (int i = 0; i < rows; i++)
                {
                    yearSum += matrix[i, j];
                }

                if (yearSum < minYearSum)
                {
                    minYearSum = yearSum;
                    minYear = j;
                }
            }

            Console.WriteLine($"\n3. Навчальний рік з найменшою сумарною кількістю студентів: {minYear + 1}");
            Console.WriteLine($"   Сумарна кількість: {minYearSum}");

            Console.WriteLine("\nНатисніть будь-яку клавішу...");
            Console.ReadKey();
        }

        // Завдання 7: Модифікація матриці
        static void Task7()
        {
            Console.Clear();
            Console.WriteLine("Завдання 7: Модифікація матриці");

            if (matrix == null)
            {
                Console.WriteLine("Спочатку виконайте завдання 6 для генерації матриці!");
                Console.WriteLine("\nНатисніть будь-яку клавішу...");
                Console.ReadKey();
                return;
            }

            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            Console.WriteLine("Поточна матриця:");
            PrintMatrix(matrix);

            // Введення навчального року
            Console.Write($"\nВведіть номер навчального року (1-{cols}): ");
            int year = int.Parse(Console.ReadLine()) - 1;

            if (year < 0 || year >= cols)
            {
                Console.WriteLine("Невірний номер навчального року!");
                Console.WriteLine("\nНатисніть будь-яку клавішу...");
                Console.ReadKey();
                return;
            }

            // Знаходимо мінімальне значення в заданому стовпці
            int minInColumn = int.MaxValue;
            for (int i = 0; i < rows; i++)
            {
                if (matrix[i, year] < minInColumn)
                {
                    minInColumn = matrix[i, year];
                }
            }

            Console.WriteLine($"\nМінімальна кількість студентів у {year + 1} році: {minInColumn}");

            // Створюємо новий список рядків для збереження
            List<int[]> rowsToKeep = new List<int[]>();

            for (int i = 0; i < rows; i++)
            {
                if (matrix[i, year] != minInColumn)
                {
                    int[] row = new int[cols];
                    for (int j = 0; j < cols; j++)
                    {
                        row[j] = matrix[i, j];
                    }
                    rowsToKeep.Add(row);
                }
            }

            Console.WriteLine($"Видалено {rows - rowsToKeep.Count} факультетів з мінімальною кількістю");

            if (rowsToKeep.Count == 0)
            {
                Console.WriteLine("Після видалення не залишилося рядків!");
                Console.WriteLine("\nНатисніть будь-яку клавішу...");
                Console.ReadKey();
                return;
            }

            // Сортуємо рядки за зростанням (за першим елементом)
            rowsToKeep.Sort((a, b) => a[0].CompareTo(b[0]));

            // Створюємо нову матрицю
            int[,] newMatrix = new int[rowsToKeep.Count, cols];
            for (int i = 0; i < rowsToKeep.Count; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    newMatrix[i, j] = rowsToKeep[i][j];
                }
            }

            matrix = newMatrix;

            Console.WriteLine($"\nМатриця після видалення та сортування рядків:");
            PrintMatrix(matrix);

            // Пошук значення в матриці
            Console.Write("\nВведіть значення для пошуку в матриці: ");
            int searchValue = int.Parse(Console.ReadLine());

            Console.WriteLine("Результати пошуку:");
            bool found = false;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == searchValue)
                    {
                        Console.WriteLine($"   Значення {searchValue} знайдено на позиції [{i + 1}, {j + 1}]");
                        found = true;
                    }
                }
            }

            if (!found)
            {
                Console.WriteLine($"   Значення {searchValue} не знайдено в матриці.");
            }

            Console.WriteLine("\nНатисніть будь-яку клавішу...");
            Console.ReadKey();
        }

        // Завдання 8: Розв'язання нелінійного рівняння методом бісекції
        static void Task8()
        {
            Console.Clear();
            Console.WriteLine("Завдання 8: Розв'язання нелінійного рівняння 3x - cos(x) - 1 = 0");
            Console.WriteLine("Метод половинного ділення (бісекції)");

            Console.Write("Введіть точність обчислень (наприклад, 0,0001): ");
            double eps = double.Parse(Console.ReadLine());

            Console.Write("Введіть ліву межу інтервалу пошуку: ");
            double left = double.Parse(Console.ReadLine());

            Console.Write("Введіть праву межу інтервалу пошуку: ");
            double right = double.Parse(Console.ReadLine());

            Console.WriteLine("\nАналіз функції на інтервалі:");
            Console.WriteLine($"f({left}) = {F8(left):F6}");
            Console.WriteLine($"f({right}) = {F8(right):F6}");

            if (F8(left) * F8(right) > 0)
            {
                Console.WriteLine("На вказаному інтервалі функція не змінює знак. Корінь може бути відсутнім.");
            }

            try
            {
                double root = BisectionMethod(left, right, eps);
                Console.WriteLine($"\nЗнайдений корінь: x = {root:F6}");

                // Перевірка
                double check = F8(root);
                Console.WriteLine($"\nПеревірка:");
                Console.WriteLine($"3 * {root:F6} - cos({root:F6}) - 1 = {check:F10}");

                if (Math.Abs(check) < eps * 10)
                {
                    Console.WriteLine("Корінь знайдено правильно (похибка в межах допустимого)");
                }
                else
                {
                    Console.WriteLine("Можлива помилка обчислень (велика похибка)");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }

            Console.WriteLine("\nНатисніть будь-яку клавішу...");
            Console.ReadKey();
        }

        // Завдання 9: Обробка рядка
        static void Task9()
        {
            Console.Clear();
            Console.WriteLine("Завдання 9: Обробка рядка");
            Console.WriteLine("Введіть рядок символів (слова, розділові знаки):");

            string input = Console.ReadLine();

            Console.WriteLine("\nОригінальний рядок:");
            Console.WriteLine($"\"{input}\"");

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("\nОброблений рядок:");
                Console.WriteLine("\"\"");
                Console.ReadKey();
                return;
            }

            // Замінюємо всі НЕ літери на пробіли
            StringBuilder cleaned = new StringBuilder();

            foreach (char c in input)
            {
                if (char.IsLetter(c))
                    cleaned.Append(c);
                else
                    cleaned.Append(' ');
            }

            // Розбиваємо на слова
            string[] words = cleaned
                .ToString()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // Видаляємо однолітерні слова
            List<string> filtered = new List<string>();

            foreach (string word in words)
            {
                if (word.Length > 1)
                    filtered.Add(word);
            }

            // Збираємо назад в рядок
            string result = string.Join(" ", filtered);

            Console.WriteLine("\nОброблений рядок:");
            Console.WriteLine($"\"{result}\"");

            Console.WriteLine("\nНатисніть будь-яку клавішу...");
            Console.ReadKey();
        }
    }
}