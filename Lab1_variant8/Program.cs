using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_Variant8

{
    class Program

    {
        static void Main(string[] args)

        {
            // Встановлюємо кодування UTF-8 для коректного відображення українських літер

            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Виведення заголовку програми та інформації про студента
            Console.WriteLine("Лабораторна робота №1, варiант 8");
            Console.WriteLine();

            Console.WriteLine("Студент: Маленький Сергій");
            Console.WriteLine("Група: ІПЗ 12-4");
            Console.WriteLine();

            int choice; // Змінна для вибору пункту меню

            do

            {

                // Виведення меню на екран

                Console.WriteLine("=================================");

                Console.WriteLine("МЕНЮ:");

                Console.WriteLine();

                Console.WriteLine("1 - Завдання 1 (Анкета + цилiндр)");

                Console.WriteLine("2 - Завдання 2 (Математичний вираз)");

                Console.WriteLine("3 - Завдання 3 (Функцiя f(x))");

                Console.WriteLine("4 - Завдання 4 (Рейтинг унiверситетiв)");

                Console.WriteLine("5 - Завдання 5 (Сума ряду)");

                Console.WriteLine("0 - Вихiд");

                Console.WriteLine();

                Console.WriteLine("=================================");

                Console.Write("Ваш вибiр: ");

                // Зчитування вибору користувача

                choice = int.Parse(Console.ReadLine());

                Console.WriteLine();

                // Оператор switch для обробки вибору

                switch (choice)

                {

                    case 1:

                        Console.WriteLine("--- Завдання 1 ---");
                        Console.WriteLine();

                        Console.WriteLine("Мої анкетнi данi:");
                        Console.WriteLine();

                        Console.WriteLine("Прiзвище: Маленький");
                        Console.WriteLine("Iм'я: Сергій");
                        Console.WriteLine("Вiк: 17");
                        Console.WriteLine("Група: ІПЗ 12-4");
                        Console.WriteLine("Курс: 1");
                        Console.WriteLine("Email: malenkiysergii@knu.ua");
                        Console.WriteLine();

                        Console.WriteLine("Обчислення об'єму цилiндра:");
                        Console.WriteLine();

                        Console.Write("Введiть радiус основи (r): ");
                        double r = double.Parse(Console.ReadLine());
                        Console.WriteLine();

                        Console.Write("Введiть висоту (h): ");
                        double h = double.Parse(Console.ReadLine());
                        Console.WriteLine();

                        // Обчислення об'єму цилiндра за формулою V = π * r² * h
                        double volume = Math.PI * r * r * h;
                        Console.WriteLine("Об'єм цилiндра = " + volume.ToString("F2"));
                        Console.WriteLine();

                        break;

                    case 2:

                        Console.WriteLine("--- Завдання 2 ---");
                        Console.WriteLine();

                        Console.WriteLine("Обчислення: X = (sin²(a) * cos²(b) / √(a - b)) * sinh(2a)");
                        Console.WriteLine();

                        Console.Write("Введiть a (a > b): ");
                        double a = double.Parse(Console.ReadLine());
                        Console.WriteLine();

                        Console.Write("Введiть b: ");
                        double b_val = double.Parse(Console.ReadLine());
                        Console.WriteLine();

                        // Перевірка, чи a більше за b (щоб корінь існував)
                        if (a <= b_val)

                        {

                            Console.WriteLine("Помилка: a має бути бiльше за b!");

                        }

                        else

                        {

                            // Обчислення частин формули
                            double sin_a = Math.Sin(a);                    // sin(a)
                            double cos_b = Math.Cos(b_val);                // cos(b)
                            double sin_squared = sin_a * sin_a;            // sin²(a)
                            double cos_squared = cos_b * cos_b;            // cos²(b)
                            double sqrt_ab = Math.Sqrt(a - b_val);         // √(a - b)
                            double sinh_2a = Math.Sinh(2 * a);             // sinh(2a)

                            // Обчислення кінцевого результату
                            double X = (sin_squared * cos_squared / sqrt_ab) * sinh_2a;
                            Console.WriteLine("Результат X = " + X.ToString("F6"));
                        }

                        Console.WriteLine();

                        break;

                    case 3:
                        Console.WriteLine("--- Завдання 3 ---");
                        Console.WriteLine();

                        Console.WriteLine("Функцiя f(x):");
                        Console.WriteLine();

                        Console.WriteLine("f(x) = -1, якщо x < -1");
                        Console.WriteLine("f(x) = x², якщо -1 <= x < 2");
                        Console.WriteLine("f(x) = 2x, якщо x >= 2");
                        Console.WriteLine();

                        Console.Write("Введiть x: ");
                        double x = double.Parse(Console.ReadLine());
                        Console.WriteLine();

                        double fx; // Змінна для результату функції

                        // Визначення значення функції залежно від x
                        if (x < -1)

                        {

                            fx = -1;
                            Console.WriteLine("x < -1, тому f(x) = -1");

                        }

                        else if (x < 2)

                        {

                            fx = x * x;
                            Console.WriteLine("-1 <= x < 2, тому f(x) = x² = " + fx);

                        }

                        else

                        {

                            fx = 2 * x;
                            Console.WriteLine("x >= 2, тому f(x) = 2x = " + fx);

                        }

                        Console.WriteLine();

                        Console.WriteLine("Результат f(" + x + ") = " + fx);
                        Console.WriteLine();

                        break;

                    case 4:
                        Console.WriteLine("--- Завдання 4 ---");
                        Console.WriteLine();

                        Console.WriteLine("Введiть рейтинг унiверситету (1-5):");
                        Console.WriteLine();

                        Console.Write("Рейтинг: ");
                        int rating = int.Parse(Console.ReadLine());
                        Console.WriteLine();

                        string university = ""; // Змінна для назви університету

                        // Визначення університету за рейтингом
                        switch (rating)

                        {

                            case 1:

                                university = "Київський нацiональний унiверситет iменi Тараса Шевченка";

                                break;

                            case 2:

                                university = "Києво-Могилянська академiя";

                                break;

                            case 3:

                                university = "Нацiональний технiчний унiверситет 'КПI'";

                                break;

                            case 4:

                                university = "Львiвський нацiональний унiверситет iменi Iвана Франка";

                                break;

                            case 5:

                                university = "Харкiвський нацiональний унiверситет iменi В. Н. Каразiна";

                                break;

                            default:

                                Console.WriteLine("Невiрний рейтинг! Введiть число вiд 1 до 5.");

                                break;

                        }

                        Console.WriteLine();

                        // Виведення результату, якщо рейтинг правильний
                        if (university != "")

                        {

                            Console.WriteLine("Унiверситет з рейтингом " + rating + ": " + university);

                        }

                        Console.WriteLine();

                        break;

                    case 5:

                        Console.WriteLine("--- Завдання 5 ---");
                        Console.WriteLine();

                        Console.WriteLine("Сума ряду: Σ(k=1 до 2n) [(-1)^(k+1) / (k * k-√(k+1))]");
                        Console.WriteLine("де k-√(k+1) - корiнь степеня k з (k+1)");
                        Console.WriteLine();

                        Console.Write("Введiть натуральне число n: ");
                        int n = int.Parse(Console.ReadLine());
                        Console.WriteLine();

                        // Перевірка, чи n є натуральним числом
                        if (n <= 0)

                        {

                            Console.WriteLine("Помилка: n має бути натуральним числом!");

                        }

                        else

                        {

                            double sum = 0; // Змінна для суми ряду
                            int limit = 2 * n; // Верхня межа сумування: 2n

                            Console.WriteLine("Обчислюємо суму для k вiд 1 до " + limit + ":");
                            Console.WriteLine();

                            // Цикл для обчислення суми ряду
                            for (int k = 1; k <= limit; k++)

                            {

                                // Обчислення (-1)^(k+1)
                                // Якщо (k+1) парне -> (-1)^(k+1) = 1
                                // Якщо (k+1) непарне -> (-1)^(k+1) = -1
                                double sign = Math.Pow(-1, k + 1);

                                // Обчислення k-кореня з (k+1) = (k+1)^(1/k)
                                // Це корiнь степеня k з числа (k+1)
                                double k_root = Math.Pow(k + 1, 1.0 / k);

                                // Обчислення знаменника: k * k-√(k+1)
                                double denominator = k * k_root;

                                // Обчислення поточного члена ряду
                                double term = sign / denominator;

                                // Додавання до загальної суми
                                sum += term;

                                // Виведення проміжних результатів
                                Console.WriteLine($"Для k = {k}:");
                                Console.WriteLine($"  (-1)^{k + 1} = {sign}");
                                Console.WriteLine($"  {k}√({k + 1}) = ({(k + 1)})^(1/{k}) = {k_root:F6}");
                                Console.WriteLine($"  Знаменник: {k} * {k_root:F6} = {denominator:F6}");
                                Console.WriteLine($"  Член ряду: {sign} / {denominator:F6} = {term:F6}");
                                Console.WriteLine();

                            }

                            Console.WriteLine("=================================");
                            Console.WriteLine("Загальна сума = " + sum.ToString("F6"));

                        }

                        Console.WriteLine();

                        break;

                    case 0:

                        Console.WriteLine("Вихiд з програми...");
                        Console.WriteLine();

                        break;

                    default:

                        Console.WriteLine("Невiрний вибiр! Спробуйте ще раз.");
                        Console.WriteLine();

                        break;

                }

            } 
            
            while (choice != 0); // Цикл триває, поки не вибрано 0

            Console.WriteLine("Натиснiть будь-яку клавiшу для виходу...");
            Console.ReadKey(); // Очікування натискання клавіші перед закриттям

        }
    }
}