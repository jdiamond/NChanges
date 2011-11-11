using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using ExcelLibrary.SpreadSheet;
using Mono.Options;
using NChanges.Core;

namespace NChanges.Tool
{
    public class ExcelCommand
    {
        private const int NAMESPACE = 0;
        private const int CLASS_NAME = 1;
        private const int METHOD_NAME = 2;
        private const int PARAMETERS = 3;
        private const int RETURN_TYPE = 4;
        private const int CHANGE = 5;
        private const int VERSION = 6;

        private readonly OptionSet _optionSet;
        private string _output = "%name%-%version%-report.xls";

        public ExcelCommand()
        {
            _optionSet = new OptionSet();
        }

        public void Run(IEnumerable<string> args)
        {
            AssemblyInfo report = LoadReport(args);

            var fileName = PathHelper.FormatPath(_output, report);
            var worksheet = new Worksheet("API Changes");

            AddEmptyCellsToFixKnownExcelBug(worksheet);
            AddHeaders(worksheet);
            SetColumnSize(worksheet);
            AddData(report, worksheet);
            CreateFile(worksheet, fileName);
        }

        private void AddEmptyCellsToFixKnownExcelBug(Worksheet worksheet)
        {
            for (var k = 0; k < 200; k++)
                worksheet.Cells[k, 0] = new Cell(null);
        }

        private AssemblyInfo LoadReport(IEnumerable<string> args)
        {
            var extras = _optionSet.Parse(args);
            var report = new AssemblyInfo();

            report.ReadXml(new XmlTextReader(extras[0]));

            return report;
        }

        private static void AddHeaders(Worksheet worksheet)
        {
            worksheet.Cells[0, NAMESPACE] = new Cell("Namespace");
            worksheet.Cells[0, CLASS_NAME] = new Cell("Class");
            worksheet.Cells[0, METHOD_NAME] = new Cell("Method");
            worksheet.Cells[0, PARAMETERS] = new Cell("Parameters");
            worksheet.Cells[0, RETURN_TYPE] = new Cell("Return Type");
            worksheet.Cells[0, CHANGE] = new Cell("Change");
            worksheet.Cells[0, VERSION] = new Cell("Version");
        }

        private static void SetColumnSize(Worksheet worksheet)
        {
            worksheet.Cells.ColumnWidth[0, NAMESPACE] = 10000;
            worksheet.Cells.ColumnWidth[0, CLASS_NAME] = 5000;
            worksheet.Cells.ColumnWidth[0, METHOD_NAME] = 5000;
            worksheet.Cells.ColumnWidth[0, PARAMETERS] = 10000;
            worksheet.Cells.ColumnWidth[0, RETURN_TYPE] = 5000;
            worksheet.Cells.ColumnWidth[0, CHANGE] = 5000;
            worksheet.Cells.ColumnWidth[0, VERSION] = 5000;
        }

        private void AddData(AssemblyInfo report, Worksheet worksheet)
        {
            var rowIndex = 2;

            foreach (var typeInfo in report.Types)
            {
                foreach (var methodInfo in typeInfo.Members)
                {
                    foreach (var change in methodInfo.Changes)
                    {
                        var typeName = typeInfo.Name;
                        var namesp = typeInfo.Namespace;
                        var methodName = methodInfo.Name;
                        var kind = change.Kind;
                        var version = change.Version;
                        var parameters = string.Join(", ", methodInfo.Parameters.Select(mi => mi.Type + " " + mi.Name).ToArray());
                        
                        worksheet.Cells[rowIndex, METHOD_NAME] = new Cell(methodName);
                        worksheet.Cells[rowIndex, NAMESPACE] = new Cell(namesp);
                        worksheet.Cells[rowIndex, CLASS_NAME] = new Cell(typeName);
                        worksheet.Cells[rowIndex, VERSION] = new Cell(version);
                        worksheet.Cells[rowIndex, CHANGE] = new Cell(kind.ToString());
                        worksheet.Cells[rowIndex, PARAMETERS] = new Cell(parameters);

                        rowIndex++;
                    }
                }
            }
        }

        private static void CreateFile(Worksheet worksheet, string fileName)
        {
            var workbook = new Workbook
                           {
                               Worksheets = { worksheet }
                           };

            workbook.Save(fileName);
        }

        public void ShowHelp()
        {
            _optionSet.WriteOptionDescriptions(Console.Error);
        }
    }
}