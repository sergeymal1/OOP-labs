using System;
using System.IO;
using System.Text;

namespace Lab5_Var14
{
    // ==== Базовий клас КНИГА ====
    class Book
    {
        // Статичний random для всіх об'єктів
        protected static Random random = new Random();

        // Закриті поля
        private string title;
        private double price;

        // 1. Конструктор за замовчуванням
        public Book()
        {
            title = "Невідома книга";
            price = 0;
        }

        // 2. Конструктор з параметрами
        public Book(string title, double price)
        {
            this.title = title;
            this.price = price;
        }

        // 3. Конструктор копіювання
        public Book(Book other)
        {
            this.title = other.title;
            this.price = other.price;
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
                using (StreamWriter writer = new StreamWriter(fileName, true, Encoding.GetEncoding(1251)))
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

        // Віртуальний метод для доступності матеріалу
        public virtual string GetMaterialAvailability()
        {
            return "Загальна доступність";
        }

        // Віртуальний метод для статусу
        public virtual string GetStatus()
        {
            return "Загальний статус";
        }

        // Віртуальний метод для рейтингу студента
        public virtual double CalculateStudentRating()
        {
            return 0;
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
        private double studentRating;

        // 1. Конструктор за замовчуванням
        public Textbook() : base()
        {
            author = "";
            pages = 0;
            language = "";
            popularityRating = 0;
            studentRating = 0;
        }

        // 2. Конструктор з параметрами
        public Textbook(string title, double price, string author, int pages, string language)
            : base(title, price)
        {
            this.author = author;
            this.pages = pages;
            this.language = language;
            this.popularityRating = 0;
            this.studentRating = 0;
            CalculateStudentRating();
            CalculatePopularityRating();
        }

        // 3. Конструктор копіювання
        public Textbook(Textbook other) : base(other)
        {
            this.author = other.author;
            this.pages = other.pages;
            this.language = other.language;
            this.popularityRating = other.popularityRating;
            this.studentRating = other.studentRating;
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

        public double StudentRating
        {
            get { return studentRating; }
            set { studentRating = value; }
        }

        // Виведення на консоль
        public override void OutputToConsole()
        {
            base.OutputToConsole();
            Console.WriteLine("=== ПІДРУЧНИК ===");
            Console.WriteLine("Автор: " + author);
            Console.WriteLine("Сторінок: " + pages);
            Console.WriteLine("Мова: " + language);
            Console.WriteLine("Рейтинг популярності: " + popularityRating);
            Console.WriteLine("Рейтинг студента: " + studentRating.ToString("F2"));
        }

        // Запис у файл
        public override void WriteToFile(string fileName)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName, true, Encoding.GetEncoding(1251)))
                {
                    writer.WriteLine("=== ПІДРУЧНИК ===");
                    writer.WriteLine("Назва: " + Title);
                    writer.WriteLine("Вартість: " + Price + " грн");
                    writer.WriteLine("Автор: " + author);
                    writer.WriteLine("Сторінок: " + pages);
                    writer.WriteLine("Мова: " + language);
                    writer.WriteLine("Рейтинг популярності: " + popularityRating);
                    writer.WriteLine("Рейтинг студента: " + studentRating.ToString("F2"));
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

        // Доступність матеріалу для підручника
        public override string GetMaterialAvailability()
        {
            string[] levels = { "легка для розуміння", "важка для розуміння", "проста", "середня" };
            int index = random.Next(0, levels.Length);
            return levels[index];
        }

        // Статус підручника
        public override string GetStatus()
        {
            string[] statuses = { "рекомендовано", "затверджено" };
            int index = random.Next(0, statuses.Length);
            return statuses[index];
        }

        // Рейтинг з погляду студента для підручника
        public override double CalculateStudentRating()
        {
            int actuality = random.Next(50, 101);
            int availability = random.Next(40, 101);
            int exercises = random.Next(30, 101);
            int logic = random.Next(40, 101);
            int reviews = random.Next(30, 101);

            studentRating = (actuality * 0.2) + (availability * 0.25) +
                           (exercises * 0.2) + (logic * 0.15) + (reviews * 0.2);

            Console.WriteLine("\n=== Рейтинг підручника (оцінка студента) ===");
            Console.WriteLine("Актуальність: " + actuality);
            Console.WriteLine("Доступність: " + availability);
            Console.WriteLine("Наявність вправ: " + exercises);
            Console.WriteLine("Розвиток логіки: " + logic);
            Console.WriteLine("Відгуки: " + reviews);
            Console.WriteLine("Загальний рейтинг: " + studentRating.ToString("F2"));

            return studentRating;
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

        // Перевантаження бінарних операторів
        public static Textbook operator +(Textbook t, double amount)
        {
            t.Price += amount;
            return t;
        }

        public static Textbook operator -(Textbook t, double amount)
        {
            t.Price -= amount;
            if (t.Price < 0) t.Price = 0;
            return t;
        }

        public static bool operator ==(Textbook a, Textbook b)
        {
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;
            return Math.Abs(a.studentRating - b.studentRating) <= 5;
        }

        public static bool operator !=(Textbook a, Textbook b)
        {
            return !(a == b);
        }

        public static bool operator >(Textbook a, Textbook b)
        {
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;
            return a.studentRating > b.studentRating;
        }

        public static bool operator <(Textbook a, Textbook b)
        {
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;
            return a.studentRating < b.studentRating;
        }

        // Перевантаження унарних операторів
        public static Textbook operator ++(Textbook t)
        {
            t.studentRating += 5;
            if (t.studentRating > 100) t.studentRating = 100;
            return t;
        }

        public static Textbook operator --(Textbook t)
        {
            t.studentRating -= 5;
            if (t.studentRating < 0) t.studentRating = 0;
            return t;
        }

        public static double operator -(Textbook t)
        {
            return -t.studentRating;
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
        private double studentRating;

        // 1. Конструктор за замовчуванням
        public Synopsis() : base()
        {
            studentName = "";
            pages = 0;
            efficiency = 0;
            freeAccess = false;
            studentRating = 0;
        }

        // 2. Конструктор з параметрами
        public Synopsis(string title, double price, string studentName, int pages, bool freeAccess)
            : base(title, price)
        {
            this.studentName = studentName;
            this.pages = pages;
            this.freeAccess = freeAccess;
            this.efficiency = 0;
            this.studentRating = 0;
            CalculateStudentRating();
            CalculatePopularityRating();
        }

        // 3. Конструктор копіювання
        public Synopsis(Synopsis other) : base(other)
        {
            this.studentName = other.studentName;
            this.pages = other.pages;
            this.freeAccess = other.freeAccess;
            this.efficiency = other.efficiency;
            this.studentRating = other.studentRating;
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

        public double StudentRating
        {
            get { return studentRating; }
            set { studentRating = value; }
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
            Console.WriteLine("Рейтинг студента: " + studentRating.ToString("F2"));
        }

        // Запис у файл
        public override void WriteToFile(string fileName)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName, true, Encoding.GetEncoding(1251)))
                {
                    writer.WriteLine("=== КОНСПЕКТ ===");
                    writer.WriteLine("Назва: " + Title);
                    writer.WriteLine("Вартість: " + Price + " грн");
                    writer.WriteLine("Студент: " + studentName);
                    writer.WriteLine("Сторінок: " + pages);
                    writer.WriteLine("Ефективність: " + efficiency.ToString("F2") + "%");
                    writer.WriteLine("Вільний доступ: " + (freeAccess ? "Так" : "Ні"));
                    writer.WriteLine("Рейтинг студента: " + studentRating.ToString("F2"));
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
            int rememberedInfo = random.Next(1, 100);
            int understoodInfo = random.Next(0, rememberedInfo + 1);

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

        // Доступність матеріалу для конспекту
        public override string GetMaterialAvailability()
        {
            string[] levels = { "чіткий", "нечіткий", "зрозумілий", "заплутаний" };
            int index = random.Next(0, levels.Length);
            return levels[index];
        }

        // Статус конспекту
        public override string GetStatus()
        {
            string[] statuses = { "відсутній", "чорновий", "остаточний" };
            int index = random.Next(0, statuses.Length);
            return statuses[index];
        }

        // Рейтинг з погляду студента для конспекту
        public override double CalculateStudentRating()
        {
            int actuality = random.Next(40, 101);
            int availability = random.Next(50, 101);
            int exercises = random.Next(20, 101);
            int logic = random.Next(30, 101);
            int reviews = random.Next(25, 101);

            studentRating = (actuality * 0.2) + (availability * 0.25) +
                           (exercises * 0.2) + (logic * 0.15) + (reviews * 0.2);

            Console.WriteLine("\n=== Рейтинг конспекту (оцінка студента) ===");
            Console.WriteLine("Актуальність: " + actuality);
            Console.WriteLine("Доступність: " + availability);
            Console.WriteLine("Наявність вправ: " + exercises);
            Console.WriteLine("Розвиток логіки: " + logic);
            Console.WriteLine("Відгуки: " + reviews);
            Console.WriteLine("Загальний рейтинг: " + studentRating.ToString("F2"));

            return studentRating;
        }

        // Перевантаження бінарних операторів
        public static Synopsis operator +(Synopsis s, double amount)
        {
            s.Price += amount;
            return s;
        }

        public static Synopsis operator -(Synopsis s, double amount)
        {
            s.Price -= amount;
            if (s.Price < 0) s.Price = 0;
            return s;
        }

        public static bool operator ==(Synopsis a, Synopsis b)
        {
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;
            return Math.Abs(a.studentRating - b.studentRating) <= 5;
        }

        public static bool operator !=(Synopsis a, Synopsis b)
        {
            return !(a == b);
        }

        public static bool operator >(Synopsis a, Synopsis b)
        {
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;
            return a.studentRating > b.studentRating;
        }

        public static bool operator <(Synopsis a, Synopsis b)
        {
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;
            return a.studentRating < b.studentRating;
        }

        // Перевантаження унарних операторів
        public static Synopsis operator ++(Synopsis s)
        {
            s.studentRating += 5;
            if (s.studentRating > 100) s.studentRating = 100;
            return s;
        }

        public static Synopsis operator --(Synopsis s)
        {
            s.studentRating -= 5;
            if (s.studentRating < 0) s.studentRating = 0;
            return s;
        }

        public static double operator -(Synopsis s)
        {
            return -s.studentRating;
        }
    }

    // ==== Похідний клас ЕЛЕКТРОННА_КНИГА ====
    class EBook : Book
    {
        // Специфічні поля
        private string format;
        private double fileSizeMB;
        private int downloadsCount;
        private string publicationStatus;
        private double studentRating;

        // 1. Конструктор за замовчуванням
        public EBook() : base()
        {
            format = "";
            fileSizeMB = 0;
            downloadsCount = 0;
            publicationStatus = "";
            studentRating = 0;
        }

        // 2. Конструктор з параметрами
        public EBook(string title, double price, string format, double fileSizeMB,
                     int downloadsCount, string publicationStatus)
            : base(title, price)
        {
            this.format = format;
            this.fileSizeMB = fileSizeMB;
            this.downloadsCount = downloadsCount;
            this.publicationStatus = publicationStatus;
            this.studentRating = 0;
            CalculateStudentRating();
            CalculatePopularityRating();
        }

        // 3. Конструктор копіювання
        public EBook(EBook other) : base(other)
        {
            this.format = other.format;
            this.fileSizeMB = other.fileSizeMB;
            this.downloadsCount = other.downloadsCount;
            this.publicationStatus = other.publicationStatus;
            this.studentRating = other.studentRating;
        }

        // Властивості
        public string Format
        {
            get { return format; }
            set { format = value; }
        }

        public double FileSizeMB
        {
            get { return fileSizeMB; }
            set { fileSizeMB = value; }
        }

        public int DownloadsCount
        {
            get { return downloadsCount; }
            set { downloadsCount = value; }
        }

        public string PublicationStatus
        {
            get { return publicationStatus; }
            set { publicationStatus = value; }
        }

        public double StudentRating
        {
            get { return studentRating; }
            set { studentRating = value; }
        }

        // Виведення на консоль
        public override void OutputToConsole()
        {
            base.OutputToConsole();
            Console.WriteLine("=== ЕЛЕКТРОННА КНИГА ===");
            Console.WriteLine("Формат: " + format);
            Console.WriteLine("Розмір: " + fileSizeMB + " МБ");
            Console.WriteLine("Завантажень: " + downloadsCount);
            Console.WriteLine("Статус: " + publicationStatus);
            Console.WriteLine("Рейтинг студента: " + studentRating.ToString("F2"));
        }

        // Запис у файл
        public override void WriteToFile(string fileName)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName, true, Encoding.GetEncoding(1251)))
                {
                    writer.WriteLine("=== ЕЛЕКТРОННА КНИГА ===");
                    writer.WriteLine("Назва: " + Title);
                    writer.WriteLine("Вартість: " + Price + " грн");
                    writer.WriteLine("Формат: " + format);
                    writer.WriteLine("Розмір: " + fileSizeMB + " МБ");
                    writer.WriteLine("Завантажень: " + downloadsCount);
                    writer.WriteLine("Статус: " + publicationStatus);
                    writer.WriteLine("Рейтинг студента: " + studentRating.ToString("F2"));
                    writer.WriteLine("---------------------------");
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Помилка: " + error.Message);
            }
        }

        // Розрахунок рейтингу популярності
        public override string CalculatePopularityRating()
        {
            string status;
            if (downloadsCount > 1000)
                status = "БЕСТСЕЛЕР!";
            else if (downloadsCount > 500)
                status = "Популярна";
            else
                status = "Звичайна";

            Console.WriteLine("\n=== Рейтинг електронної книги ===");
            Console.WriteLine("Завантажень: " + downloadsCount);
            Console.WriteLine("Статус: " + status);

            return status;
        }

        // Доступність матеріалу для електронної книги
        public override string GetMaterialAvailability()
        {
            string[] levels = { "зручний формат", "незручний формат", "адаптивний", "важкий для читання" };
            int index = random.Next(0, levels.Length);
            return levels[index];
        }

        // Статус електронної книги
        public override string GetStatus()
        {
            string[] statuses = { "опубліковано", "завантажено", "в обробці", "знята з публікації" };
            int index = random.Next(0, statuses.Length);
            return statuses[index];
        }

        // Рейтинг з погляду студента для електронної книги
        public override double CalculateStudentRating()
        {
            int actuality = random.Next(60, 101);
            int availability = random.Next(50, 101);
            int exercises = random.Next(30, 101);
            int logic = random.Next(40, 101);
            int reviews = random.Next(35, 101);

            studentRating = (actuality * 0.2) + (availability * 0.25) +
                           (exercises * 0.2) + (logic * 0.15) + (reviews * 0.2);

            int downloadBonus = downloadsCount / 100;
            studentRating += downloadBonus;
            if (studentRating > 100) studentRating = 100;

            Console.WriteLine("\n=== Рейтинг електронної книги (оцінка студента) ===");
            Console.WriteLine("Актуальність: " + actuality);
            Console.WriteLine("Доступність: " + availability);
            Console.WriteLine("Наявність вправ: " + exercises);
            Console.WriteLine("Розвиток логіки: " + logic);
            Console.WriteLine("Відгуки: " + reviews);
            Console.WriteLine("Бонус за завантаження: " + downloadBonus);
            Console.WriteLine("Загальний рейтинг: " + studentRating.ToString("F2"));

            return studentRating;
        }

        // Перевантаження бінарних операторів
        public static EBook operator +(EBook e, double amount)
        {
            e.Price += amount;
            return e;
        }

        public static EBook operator -(EBook e, double amount)
        {
            e.Price -= amount;
            if (e.Price < 0) e.Price = 0;
            return e;
        }

        public static bool operator ==(EBook a, EBook b)
        {
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;
            return Math.Abs(a.studentRating - b.studentRating) <= 5;
        }

        public static bool operator !=(EBook a, EBook b)
        {
            return !(a == b);
        }

        public static bool operator >(EBook a, EBook b)
        {
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;
            return a.studentRating > b.studentRating;
        }

        public static bool operator <(EBook a, EBook b)
        {
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;
            return a.studentRating < b.studentRating;
        }

        // Перевантаження унарних операторів
        public static EBook operator ++(EBook e)
        {
            e.studentRating += 5 + (e.downloadsCount / 100);
            if (e.studentRating > 100) e.studentRating = 100;
            return e;
        }

        public static EBook operator --(EBook e)
        {
            e.studentRating -= 5;
            if (e.studentRating < 0) e.studentRating = 0;
            return e;
        }

        public static double operator -(EBook e)
        {
            return -e.studentRating;
        }
    }

