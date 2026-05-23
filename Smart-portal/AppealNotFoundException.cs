using System;

namespace SmartPortal.Core.Exceptions
{
    // Звернення не знайдено
    public class AppealNotFoundException : Exception
    {
        public string AppealId { get; private set; }

        public AppealNotFoundException(string appealId)
            : base($"Звернення з номером '{appealId}' не знайдено")
        {
            AppealId = appealId;
        }
    }
}