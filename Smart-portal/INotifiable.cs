namespace SmartPortal.Core
{
    // Інтерфейс для об'єктів, які можуть надсилати сповіщення
    public interface INotifiable
    {
        // Метод, який має реалізувати кожен клас, що підтримує сповіщення
        string GetNotificationMessage();
    }
}