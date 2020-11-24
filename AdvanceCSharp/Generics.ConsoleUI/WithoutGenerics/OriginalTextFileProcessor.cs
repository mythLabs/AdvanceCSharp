using Generics.ConsoleUI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace Generics.ConsoleUI.WithoutGenerics
{
    public class OriginalTextFileProcessor
    {
        public static List<Person> LoadPeople(string filePath)
        {
            List<Person> output = new List<Person>();
            Person p;
            var lines = File.ReadAllLines(filePath).ToList();

            lines.RemoveAt(0);

            foreach (var line in lines)
            {
                var vals = line.Split(',');
                p = new Person();

                p.FirstName = vals[0];
                p.LastName = vals[1];
                p.IsAlive = bool.Parse(vals[2]);
                output.Add(p);
            }

            return output;
        }

        public static void SavePerson(List<Person> people, string filePath)
        {
            List<string> lines = new List<string>();

            lines.Add("FirstName,LastName,IsAlive");

            foreach (var p in people)
            {
                lines.Add($"{ p.FirstName },{ p.LastName },{ p.IsAlive }");
            }

            File.WriteAllLines(filePath, lines);
        }

        public static List<LogEntry> LoadLogs(string filePath)
        {
            List<LogEntry> output = new List<LogEntry>();
            LogEntry p;
            var lines = File.ReadAllLines(filePath).ToList();

            lines.RemoveAt(0);

            foreach (var line in lines)
            {
                var vals = line.Split(',');
                p = new LogEntry();

                p.Message = vals[0];
                p.ErrorCode = int.Parse(vals[1]);
                p.TimeOfEvent = DateTime.Parse(vals[2]);
                output.Add(p);
            }

            return output;
        }

        public static void SaveLog(List<LogEntry> logs, string filePath)
        {
            List<string> lines = new List<string>();

            lines.Add("Message,ErrorCode,TimeOfEvent");

            foreach (var p in logs)
            {
                lines.Add($"{ p.Message },{ p.ErrorCode },{ p.TimeOfEvent }");
            }

            File.WriteAllLines(filePath, lines);
        }
    }
}
