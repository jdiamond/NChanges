using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using Mono.Options;
using NChanges.Core;

namespace NChanges.Tool
{
    public class SnapshotCommand
    {
        private readonly OptionSet _optionSet;
        private string _output = "%name%-%version%.xml";
        private string _version = null;

        public SnapshotCommand()
        {
            _optionSet = new OptionSet
                         {
                             { "o|output=", "output file", v => _output = v },
                             { "v|version=", "assumed version", v => _version = v }
                         };
        }

        public void Run(IEnumerable<string> args)
        {
            var extras = _optionSet.Parse(args);

            foreach (string arg in PathHelper.ExpandPaths(extras))
            {
                var absolutePath = Path.GetFullPath(arg);

                var assembly = Assembly.LoadFrom(absolutePath);

                var assemblyInfo = new AssemblyInfo();
                assemblyInfo.ReadAssembly(assembly);

                if (_version != null)
                {
                    assemblyInfo.Version = _version;
                }

                var fileName = PathHelper.FormatPath(_output, assemblyInfo);

                using (var xmlWriter = new XmlTextWriter(fileName, Encoding.UTF8)
                                       {
                                           Formatting = Formatting.Indented
                                       })
                {
                    assemblyInfo.WriteXml(xmlWriter);
                }
            }
        }

        public void ShowHelp()
        {
            _optionSet.WriteOptionDescriptions(Console.Error);
        }
    }
}