    // ==== КЛАС-КОЛЕКЦІЯ З ІНДЕКСАТОРОМ ДЛЯ ЕЛЕКТРОННИХ КНИГ ====
    class EBookLibrary
    {
        private EBook[] books;

        // Конструктор
        public EBookLibrary(int size)
        {
            books = new EBook[size];
        }

        // Індексатор
        public EBook this[int index]
        {
            get
            {
                if (index >= 0 && index < books.Length)
                    return books[index];
                else
                    throw new IndexOutOfRangeException("Індекс поза межами масиву");
            }
            set
            {
                if (index >= 0 && index < books.Length)
                    books[index] = value;
                else
                    throw new IndexOutOfRangeException("Індекс поза межами масиву");
            }
        }

        // Отримання довжини масиву
        public int Length
        {
            get { return books.Length; }
        }
    }

    // ==== ГОЛОВНА ПРОГРАМА ====
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("=== ЛАБОРАТОРНА РОБОТА №5 ===");
            Console.WriteLine("Варіант 14\n");

            // Очищаємо файли
            string[] files = { "book_data.txt", "textbook_data.txt", "synopsis_data.txt",
                              "ebook_data.txt", "library_data.txt" };
            foreach (string file in files)
            {
                if (File.Exists(file)) File.Delete(file);
            }

