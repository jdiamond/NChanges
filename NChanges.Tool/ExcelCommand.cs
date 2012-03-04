using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using Mono.Options;
using NChanges.Core;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;

namespace NChanges.Tool
{
    public class ExcelCommand
    {
        private readonly OptionSet _optionSet;
        private string _output = "%name%-%version%-report.xls";
        private string _columns = "assembly,version,change,namespace,typeKind,type,memberKind,member,params,memberType";
        private string[] _splitColumns;
        private string _name;
        private bool _multipleSheets;
        private int _rowIndex = 1;

        private static readonly Dictionary<string, FieldInfo> _columnMap = new Dictionary<string, FieldInfo>();

        static ExcelCommand()
        {
            _columnMap["assembly"] = new FieldInfo
                                     {
                                         Header = "Assembly",
                                         Getter = (a, t, tc, m, mc) => a.Name
                                     };
            _columnMap["version"] = new FieldInfo
                                    {
                                        Header = "Version",
                                        Getter = (a, t, tc, m, mc) => tc != null ? tc.Version : mc.Version
                                    };
            _columnMap["change"] = new FieldInfo
                                   {
                                       Header = "Change",
                                       Getter = (a, t, tc, m, mc) => tc != null ? tc.Kind.ToString() : mc.Kind.ToString()
                                   };
            _columnMap["namespace"] = new FieldInfo
                                      {
                                          Header = "Namespace",
                                          Getter = (a, t, tc, m, mc) => t.Namespace
                                      };
            _columnMap["typeKind"] = new FieldInfo
                                     {
                                         Header = "Type Kind",
                                         Getter = (a, t, tc, m, mc) => t.Kind.ToString()
                                     };
            _columnMap["type"] = new FieldInfo
                                 {
                                     Header = "Type",
                                     Getter = (a, t, tc, m, mc) => t.Name
                                 };
            _columnMap["memberKind"] = new FieldInfo
                                       {
                                           Header = "Member Kind",
                                           Getter = (a, t, tc, m, mc) => m != null ? m.Kind.ToString() : ""
                                       };
            _columnMap["member"] = new FieldInfo
                                   {
                                       Header = "Member",
                                       Getter = (a, t, tc, m, mc) => m != null ? m.Name : ""
                                   };
            _columnMap["params"] = new FieldInfo
                                   {
                                       Header = "Parameters",
                                       Getter = (a, t, tc, m, mc) => m != null ? string.Join(", ", m.Parameters.Select(mi => TypeHelpers.NormalizeTypeName(mi.Type) + " " + mi.Name).ToArray()) : ""
                                   };
            _columnMap["memberType"] = new FieldInfo
                                       {
                                           Header = "Return/Property/Event/Field Type",
                                           Getter = (a, t, tc, m, mc) => m != null ? TypeHelpers.NormalizeTypeName(m.Type) : ""
                                       };

            // Where does change-specific information go (e.g., added parameter "foo")?
        }

        private class FieldInfo
        {
            public string Header { get; set; }
            public Getter Getter { get; set; }
        }

        private delegate string Getter(AssemblyInfo a, TypeInfo t, TypeChangeInfo tc, MemberInfo m, MemberChangeInfo mi);

        public ExcelCommand()
        {
            _optionSet = new OptionSet
                         {
                             { "o|output=", "output file", v => _output = v },
                             { "c|columns=", "columns", v => _columns = v },
                             { "m|multiple-sheets", "create a new worksheet for each report", v => _multipleSheets = true },
                             { "n|name=", "worksheet name regex pattern", v => _name = v }
                         };
        }

