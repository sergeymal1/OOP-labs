namespace SmartPortal.Core
{
    // Файл із текстовими повідомленнями
    public static class Messages
    {
        // Привітання
        public const string WelcomeMessage = "Вітаємо на порталі міста";
        public const string GoodMorning = "Добрий день";
        public const string Goodbye = "До побачення";

        // Помилки
        public const string CitizenNotFound = "Громадянина з таким ID не знайдено";
        public const string AppealNotFound = "Звернення з таким номером не знайдено";
        public const string NotLoggedIn = "Спочатку увійдіть у систему";
        public const string EmptyContent = "Текст звернення не може бути порожнім";
        public const string UnknownCommand = "Невідома команда";

        // Інформація
        public const string AppealCreated = "Ваше звернення зареєстровано під номером";
        public const string NoAppeals = "У вас поки немає звернень";
        public const string WaitResponse = "Очікуйте на відповідь";

        // Статуси
        public const string StatusNew = "нове";
        public const string StatusInProgress = "в роботі";
        public const string StatusResolved = "вирішено";
        public const string StatusRejected = "відхилено";
    }
}