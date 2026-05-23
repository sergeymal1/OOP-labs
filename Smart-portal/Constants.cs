namespace SmartPortal.Core
{
    // Обмеження системи
    public static class Constants
    {
        // Максимальна довжина рядків
        public const int MaxNameLength = 50;
        public const int MaxAddressLength = 100;
        public const int MaxPhoneLength = 15;
        public const int MaxEmailLength = 50;
        public const int MaxContentLength = 500;

        // Назви файлів
        public const string CitizensFileName = "citizens.txt";
        public const string AppealsFileName = "appeals.txt";

        // Статуси за замовчуванням
        public const string DefaultExecutor = "не призначено";

        // Обмеження системи
        public const int MaxAppealsPerCitizen = 10;
    }
}