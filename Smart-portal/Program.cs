using System;
using System.Text;
using SmartPortal.Core;

namespace SmartPortal.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.GetEncoding(1251);
            Console.InputEncoding = Encoding.GetEncoding(1251);

            var citizen = new Citizen("C001", "Іван", "Петренко");
            citizen.Address = "вул. Хрещатик, 1";
            citizen.Phone = "+380501234567";
            citizen.Email = "ivan@email.com";

            var appeal = new Appeal("A001", citizen, "Не працює ліфт у під'їзді");

            Console.WriteLine(citizen);
            Console.WriteLine(appeal);
            Console.WriteLine($"Зміст: {appeal.Content}");

            Console.ReadLine();
        }
    }
}