            // Демонстрація роботи класів
            Console.WriteLine("\n--- ПІДРУЧНИК ---");
            Textbook textbook = new Textbook("C# Programming", 550, "John Doe", 500, "English");
            textbook.OutputToConsole();
            textbook.WriteToFile("textbook_data.txt");
            textbook.CalculatePopularityRating();
            textbook.CheckFreeAccess();
            textbook.CalculateStudentRating();

            Console.WriteLine("\nДоступність матеріалу: " + textbook.GetMaterialAvailability());
            Console.WriteLine("Статус: " + textbook.GetStatus());

            // Демонстрація операторів для підручника
            Console.WriteLine("\n--- Оператори для підручника ---");
            Console.WriteLine("Початкова ціна: " + textbook.Price);
            textbook = textbook + 50;
            Console.WriteLine("Після +50: " + textbook.Price);
            textbook = textbook - 30;
            Console.WriteLine("Після -30: " + textbook.Price);

            Textbook textbook2 = new Textbook("Java Basics", 450, "Jane Smith", 400, "English");
            textbook2.CalculateStudentRating();
            Console.WriteLine("\nРейтинг підручника 1: " + textbook.StudentRating);
            Console.WriteLine("Рейтинг підручника 2: " + textbook2.StudentRating);
            Console.WriteLine("textbook > textbook2: " + (textbook > textbook2));
            Console.WriteLine("textbook < textbook2: " + (textbook < textbook2));

