using AbstractClasses.Library;
using System;
using System.Collections.Generic;

namespace AbstractClasses.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            DataAccess da = new SqlDataAccess();

            List<DataAccess> databases = new List<DataAccess>
            {
                new SqlDataAccess(),
                new SqliteDataAccess()
            };

            foreach (DataAccess db in databases)
            {
                db.LoadConnectionString("demo");
                db.LoadData("select * from users");
                db.SaveData("insert into users");
                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }
}
