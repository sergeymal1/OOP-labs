using System;

namespace Lab6_SmartFridge
{
    // ==== Клас користувацького винятку ====
    public class FridgeException : ApplicationException
    {
        private string messageDetails;

        public FridgeException()
        {
            messageDetails = "";
        }

        public FridgeException(string message) : base(message)
        {
            messageDetails = message;
        }

        public FridgeException(string message, Exception innerException)
            : base(message, innerException)
        {
            messageDetails = message;
        }

        public override string Message
        {
            get
            {
                return string.Format("Помилка холодильника: {0}", messageDetails);
            }
        }

        public DateTime ErrorTime
        {
            get { return DateTime.Now; }
        }
    }
}