        public void Run(IEnumerable<string> args)
        {
            var extras = _optionSet.Parse(args);

            _splitColumns = _columns.Split(',');

            var workbook = new HSSFWorkbook();

            var headerCellStyle = workbook.CreateCellStyle();
            var headerFont = workbook.CreateFont();
            headerFont.Boldweight = (short)FontBoldWeight.BOLD;
            headerCellStyle.SetFont(headerFont);

            string fileName = null;

            ISheet worksheet = null;

            foreach (var path in PathHelper.ExpandPaths(extras))
            {
                var report = LoadReport(path);

                if (fileName == null)
                {
                    fileName = PathHelper.FormatPath(_output, report);
                }

                if (report.HasChanges())
                {
                    var name = report.Name;

                    if (!string.IsNullOrEmpty(_name))
                    {
                        var match = Regex.Match(name, _name);

                        if (match.Success)
                        {
                            name = match.Groups
                                        .Cast<Group>()
                                        .Skip(1)
                                        .First(g => !string.IsNullOrEmpty(g.Value))
                                        .Value;
                        }
                    }

                    if (_multipleSheets || worksheet == null)
                    {
                        worksheet = workbook.CreateSheet(_multipleSheets ? name : "Changes");
                        AddHeaders(worksheet, headerCellStyle);
                    }

                    AddData(report, worksheet);

                    if (_multipleSheets)
                    {
                        FinalizeWorkSheet(worksheet);
                    }
                }
            }

            if (!_multipleSheets)
            {
                FinalizeWorkSheet(worksheet);
            }

            workbook.Write(new FileStream(fileName, FileMode.Create));
        }

        private void FinalizeWorkSheet(ISheet worksheet)
        {
            if (worksheet != null)
            {
                var hssfSheet = worksheet as HSSFSheet;

                if (hssfSheet != null)
                {
                    hssfSheet.SetAutoFilter(new CellRangeAddress(0, _rowIndex - 1, 0, _splitColumns.Length - 1));
                }

                ForEachColumn((i, f) =>
                    {
                        worksheet.AutoSizeColumn(i);
                        // Units are 256 per character?
                        worksheet.SetColumnWidth(i, worksheet.GetColumnWidth(i) + 1024);
                    });
            }
        }

        private AssemblyInfo LoadReport(string path)
        {
            var report = new AssemblyInfo();
            report.ReadXml(new XmlTextReader(path));

            return report;
        }

        private void AddHeaders(ISheet worksheet, ICellStyle headerCellStyle)
        {
            var row = worksheet.CreateRow(0);

            ForEachColumn((i, f) =>
                {
                    var cell = row.CreateCell(i);
                    cell.SetCellValue(f.Header);
                    cell.CellStyle = headerCellStyle;
                });
        }

        private void AddData(AssemblyInfo report, ISheet worksheet)
        {
            var data = new List<List<string>>();

            foreach (var typeInfo in report.Types)
            {
                foreach (var change in typeInfo.Changes)
                {
                    var row = new List<string>();
                    data.Add(row);
                    ForEachColumn((i, f) => row.Add(f.Getter(report, typeInfo, change, null, null)));
                }

                foreach (var memberInfo in typeInfo.Members)
                {
                    foreach (var change in memberInfo.Changes)
                    {
                        var row = new List<string>();
                        data.Add(row);
                        ForEachColumn((i, f) => row.Add(f.Getter(report, typeInfo, null, memberInfo, change)));
                    }
                }
            }

            data = data.OrderByDescending(row => row[0]).ToList();

            if (_multipleSheets)
            {
                _rowIndex = 1;
            }

            foreach (var dataRow in data)
            {
                var row = worksheet.CreateRow(_rowIndex);

                for (var i = 0; i < dataRow.Count; i++)
                {
                    row.CreateCell(i).SetCellValue(dataRow[i]);
                }

                _rowIndex++;
            }
        }

        private void ForEachColumn(Action<int, FieldInfo> worker)
        {
            int i = 0;

            foreach (var col in _splitColumns)
            {
                worker(i++, _columnMap[col.Trim()]);
            }
        }

        public void ShowHelp()
        {
            _optionSet.WriteOptionDescriptions(Console.Error);
            Console.Error.WriteLine();
            Console.Error.WriteLine("Avaliable Columns: " + string.Join(",", _columnMap.Keys.OrderBy(c => c).ToArray()));
            Console.Error.WriteLine("Default Columns: " + _columns);
        }
    }
}