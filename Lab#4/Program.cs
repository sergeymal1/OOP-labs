using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace Lab4_Var14
{
    // ==== Базовий клас КНИГА ====
    class Book
    {
        // Статичний random для всіх об'єктів
        protected static Random random = new Random();

        // Закриті поля
        private string title;
        private double price;

        // Конструктор з параметрами
        public Book(string title, double price)
        {
            this.title = title;
            this.price = price;
        }

        // Властивості
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        // Виведення на консоль
        public virtual void OutputToConsole()
        {
            Console.WriteLine("\n=== КНИГА ===");
            Console.WriteLine("Назва: " + title);
            Console.WriteLine("Вартість: " + price + " грн");
        }

        // Запис у файл
        public virtual void WriteToFile(string fileName)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName, true))
                {
                    writer.WriteLine("=== КНИГА ===");
                    writer.WriteLine("Назва: " + title);
                    writer.WriteLine("Вартість: " + price + " грн");
                    writer.WriteLine("---------------------------");
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Помилка: " + error.Message);
            }
        }

        // Розрахунок рейтингу популярності
        public virtual string CalculatePopularityRating()
        {
            int weeklySales = random.Next(0, 1000);
            int monthlySales = random.Next(0, 5000);

            Console.WriteLine("\n=== Розрахунок рейтингу ===");
            Console.WriteLine("Продажі за тиждень: " + weeklySales);
            Console.WriteLine("Продажі за місяць: " + monthlySales);

            string status;
            if (weeklySales > 500 || monthlySales > 2000)
                status = "БЕСТСЕЛЕР!";
            else if (weeklySales > 200 || monthlySales > 800)
                status = "Популярна";
            else
                status = "Звичайна";

            Console.WriteLine("Статус: " + status);
            return status;
        }
    }

    // ==== Похідний клас ПІДРУЧНИК ====
    class Textbook : Book
    {
        // Специфічні поля
        private string author;
        private int pages;
        private double popularityRating;
        private string language;

        // Конструктор з параметрами
        public Textbook(string title, double price, string author, int pages, string language)
            : base(title, price)
        {
            this.author = author;
            this.pages = pages;
            this.language = language;
            this.popularityRating = 0;
        }

        // Властивості
        public string Author
        {
            get { return author; }
            set { author = value; }
        }

        public int Pages
        {
            get { return pages; }
            set { pages = value; }
        }

        public double PopularityRating
        {
            get { return popularityRating; }
            set { popularityRating = value; }
        }

        public string Language
        {
            get { return language; }
            set { language = value; }
        }

        // Виведення на консоль
        public override void OutputToConsole()
        {
            base.OutputToConsole();
            Console.WriteLine("=== ПІДРУЧНИК ===");
            Console.WriteLine("Автор: " + author);
            Console.WriteLine("Сторінок: " + pages);
            Console.WriteLine("Мова: " + language);
            Console.WriteLine("Рейтинг: " + popularityRating);
        }

        // Запис у файл
        public override void WriteToFile(string fileName)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName, true))
                {
                    writer.WriteLine("=== ПІДРУЧНИК ===");
                    writer.WriteLine("Назва: " + Title);
                    writer.WriteLine("Вартість: " + Price + " грн");
                    writer.WriteLine("Автор: " + author);
                    writer.WriteLine("Сторінок: " + pages);
                    writer.WriteLine("Мова: " + language);
                    writer.WriteLine("Рейтинг: " + popularityRating);
                    writer.WriteLine("---------------------------");
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Помилка: " + error.Message);
            }
        }

        // Розрахунок складності підручника
        public override string CalculatePopularityRating()
        {
            int studentsCount = random.Next(10, 200);
            int completedTasks = random.Next(0, studentsCount);
            double completionRate = (double)completedTasks / studentsCount * 100;

            Console.WriteLine("\n=== Складність підручника ===");
            Console.WriteLine("Студентів: " + studentsCount);
            Console.WriteLine("Виконали завдання: " + completedTasks);
            Console.WriteLine("Відсоток: " + completionRate.ToString("F2") + "%");

            if (completionRate < 30)
            {
                Console.WriteLine("Складність: Дуже складний");
                popularityRating = 2.0;
            }
            else if (completionRate < 60)
            {
                Console.WriteLine("Складність: Середня");
                popularityRating = 3.5;
            }
            else
            {
                Console.WriteLine("Складність: Легкий");
                popularityRating = 5.0;
            }

            return "Складність визначено";
        }

        // Перевірка вільного доступу
        public bool CheckFreeAccess()
        {
            bool hasFreeAccess = (popularityRating > 4.0) || (Price < 200);
            Console.WriteLine("\n=== Вільний доступ ===");
            Console.WriteLine("Рейтинг: " + popularityRating);
            Console.WriteLine("Ціна: " + Price + " грн");
            Console.WriteLine("Доступ: " + (hasFreeAccess ? "Вільний" : "Обмежений"));
            return hasFreeAccess;
        }
    }

    // ==== Похідний клас КОНСПЕКТ ====
    class Synopsis : Book
    {
        // Специфічні поля
        private string studentName;
        private int pages;
        private double efficiency;
        private bool freeAccess;

        // Конструктор з параметрами
        public Synopsis(string title, double price, string studentName, int pages, bool freeAccess)
            : base(title, price)
        {
            this.studentName = studentName;
            this.pages = pages;
            this.freeAccess = freeAccess;
            this.efficiency = 0;
        }

        // Властивості
        public string StudentName
        {
            get { return studentName; }
            set { studentName = value; }
        }

        public int Pages
        {
            get { return pages; }
            set { pages = value; }
        }

        public double Efficiency
        {
            get { return efficiency; }
            set { efficiency = value; }
        }

        public bool FreeAccess
        {
            get { return freeAccess; }
            set { freeAccess = value; }
        }

        // Виведення на консоль
        public override void OutputToConsole()
        {
            base.OutputToConsole();
            Console.WriteLine("=== КОНСПЕКТ ===");
            Console.WriteLine("Студент: " + studentName);
            Console.WriteLine("Сторінок: " + pages);
            Console.WriteLine("Ефективність: " + efficiency.ToString("F2") + "%");
            Console.WriteLine("Вільний доступ: " + (freeAccess ? "Так" : "Ні"));
        }

        // Запис у файл
        public override void WriteToFile(string fileName)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName, true))
                {
                    writer.WriteLine("=== КОНСПЕКТ ===");
                    writer.WriteLine("Назва: " + Title);
                    writer.WriteLine("Вартість: " + Price + " грн");
                    writer.WriteLine("Студент: " + studentName);
                    writer.WriteLine("Сторінок: " + pages);
                    writer.WriteLine("Ефективність: " + efficiency.ToString("F2") + "%");
                    writer.WriteLine("Вільний доступ: " + (freeAccess ? "Так" : "Ні"));
                    writer.WriteLine("---------------------------");
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Помилка: " + error.Message);
            }
        }

        // Розрахунок ефективності конспекту
        public override string CalculatePopularityRating()
        {
            // rememberedInfo від 1 до 99, щоб уникнути ділення на 0
            int rememberedInfo = random.Next(1, 100);
            int understoodInfo = random.Next(0, rememberedInfo + 1);

            // Ефективність = (усвідомив / запам'ятав) * 100
            efficiency = (double)understoodInfo / rememberedInfo * 100;

            if (efficiency > 100) efficiency = 100;

            Console.WriteLine("\n=== Ефективність конспекту ===");
            Console.WriteLine("Запам'ятав: " + rememberedInfo + "%");
            Console.WriteLine("Усвідомив: " + understoodInfo + "%");
            Console.WriteLine("Сторінок: " + pages);
            Console.WriteLine("Ефективність: " + efficiency.ToString("F2") + "%");

            if (efficiency > 80)
                Console.WriteLine("Рівень: Високий");
            else if (efficiency > 50)
                Console.WriteLine("Рівень: Середній");
            else
                Console.WriteLine("Рівень: Низький");

            return "Ефективність визначено";
        }
    }

    // ==== Інтерфейс для другої версії ====
    interface IBook
    {
        string Title { get; set; }
        double Price { get; set; }
        void OutputToConsole();
        void WriteToFile(string fileName);
        string CalculatePopularityRating();
    }

    // Клас, що реалізує інтерфейс
    class BookInterface : IBook
    {
        protected static Random random = new Random();
        private string title;
        private double price;

        public BookInterface(string title, double price)
        {
            this.title = title;
            this.price = price;
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        public virtual void OutputToConsole()
        {
            Console.WriteLine("\n=== КНИГА (інтерфейс) ===");
            Console.WriteLine("Назва: " + title);
            Console.WriteLine("Ціна: " + price + " грн");
        }

        public virtual void WriteToFile(string fileName)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName, true))
                {
                    writer.WriteLine("=== КНИГА (інтерфейс) ===");
                    writer.WriteLine("Назва: " + title);
                    writer.WriteLine("Ціна: " + price + " грн");
                    writer.WriteLine("---------------------------");
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Помилка: " + error.Message);
            }
        }

        public virtual string CalculatePopularityRating()
        {
            int sales = random.Next(0, 1000);
            string status = sales > 500 ? "БЕСТСЕЛЕР" : "Звичайна";
            Console.WriteLine("\nПродажі: " + sales + ", Статус: " + status);
            return status;
        }
    }

    // Підручник через інтерфейс
    class TextbookInterface : BookInterface
    {
        private string author;
        private int pages;

        public TextbookInterface(string title, double price, string author, int pages)
            : base(title, price)
        {
            this.author = author;
            this.pages = pages;
        }

        public override void OutputToConsole()
        {
            base.OutputToConsole();
            Console.WriteLine("Автор: " + author);
            Console.WriteLine("Сторінок: " + pages);
        }

        public override void WriteToFile(string fileName)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName, true))
                {
                    writer.WriteLine("=== ПІДРУЧНИК (інтерфейс) ===");
                    writer.WriteLine("Назва: " + Title);
                    writer.WriteLine("Ціна: " + Price + " грн");
                    writer.WriteLine("Автор: " + author);
                    writer.WriteLine("Сторінок: " + pages);
                    writer.WriteLine("---------------------------");
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Помилка: " + error.Message);
            }
        }
    }

    // Конспект через інтерфейс
    class SynopsisInterface : BookInterface
    {
        private string studentName;
        private int pages;

        public SynopsisInterface(string title, double price, string studentName, int pages)
            : base(title, price)
        {
            this.studentName = studentName;
            this.pages = pages;
        }

        public override void OutputToConsole()
        {
            base.OutputToConsole();
            Console.WriteLine("Студент: " + studentName);
            Console.WriteLine("Сторінок: " + pages);
        }

        public override void WriteToFile(string fileName)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName, true))
                {
                    writer.WriteLine("=== КОНСПЕКТ (інтерфейс) ===");
                    writer.WriteLine("Назва: " + Title);
                    writer.WriteLine("Ціна: " + Price + " грн");
                    writer.WriteLine("Студент: " + studentName);
                    writer.WriteLine("Сторінок: " + pages);
                    writer.WriteLine("---------------------------");
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Помилка: " + error.Message);
            }
        }
    }

    // ==== Абстрактний клас для третьої версії ====
    abstract class AbstractBook
    {
        protected static Random random = new Random();
        private string title;
        private double price;

        public AbstractBook(string title, double price)
        {
            this.title = title;
            this.price = price;
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        public abstract void OutputToConsole();
        public abstract void WriteToFile(string fileName);
        public abstract string CalculatePopularityRating();
    }

    // Абстрактний підручник
    class AbstractTextbook : AbstractBook
    {
        private string author;
        private int pages;

        public AbstractTextbook(string title, double price, string author, int pages)
            : base(title, price)
        {
            this.author = author;
            this.pages = pages;
        }

        public override void OutputToConsole()
        {
            Console.WriteLine("\n=== АБСТРАКТНИЙ ПІДРУЧНИК ===");
            Console.WriteLine("Назва: " + Title);
            Console.WriteLine("Ціна: " + Price + " грн");
            Console.WriteLine("Автор: " + author);
            Console.WriteLine("Сторінок: " + pages);
        }

        public override void WriteToFile(string fileName)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName, true))
                {
                    writer.WriteLine("=== АБСТРАКТНИЙ ПІДРУЧНИК ===");
                    writer.WriteLine("Назва: " + Title);
                    writer.WriteLine("Ціна: " + Price + " грн");
                    writer.WriteLine("Автор: " + author);
                    writer.WriteLine("Сторінок: " + pages);
                    writer.WriteLine("---------------------------");
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Помилка: " + error.Message);
            }
        }

        public override string CalculatePopularityRating()
        {
            int students = random.Next(10, 200);
            int completed = random.Next(0, students);
            double rate = (double)completed / students * 100;

            Console.WriteLine("\nСтудентів: " + students);
            Console.WriteLine("Виконали: " + completed);
            Console.WriteLine("Результат: " + (rate > 60 ? "Популярний" : "Непопулярний"));
            return rate > 60 ? "Популярний" : "Непопулярний";
        }
    }

    // Абстрактний конспект
    class AbstractSynopsis : AbstractBook
    {
        private string studentName;
        private int pages;

        public AbstractSynopsis(string title, double price, string studentName, int pages)
            : base(title, price)
        {
            this.studentName = studentName;
            this.pages = pages;
        }

        public override void OutputToConsole()
        {
            Console.WriteLine("\n=== АБСТРАКТНИЙ КОНСПЕКТ ===");
            Console.WriteLine("Назва: " + Title);
            Console.WriteLine("Ціна: " + Price + " грн");
            Console.WriteLine("Студент: " + studentName);
            Console.WriteLine("Сторінок: " + pages);
        }

        public override void WriteToFile(string fileName)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName, true))
                {
                    writer.WriteLine("=== АБСТРАКТНИЙ КОНСПЕКТ ===");
                    writer.WriteLine("Назва: " + Title);
                    writer.WriteLine("Ціна: " + Price + " грн");
                    writer.WriteLine("Студент: " + studentName);
                    writer.WriteLine("Сторінок: " + pages);
                    writer.WriteLine("---------------------------");
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Помилка: " + error.Message);
            }
        }

        public override string CalculatePopularityRating()
        {
            int remembered = random.Next(1, 100);
            int understood = random.Next(0, remembered + 1);
            double efficiency = (double)understood / remembered * 100;

            Console.WriteLine("\nЗапам'ятав: " + remembered + "%");
            Console.WriteLine("Усвідомив: " + understood + "%");
            Console.WriteLine("Ефективність: " + efficiency.ToString("F2") + "%");
            return efficiency > 50 ? "Ефективний" : "Неефективний";
        }
    }

    // ==== Стандартні інтерфейси для четвертої версії ====
    class BookWithInterfaces : IComparable<BookWithInterfaces>
    {
        private string title;
        private double price;
        private int pages;

        public BookWithInterfaces(string title, double price, int pages)
        {
            this.title = title;
            this.price = price;
            this.pages = pages;
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        public int Pages
        {
            get { return pages; }
            set { pages = value; }
        }

        // IComparable - порівняння за ціною
        public int CompareTo(BookWithInterfaces other)
        {
            if (other == null) return 1;
            return this.price.CompareTo(other.price);
        }

        public void OutputToConsole()
        {
            Console.WriteLine($"Назва: {title}, Ціна: {price} грн, Сторінок: {pages}");
        }

        public void WriteToFile(string fileName)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName, true))
                {
                    writer.WriteLine($"Назва: {title}, Ціна: {price} грн, Сторінок: {pages}");
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Помилка: " + error.Message);
            }
        }
    }

    // IComparer для порівняння за ціною
    class PriceComparer : IComparer<BookWithInterfaces>
    {
        public int Compare(BookWithInterfaces x, BookWithInterfaces y)
        {
            if (x == null || y == null) return 0;
            return x.Price.CompareTo(y.Price);
        }
    }

    // IComparer для порівняння за сторінками
    class PagesComparer : IComparer<BookWithInterfaces>
    {
        public int Compare(BookWithInterfaces x, BookWithInterfaces y)
        {
            if (x == null || y == null) return 0;
            return x.Pages.CompareTo(y.Pages);
        }
    }

    // IEnumerable для виведення списків, впорядкованих за ціною та сторінками
    class BookCollection : IEnumerable<BookWithInterfaces>
    {
        private BookWithInterfaces[] books;
        private string sortType;

        public BookCollection(BookWithInterfaces[] books, string sortType)
        {
            this.books = books;
            this.sortType = sortType;
        }

        public IEnumerator<BookWithInterfaces> GetEnumerator()
        {
            BookWithInterfaces[] sortedBooks = (BookWithInterfaces[])books.Clone();

            if (sortType == "price")
            {
                Array.Sort(sortedBooks, new PriceComparer());
            }
            else if (sortType == "pages")
            {
                Array.Sort(sortedBooks, new PagesComparer());
            }

            foreach (BookWithInterfaces book in sortedBooks)
            {
                yield return book;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    // ==== ГОЛОВНА ПРОГРАМА ====
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== ЛАБОРАТОРНА РОБОТА №4 ===");
            Console.WriteLine("Варіант 14\n");

            // Очищаємо файли перед записом
            string[] files = { "book_data.txt", "textbook_data.txt", "synopsis_data.txt",
                              "interface_data.txt", "abstract_data.txt",
                              "sorted_by_price.txt", "sorted_by_pages.txt" };
            foreach (string file in files)
            {
                if (File.Exists(file)) File.Delete(file);
            }

            // ========== ЗАВДАННЯ 1-6: Базові класи ==========
            Console.WriteLine("\n========== ЗАВДАННЯ 1-6: Базові класи ==========");

            Book book1 = new Book("Програмування C#", 450);
            book1.OutputToConsole();
            book1.WriteToFile("book_data.txt");
            book1.CalculatePopularityRating();

            Textbook textbook = new Textbook("C# Programming", 550, "John Doe", 500, "English");
            textbook.OutputToConsole();
            textbook.WriteToFile("textbook_data.txt");
            textbook.CalculatePopularityRating();
            textbook.CheckFreeAccess();

            Synopsis synopsis = new Synopsis("Конспект з математики", 50, "Іван Петренко", 30, true);
            synopsis.OutputToConsole();
            synopsis.WriteToFile("synopsis_data.txt");
            synopsis.CalculatePopularityRating();

            // ========== ЗАВДАННЯ 7: Версія з інтерфейсом ==========
            Console.WriteLine("\n\n========== ЗАВДАННЯ 7: Версія з інтерфейсом ==========");

            IBook bookInterface = new BookInterface("Книга через інтерфейс", 200);
            IBook textbookInterface = new TextbookInterface("Підручник через інтерфейс", 400, "Автор", 300);
            IBook synopsisInterface = new SynopsisInterface("Конспект через інтерфейс", 30, "Студент", 20);

            bookInterface.OutputToConsole();
            textbookInterface.OutputToConsole();
            synopsisInterface.OutputToConsole();

            bookInterface.WriteToFile("interface_data.txt");
            textbookInterface.WriteToFile("interface_data.txt");
            synopsisInterface.WriteToFile("interface_data.txt");

            // ========== ЗАВДАННЯ 8: Версія з абстрактним класом ==========
            Console.WriteLine("\n\n========== ЗАВДАННЯ 8: Версія з абстрактним класом ==========");

            AbstractBook abstractTextbook = new AbstractTextbook("Абстрактний підручник", 600, "Автор", 550);
            AbstractBook abstractSynopsis = new AbstractSynopsis("Абстрактний конспект", 40, "Студент", 25);

            abstractTextbook.OutputToConsole();
            abstractSynopsis.OutputToConsole();

            abstractTextbook.WriteToFile("abstract_data.txt");
            abstractSynopsis.WriteToFile("abstract_data.txt");

            // Пояснення відмінностей
            Console.WriteLine("\n--- Відмінності інтерфейсу від абстрактного класу ---");
            Console.WriteLine("1. Абстрактний клас може мати реалізовані методи, інтерфейс - ні");
            Console.WriteLine("2. Клас успадковує 1 абстрактний клас, але багато інтерфейсів");
            Console.WriteLine("3. Абстрактний клас може мати поля, інтерфейс - ні");

            // ========== ЗАВДАННЯ 9: Стандартні інтерфейси ==========
            Console.WriteLine("\n\n========== ЗАВДАННЯ 9: Стандартні інтерфейси ==========");

            // Створюємо масив книг
            BookWithInterfaces[] books = new BookWithInterfaces[]
            {
                new BookWithInterfaces("C# Programming", 450, 500),
                new BookWithInterfaces("Java Basics", 350, 400),
                new BookWithInterfaces("Python Guide", 550, 600),
                new BookWithInterfaces("JavaScript Essentials", 250, 300),
                new BookWithInterfaces("C++ Advanced", 650, 700)
            };

            // Виводимо початковий список
            Console.WriteLine("\nПочатковий список:");
            foreach (var b in books) b.OutputToConsole();

            // Демонстрація IComparable - записуємо у файл
            BookWithInterfaces[] sortedByPrice = (BookWithInterfaces[])books.Clone();
            Array.Sort(sortedByPrice);
            foreach (var b in sortedByPrice)
            {
                b.WriteToFile("sorted_by_price.txt");
            }

            // Демонстрація IComparer - записуємо у файл
            BookWithInterfaces[] sortedByPages = (BookWithInterfaces[])books.Clone();
            Array.Sort(sortedByPages, new PagesComparer());
            foreach (var b in sortedByPages)
            {
                b.WriteToFile("sorted_by_pages.txt");
            }

            // IEnumerable - виведення списку, впорядкованого за ціною
            Console.WriteLine("\nIEnumerable - список, впорядкований за ціною:");
            BookCollection collectionByPrice = new BookCollection(books, "price");
            foreach (var b in collectionByPrice)
            {
                b.OutputToConsole();
            }

            // IEnumerable - виведення списку, впорядкованого за сторінками
            Console.WriteLine("\nIEnumerable - список, впорядкований за сторінками:");
            BookCollection collectionByPages = new BookCollection(books, "pages");
            foreach (var b in collectionByPages)
            {
                b.OutputToConsole();
            }

            Console.WriteLine("\n\n=== Роботу завершено ===");
        }
    }
}