using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using Mono.Options;
using NChanges.Core;

namespace NChanges.Tool
{
    public class ReportCommand
    {
        private readonly OptionSet _optionSet;
        private string _output = "%name%-%version%-report.xml";
        private string _transform;
        private string _transformOutput = "%name%-%version%-report.html";

        public ReportCommand()
        {
            _optionSet = new OptionSet
                         {
                             { "o|output=", "output file", v => _output = v },
                             { "t|transform=", "XSLT file", v => _transform = v },
                             { "r|result=", "XSLT output file", v => _transformOutput = v }
                         };
        }

        public void Run(IEnumerable<string> args)
        {
            var extras = _optionSet.Parse(args);

            var reporter = new Reporter();

            foreach (string arg in PathHelper.ExpandPaths(extras))
            {
                var assemblyInfo = new AssemblyInfo();

                using (var xmlReader = new XmlTextReader(arg))
                {
                    assemblyInfo.ReadXml(xmlReader);
                }

                reporter.Assemblies.Add(assemblyInfo);
            }

            var report = reporter.GenerateReport();

            var fileName = PathHelper.FormatPath(_output, report);

            using (var xmlWriter = new XmlTextWriter(fileName, Encoding.UTF8)
                                   {
                                       Formatting = Formatting.Indented
                                   })
            {
                report.WriteXml(xmlWriter);
            }

            if (!string.IsNullOrEmpty(_transform))
            {
                var transform = new XslCompiledTransform();
                transform.Load(_transform);

                var document = new XPathDocument(fileName);

                var transformOutput = PathHelper.FormatPath(_transformOutput, report);

                using (var stream = new FileStream(transformOutput, FileMode.Create))
                {
                    transform.Transform(document, null, stream);
                    stream.Flush();
                }
            }
        }

        public void ShowHelp()
        {
            _optionSet.WriteOptionDescriptions(Console.Error);
        }
    }
}