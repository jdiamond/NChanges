using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Mono.Options;
using NChanges.Core;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

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
            _optionSet = new OptionSet
                         {
                             { "o|output=", "output file", v => _output = v }
                         };
        }

        public void Run(IEnumerable<string> args)
        {
            var extras = _optionSet.Parse(args);

            var workbook = new HSSFWorkbook();
            string fileName = null;

            foreach (var path in PathHelper.ExpandPaths(extras))
            {
                var report = LoadReport(path);

                if (fileName == null)
                {
                    fileName = PathHelper.FormatPath(_output, report);
                }

                var worksheet = workbook.CreateSheet(report.Name);

                AddHeaders(worksheet);
                SetColumnSize(worksheet);
                AddData(report, worksheet);
            }

            workbook.Write(new FileStream(fileName, FileMode.Create));
        }

        private AssemblyInfo LoadReport(string path)
        {
            var report = new AssemblyInfo();
            report.ReadXml(new XmlTextReader(path));

            return report;
        }

        private static void AddHeaders(ISheet worksheet)
        {
            var row = worksheet.CreateRow(0);

            row.CreateCell(NAMESPACE).SetCellValue("Namespace");
            row.CreateCell(TYPE_NAME).SetCellValue("Type");
            row.CreateCell(METHOD_NAME).SetCellValue("Method");
            row.CreateCell(PARAMETERS).SetCellValue("Parameters");
            row.CreateCell(RETURN_TYPE).SetCellValue("Return Type");
            row.CreateCell(CHANGE).SetCellValue("Change");
            row.CreateCell(VERSION).SetCellValue("Version");
        }

        private static void SetColumnSize(ISheet worksheet)
        {
            worksheet.SetColumnWidth(NAMESPACE, 15000);
            worksheet.SetColumnWidth(TYPE_NAME, 12000);
            worksheet.SetColumnWidth(METHOD_NAME, 12000);
            worksheet.SetColumnWidth(PARAMETERS, 10000);
            worksheet.SetColumnWidth(RETURN_TYPE, 5000);
            worksheet.SetColumnWidth(CHANGE, 5000);
            worksheet.SetColumnWidth(VERSION, 3000);
        }

        private void AddData(AssemblyInfo report, ISheet worksheet)
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

                        var row = worksheet.CreateRow(rowIndex);
                        
                        row.CreateCell(METHOD_NAME).SetCellValue(methodName);
                        row.CreateCell(NAMESPACE).SetCellValue(namesp);
                        row.CreateCell(TYPE_NAME).SetCellValue(typeInfo.Kind.ToString().ToLower() + " " + typeName);
                        row.CreateCell(VERSION).SetCellValue(version);
                        row.CreateCell(CHANGE).SetCellValue(kind.ToString());
                        row.CreateCell(PARAMETERS).SetCellValue(parameters);

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

        public void ShowHelp()
        {
            _optionSet.WriteOptionDescriptions(Console.Error);
        }
    }
}