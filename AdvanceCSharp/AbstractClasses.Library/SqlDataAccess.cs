using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractClasses.Library
{
    public class SqlDataAccess : DataAccess
    {
        public override string LoadConnectionString(string name)
        {
            var output = base.LoadConnectionString(name); 
            Console.WriteLine($"{ output } (From Microsoft SQL Server)");
            return "testConnectionString";
        }

        public override void LoadData(string sql)
        {
            Console.WriteLine("Loading Microsoft SQL Data");
        }

        public override void SaveData(string sql)
        {
            Console.WriteLine("Saving Data To Microsoft SQL Server");
        }
    }
}
