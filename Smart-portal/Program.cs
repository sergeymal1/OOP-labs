using SmartPortal.Core;
using System;
using System.Text;

namespace SmartPortalApp
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.OutputEncoding = Encoding.GetEncoding(1251);
            Console.InputEncoding = Encoding.GetEncoding(1251);

            var portal = new SmartPortal.Core.SmartPortal("Київ");
            Console.WriteLine($"Smart-портал міста {portal.CityName} запущено\n");

            // Створюємо громадян ззовні
            var citizen1 = new Citizen("C001", "Іван", "Петренко");
            citizen1.Address = "вул. Хрещатик, 1";
            citizen1.Phone = "+380501234567";

            var citizen2 = new Citizen("C002", "Марія", "Коваленко");
            citizen2.Address = "вул. Грушевського, 5";
            citizen2.Phone = "+380671112233";

            portal.RegisterCitizen(citizen1);
            portal.RegisterCitizen(citizen2);

            Console.WriteLine();

            // Звернення створюються всередині порталу
            portal.CreateAppeal("A001", citizen1, "Не працює ліфт у під'їзді");
            portal.CreateAppeal("A002", citizen2, "Відсутнє освітлення на дитячому майданчику");
            portal.CreateAppeal("A003", citizen1, "Яма на дорозі біля будинку");

            Console.WriteLine();

            portal.UpdateAppealStatus("A001", AppealStatus.InProgress, "ЖЕК №5");

            Console.WriteLine();

            // Пошук звернень через посилання на автора
            Console.WriteLine($"Звернення громадянина {citizen1.LastName}:");
            var citizenAppeals = portal.GetAppealsByCitizen(citizen1);
            foreach (var appeal in citizenAppeals)
            {
                Console.WriteLine($"  {appeal}");
            }

            Console.ReadLine();
        }
    }
}