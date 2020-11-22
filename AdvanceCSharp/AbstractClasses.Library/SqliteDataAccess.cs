using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractClasses.Library
{
    public class SqliteDataAccess : DataAccess
    {
        public override void LoadData(string sql)
        {
            Console.WriteLine("Loading Sqlite Data");
        }

        public override void SaveData(string sql)
        {
            Console.WriteLine("Saving Data To Sqlite");
        }
    }
}