            textbook++;
            Console.WriteLine("\nПісля ++ рейтинг: " + textbook.StudentRating);
            textbook--;
            Console.WriteLine("Після -- рейтинг: " + textbook.StudentRating);
            Console.WriteLine("Від'ємний рейтинг: " + (-textbook));

            // Демонстрація конспекту
            Console.WriteLine("\n\n--- КОНСПЕКТ ---");
            Synopsis synopsis = new Synopsis("Конспект з математики", 50, "Іван Петренко", 30, true);
            synopsis.OutputToConsole();
            synopsis.WriteToFile("synopsis_data.txt");
            synopsis.CalculatePopularityRating();
            synopsis.CalculateStudentRating();

            Console.WriteLine("\nДоступність матеріалу: " + synopsis.GetMaterialAvailability());
            Console.WriteLine("Статус: " + synopsis.GetStatus());

            // Демонстрація електронної книги
            Console.WriteLine("\n\n--- ЕЛЕКТРОННА КНИГА ---");
            EBook ebook = new EBook("C# Advanced", 300, "PDF", 5.2, 150, "опубліковано");
            ebook.OutputToConsole();
            ebook.WriteToFile("ebook_data.txt");
            ebook.CalculatePopularityRating();
            ebook.CalculateStudentRating();

            Console.WriteLine("\nДоступність матеріалу: " + ebook.GetMaterialAvailability());
            Console.WriteLine("Статус: " + ebook.GetStatus());

