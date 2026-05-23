using System;

namespace SmartPortal.Core.Exceptions
{
    // Виняток — перевищено ліміт звернень
    public class MaxAppealsExceededException : Exception
    {
        public string CitizenId { get; private set; }
        public int MaxAppeals { get; private set; }

        public MaxAppealsExceededException(string citizenId, int maxAppeals)
            : base($"Громадянин '{citizenId}' перевищив ліміт звернень ({maxAppeals})")
        {
            CitizenId = citizenId;
            MaxAppeals = maxAppeals;
        }
    }
}