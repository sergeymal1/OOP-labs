using System;

namespace SmartPortal.Core.Exceptions
{
    // Виняток — спроба зареєструвати громадянина з існуючим ID
    public class DuplicateCitizenException : Exception
    {
        public string CitizenId { get; private set; }

        public DuplicateCitizenException(string citizenId)
            : base($"Громадянин із ID '{citizenId}' вже зареєстрований")
        {
            CitizenId = citizenId;
        }
    }
}