using System;
using System.IO;
using System.Text;
using System.Text.Json;

namespace SmartPortal.Core
{
    public static class Messages
    {
        private static JsonDocument doc;
        private static readonly string FileName = "messages.json";

        // Статичний конструктор — завантажує JSON один раз
        static Messages()
        {
            LoadMessages();
        }

        private static void LoadMessages()
        {
            try
            {
                if (File.Exists(FileName))
                {
                    string json = File.ReadAllText(FileName, Encoding.UTF8);
                    doc = JsonDocument.Parse(json);
                }
                else
                {
                    // Файлу немає — використовуємо значення за замовчуванням
                    doc = null;
                }
            }
            catch
            {
                doc = null;
            }
        }

        // Метод отримання рядка за ключем
        public static string Get(string key)
        {
            if (doc == null)
                return $"[{key}]";  // якщо JSON не завантажено

            try
            {
                return doc.RootElement.GetProperty(key).GetString() ?? $"[{key}]";
            }
            catch
            {
                return $"[{key}]";
            }
        }

        // Властивості для зворотної сумісності
        public static string WelcomeMessage => Get("WelcomeMessage");
        public static string GoodMorning => Get("GoodMorning");
        public static string Goodbye => Get("Goodbye");
        public static string CitizenNotFound => Get("CitizenNotFound");
        public static string AppealNotFound => Get("AppealNotFound");
        public static string NotLoggedIn => Get("NotLoggedIn");
        public static string EmptyContent => Get("EmptyContent");
        public static string UnknownCommand => Get("UnknownCommand");
        public static string AppealCreated => Get("AppealCreated");
        public static string NoAppeals => Get("NoAppeals");
        public static string WaitResponse => Get("WaitResponse");
        public static string OverdueWarning => Get("OverdueWarning");
        public static string TypeUrgent => Get("TypeUrgent");
        public static string ResolvedInfo => Get("ResolvedInfo");
        public static string ActiveInfo => Get("ActiveInfo");
        public static string NoExecutor => Get("NoExecutor");
        public static string ExecutorAssigned => Get("ExecutorAssigned");
    }
}