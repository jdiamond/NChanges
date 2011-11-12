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
        private const int VERSION = 0;
        private const int CHANGE = 1;
        private const int NAMESPACE = 2;
        private const int TYPE_NAME = 3;
        private const int METHOD_NAME = 4;
        private const int PARAMETERS = 5;
        private const int RETURN_TYPE = 6;

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
            worksheet.Cells[0, TYPE_NAME] = new Cell("Type");
            worksheet.Cells[0, METHOD_NAME] = new Cell("Method");
            worksheet.Cells[0, PARAMETERS] = new Cell("Parameters");
            worksheet.Cells[0, RETURN_TYPE] = new Cell("Return Type");
            worksheet.Cells[0, CHANGE] = new Cell("Change");
            worksheet.Cells[0, VERSION] = new Cell("Version");
        }

        private static void SetColumnSize(Worksheet worksheet)
        {
            worksheet.Cells.ColumnWidth[NAMESPACE] = 15000;
            worksheet.Cells.ColumnWidth[TYPE_NAME] = 12000;
            worksheet.Cells.ColumnWidth[METHOD_NAME] = 12000;
            worksheet.Cells.ColumnWidth[PARAMETERS] = 10000;
            worksheet.Cells.ColumnWidth[RETURN_TYPE] = 5000;
            worksheet.Cells.ColumnWidth[CHANGE] = 3000;
            worksheet.Cells.ColumnWidth[VERSION] = 3000;
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
                        var parameters = string.Join(", ", methodInfo.Parameters.Select(mi => GetKeywordForTypeName(mi.Type) + " " + mi.Name).ToArray());
                        
                        worksheet.Cells[rowIndex, METHOD_NAME] = new Cell(methodName);
                        worksheet.Cells[rowIndex, NAMESPACE] = new Cell(namesp);
                        worksheet.Cells[rowIndex, TYPE_NAME] = new Cell(typeInfo.Kind.ToString().ToLower() + " " + typeName);
                        worksheet.Cells[rowIndex, VERSION] = new Cell(version);
                        worksheet.Cells[rowIndex, CHANGE] = new Cell(kind.ToString());
                        worksheet.Cells[rowIndex, PARAMETERS] = new Cell(parameters);

                        rowIndex++;
                    }
                }
            }
        }

        private string GetKeywordForTypeName(string typeName)
        {
            switch (typeName)
            {
                case "System.Boolean":
                    return "bool";
                case "System.Byte":
                    return "byte";
                case "System.SByte":
                    return "sbyte";
                case "System.Char":
                    return "char";
                case "System.Decimal":
                    return "decimal";
                case "System.Double":
                    return "double";
                case "System.Single":
                    return "float";
                case "System.Int32":
                    return "int";
                case "System.UInt32":
                    return "uint";
                case "System.Int64":
                    return "long";
                case "System.Object":
                    return "object";
                case "System.Int16":
                    return "short";
                case "System.UInt16":
                    return "ushort";
                case "System.String":
                    return "string";

                default:
                    return typeName;
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