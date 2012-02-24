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

namespace NChanges.Tool
{
    public class ExcelCommand
    {
        private readonly OptionSet _optionSet;
        private string _output = "%name%-%version%-report.xls";
        private string _columns = "version,change,namespace,type,member,params,return";
        private string _name = null;
        private bool _multipleSheets = false;
        private int _rowIndex = 2;

        private static readonly Dictionary<string, FieldInfo> _columnMap = new Dictionary<string, FieldInfo>();

        static ExcelCommand()
        {
            _columnMap["version"] = new FieldInfo
                                    {
                                        Header = "Version",
                                        Width = 3000,
                                        Getter = (t, tc, m, mc) => 
                                            tc != null 
                                                ? tc.Version
                                                : mc.Version
                                    };
            _columnMap["change"] = new FieldInfo
                                    {
                                        Header = "Change",
                                        Width = 5000,
                                        Getter = (t, tc, m, mc) =>
                                            tc != null
                                                ? tc.Kind.ToString()
                                                : mc.Kind.ToString()
                                    };
            _columnMap["namespace"] = new FieldInfo
                                    {
                                        Header = "Namespace",
                                        Width = 15000,
                                        Getter = (t, tc, m, mc) => t.Namespace
                                    };
            _columnMap["type"] = new FieldInfo
                                    {
                                        Header = "Type",
                                        Width = 12000,
                                        Getter = (t, tc, m, mc) => t.Kind.ToString().ToLower() + " " + t.Name
                                    };
            _columnMap["member"] = new FieldInfo
                                    {
                                        Header = "Member",
                                        Width = 12000,
                                        Getter = (t, tc, m, mc) =>
                                            m != null
                                                ? m.Name
                                                : ""
                                    };
            _columnMap["params"] = new FieldInfo
                                    {
                                        Header = "Parameters",
                                        Width = 10000,
                                        Getter = (t, tc, m, mc) =>
                                            m != null
                                                ? string.Join(", ", m.Parameters.Select(mi => TypeHelpers.NormalizeTypeName(mi.Type) + " " + mi.Name).ToArray())
                                                : ""
                                    };
            _columnMap["return"] = new FieldInfo
                                    {
                                        Header = "Return Type",
                                        Width = 5000,
                                        Getter = (t, tc, m, mc) => "" // TODO: Uh...
                                    };

            // What about property types?
            // What about type changes (new/removed types)?
            // Should type kind be its own column (so it can be used to filter)?
            // Where does change-specific information go (e.g., added parameter "foo")?
            // Should users be able to configure headers and widths?
        }

        private class FieldInfo
        {
            public string Header { get; set; }
            public int Width { get; set; }
            public Func<TypeInfo, TypeChangeInfo, MemberInfo, MemberChangeInfo, string> Getter { get; set; }
        }

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

            var workbook = new HSSFWorkbook();
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
                        AddHeaders(worksheet);
                        SetColumnSize(worksheet);
                    }

                    AddData(report, worksheet);
                }
            }

            workbook.Write(new FileStream(fileName, FileMode.Create));
        }

        private AssemblyInfo LoadReport(string path)
        {
            var report = new AssemblyInfo();
            report.ReadXml(new XmlTextReader(path));

            return report;
        }

        private void AddHeaders(ISheet worksheet)
        {
            var row = worksheet.CreateRow(0);

            ForEachColumn((i, f) => row.CreateCell(i).SetCellValue(f.Header));
        }

        private void SetColumnSize(ISheet worksheet)
        {
            ForEachColumn((i, f) => worksheet.SetColumnWidth(i, f.Width));
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
                    ForEachColumn((i, f) => row.Add(f.Getter(typeInfo, change, null, null)));
                }

                foreach (var memberInfo in typeInfo.Members)
                {
                    foreach (var change in memberInfo.Changes)
                    {
                        var row = new List<string>();
                        data.Add(row);
                        ForEachColumn((i, f) => row.Add(f.Getter(typeInfo, null, memberInfo, change)));
                    }
                }
            }

            data = data.OrderByDescending(row => row[0]).ToList();

            if (_multipleSheets)
            {
                _rowIndex = 2;
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

            foreach (var col in _columns.Split(','))
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