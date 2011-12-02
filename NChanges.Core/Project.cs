using System.Collections.Generic;
using System.Xml;

namespace NChanges.Core
{
    public class Project
    {
        private readonly List<AssemblyToSnapshot> _assembliesToSnapshot = new List<AssemblyToSnapshot>();

        public string NChangesToolPath { get; set; }
        public string TypesToExcludePattern { get; set; }
        public string ExcelOutputPath { get; set; }
        public ICollection<AssemblyToSnapshot> AssembliesToSnapshot { get { return _assembliesToSnapshot; } }

        public void WriteXml(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("Project", "http://schemas.microsoft.com/developer/msbuild/2003");
            xmlWriter.WriteAttributeString("DefaultTargets", "Excel");

            WriteProperties(xmlWriter);
            WriteAssembliesToSnapshot(xmlWriter);
            WriteTargets(xmlWriter);

            xmlWriter.WriteEndElement();
        }

        private void WriteProperties(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("PropertyGroup");

            xmlWriter.WriteElementString("NChangesTool", NChangesToolPath);
            xmlWriter.WriteElementString("TypesToExclude", TypesToExcludePattern);
            xmlWriter.WriteElementString("ExcelOutput", ExcelOutputPath);

            xmlWriter.WriteEndElement();
        }

        private void WriteAssembliesToSnapshot(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("ItemGroup");

            foreach (var ats in AssembliesToSnapshot)
            {
                xmlWriter.WriteStartElement("Assembly");
                xmlWriter.WriteAttributeString("Include", ats.Path);

                if (!string.IsNullOrEmpty(ats.Version))
                {
                    xmlWriter.WriteElementString("Version", ats.Version);
                }

                xmlWriter.WriteEndElement();
            }

            xmlWriter.WriteEndElement();
        }

        private void WriteTargets(XmlWriter xmlWriter)
        {
            WriteSnapshotTarget(xmlWriter);
            WriteReportTarget(xmlWriter);
            WriteExcelTarget(xmlWriter);
            WriteCleanTarget(xmlWriter);
        }

        private static void WriteSnapshotTarget(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("Target");
            xmlWriter.WriteAttributeString("Name", "Snapshot");

            xmlWriter.WriteStartElement("Exec");
            xmlWriter.WriteAttributeString("Command", "$(NChangesTool) snapshot %(Assembly.Identity) -v=%(Version) -x=$(TypesToExclude)");
            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndElement();
        }

        private static void WriteReportTarget(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("Target");
            xmlWriter.WriteAttributeString("Name", "Report");

            xmlWriter.WriteStartElement("Exec");
            xmlWriter.WriteAttributeString("Command", "$(NChangesTool) report *-snapshot.xml");
            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndElement();
        }

        private static void WriteExcelTarget(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("Target");
            xmlWriter.WriteAttributeString("Name", "Excel");

            xmlWriter.WriteStartElement("Exec");
            xmlWriter.WriteAttributeString("Command", "$(NChangesTool) excel *-report.xml -o=$(ExcelOutput)");
            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndElement();
        }

        private static void WriteCleanTarget(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("Target");
            xmlWriter.WriteAttributeString("Name", "Clean");

            xmlWriter.WriteStartElement("Delete");
            xmlWriter.WriteAttributeString("Files", "%(Assembly.Filename)-%(Version)-snapshot.xml");
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("Delete");
            xmlWriter.WriteAttributeString("Files", "%(Assembly.Filename)-%(Version)-report.xml");
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("Delete");
            xmlWriter.WriteAttributeString("Files", "$(ExcelOutput)");
            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndElement();
        }
    }

    public class AssemblyToSnapshot
    {
        public string Path { get; set; }
        public string Version { get; set; }
    }
}