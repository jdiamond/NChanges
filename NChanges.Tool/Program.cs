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
            else if (command == "excel")
            {
                new ExcelCommand().Run(args);
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

            Console.Error.WriteLine();
            Console.Error.WriteLine("Usage 1: {0} snapshot [assembly name] [option]", exeName);
            Console.Error.WriteLine("Create a snapshot file for each assembly that will be compared.");
            Console.Error.WriteLine("Snapshot will generate an xml file called: [assembly name]-[version number].xml");

            Console.Error.WriteLine();
            Console.Error.WriteLine("Usage 2: {0} report [snapshot filename 1] [snapshot filename 2] [option]", exeName);
            Console.Error.WriteLine("Report may compare more than 2 snapshot files the same time.");
            Console.Error.WriteLine("Report will take the differences of the snapshots and put them into a xml file called: " +
                                    "[assembly name]-[highest version number]-report.xml");

            Console.Error.WriteLine();
            Console.Error.WriteLine("Usage 3: {0} export [report filename]", exeName);
            Console.Error.WriteLine("Export will export the report to an excel file.");
            Console.Error.WriteLine("The exported file name is called: " +
                                    "[report name].xls");

            Console.Error.WriteLine();
            Console.Error.WriteLine("Command can be \"snapshot\" or \"report\" or \"export\".");

            Console.Error.WriteLine();
            Console.Error.WriteLine("Options for the \"snapshot\" command:");
            Console.Error.WriteLine();
            new SnapshotCommand().ShowHelp();

            Console.Error.WriteLine();
            Console.Error.WriteLine("Options for the \"report\" command:");
            Console.Error.WriteLine();
            new ReportCommand().ShowHelp();

            Console.Error.WriteLine();
            Console.Error.WriteLine("Options for the \"excel\" command:");
            Console.Error.WriteLine();
            new ExcelCommand().ShowHelp();
        }
    }
}