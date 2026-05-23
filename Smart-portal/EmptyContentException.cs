using System;

namespace SmartPortal.Core.Exceptions
{
    // Виняток — порожній текст звернення
    public class EmptyContentException : Exception
    {
        public EmptyContentException()
            : base("Текст звернення не може бути порожнім")
        {
        }
    }
}