            // Демонстрація операторів для електронної книги
            Console.WriteLine("\n--- Оператори для електронної книги ---");
            Console.WriteLine("Початкова ціна: " + ebook.Price);
            ebook = ebook + 25;
            Console.WriteLine("Після +25: " + ebook.Price);
            ebook = ebook - 10;
            Console.WriteLine("Після -10: " + ebook.Price);

            EBook ebook2 = new EBook("Python Guide", 250, "EPUB", 3.8, 80, "завантажено");
            ebook2.CalculateStudentRating();
            Console.WriteLine("\nРейтинг книги 1: " + ebook.StudentRating);
            Console.WriteLine("Рейтинг книги 2: " + ebook2.StudentRating);
            Console.WriteLine("ebook > ebook2: " + (ebook > ebook2));
            Console.WriteLine("ebook < ebook2: " + (ebook < ebook2));

            ebook++;
            Console.WriteLine("\nПісля ++ рейтинг: " + ebook.StudentRating);
            ebook--;
            Console.WriteLine("Після -- рейтинг: " + ebook.StudentRating);
            Console.WriteLine("Від'ємний рейтинг: " + (-ebook));

            // Демонстрація індексатора
            Console.WriteLine("\n\n--- ІНДЕКСАТОР ДЛЯ ЕЛЕКТРОННИХ КНИГ ---");
            EBookLibrary library = new EBookLibrary(3);

            library[0] = new EBook("C# Programming", 350, "PDF", 4.5, 200, "опубліковано");
            library[1] = new EBook("Java Basics", 300, "EPUB", 3.2, 150, "завантажено");
            library[2] = new EBook("Python Guide", 280, "PDF", 2.8, 100, "опубліковано");

            for (int i = 0; i < library.Length; i++)
            {
                Console.WriteLine($"\nКнига {i + 1}:");
                library[i].OutputToConsole();
                library[i].WriteToFile("library_data.txt");
            }

            Console.WriteLine("\n\n=== Роботу завершено ===");
        }
    }
}