using Generics.ConsoleUI.Models;
using Generics.ConsoleUI.WithoutGenerics;
using System;
using System.Collections.Generic;

namespace Generics.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            DemonstrateTextFileStorage();
            Console.ReadLine();
        }

        private static void DemonstrateTextFileStorage()
        {
            List<Person> people = new List<Person>();
            List<LogEntry> logs = new List<LogEntry>();

            string peopleFile = @"C:\Temp\people.csv";
            string logFile = @"C:\Temp\logs.csv";

            PopulateLists(people, logs);

            OriginalTextFileProcessor.SavePerson(people, peopleFile);

            var newPeople = OriginalTextFileProcessor.LoadPeople(peopleFile);

            foreach (var p in newPeople)
            {
                Console.WriteLine($"{ p.FirstName } { p.LastName } ( IsAlive= { p.IsAlive })");
            }

            OriginalTextFileProcessor.SaveLog(logs, logFile);

            var newLogs = OriginalTextFileProcessor.LoadLogs(logFile);

            foreach (var p in newLogs)
            {
                Console.WriteLine($"Message = { p.Message }, ErrorCode = { p.ErrorCode } at { p.TimeOfEvent.ToShortTimeString() }");
            }

        }

        private static void PopulateLists(List<Person> people, List<LogEntry> logs)
        {
            people.Add(new Person { FirstName = "Sam", LastName = "Smith" });
            people.Add(new Person { FirstName = "Aurther", LastName = "Morgan", IsAlive = false });
            people.Add(new Person { FirstName = "Robin", LastName = "k" });

            logs.Add(new LogEntry { Message = "I am still alive", ErrorCode = 345345 });
            logs.Add(new LogEntry { Message = "Something is wrong", ErrorCode = 268905 });
            logs.Add(new LogEntry { Message = "Wow champ", ErrorCode = 245789 });
        }
    }
}
