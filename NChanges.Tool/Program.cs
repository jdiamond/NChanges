using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace NChanges.Tool
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                ShowHelp();
                Environment.Exit(-1);
            }

            var command = args.First();
            args = args.Skip(1).ToArray();

            if (command == "snapshot")
            {
                new SnapshotCommand().Run(args);
            }
            else if (command == "report")
            {
                new ReportCommand().Run(args);
            }
            else
            {
                Console.Error.WriteLine("Unknown command: {0}", command);
                Console.Error.WriteLine();
                ShowHelp();
                Environment.Exit(-1);
            }
        }

        private static void ShowHelp()
        {
            string exeName = Path.GetFileName(Assembly.GetEntryAssembly().Location);
            Console.Error.WriteLine("Usage: {0} command ...", exeName);
            Console.Error.WriteLine();
            Console.Error.WriteLine("Command can be \"snapshot\" or \"report\".");
            Console.Error.WriteLine();
            Console.Error.WriteLine("Options for the \"snapshot\" command:");
            Console.Error.WriteLine();
            new SnapshotCommand().ShowHelp();
            Console.Error.WriteLine();
            Console.Error.WriteLine("Options for the \"report\" command:");
            Console.Error.WriteLine();
            new ReportCommand().ShowHelp();
        }
    }
}