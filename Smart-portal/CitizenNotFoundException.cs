using System;

namespace SmartPortal.Core.Exceptions
{
    // Власний клас винятку — громадянин не знайдений
    public class CitizenNotFoundException : Exception
    {
        public string CitizenId { get; private set; }

        public CitizenNotFoundException(string citizenId)
            : base($"Громадянина з ID '{citizenId}' не знайдено в системі")
        {
            CitizenId = citizenId;
        }
    }
}