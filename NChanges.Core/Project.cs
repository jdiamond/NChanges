using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Xml;

namespace NChanges.Core
{
    public class Project
    {
        private const string MSBuildNamespace = "http://schemas.microsoft.com/developer/msbuild/2003";

        private const string NChangesToolPathPropertyName = "NChangesTool";
        private const string TypesToExcludePatternPropertyName = "TypesToExclude";
        private const string ExcelOutputPathPropertyName = "ExcelOutput";
        private const string AssembliesToSnapshotItemName = "Assembly";
        private const string VersionMetaDataName = "Version";

        private readonly List<AssemblyToSnapshot> _assembliesToSnapshot = new List<AssemblyToSnapshot>();

        public string NChangesToolPath { get; set; }
        public string TypesToExcludePattern { get; set; }
        public string ExcelOutputPath { get; set; }
        public ICollection<AssemblyToSnapshot> AssembliesToSnapshot { get { return _assembliesToSnapshot; } }

        public void WriteXml(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("Project", MSBuildNamespace);
            xmlWriter.WriteAttributeString("DefaultTargets", "Excel");

            WriteProperties(xmlWriter);
            WriteAssembliesToSnapshot(xmlWriter);
            WriteTargets(xmlWriter);

            xmlWriter.WriteEndElement();
        }

        private void WriteProperties(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("PropertyGroup");

            xmlWriter.WriteElementString(NChangesToolPathPropertyName, NChangesToolPath);
            xmlWriter.WriteElementString(TypesToExcludePatternPropertyName, TypesToExcludePattern);
            xmlWriter.WriteElementString(ExcelOutputPathPropertyName, ExcelOutputPath);

            xmlWriter.WriteEndElement();
        }

        private void WriteAssembliesToSnapshot(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("ItemGroup");

            foreach (var ats in AssembliesToSnapshot)
            {
                xmlWriter.WriteStartElement(AssembliesToSnapshotItemName);
                xmlWriter.WriteAttributeString("Include", ats.Path);

                if (!string.IsNullOrEmpty(ats.Version))
                {
                    xmlWriter.WriteElementString(VersionMetaDataName, ats.Version);
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
            xmlWriter.WriteAttributeString(
                "Command",
                string.Format(
                    "$({0}) snapshot %({1}.Identity) -v=%({2}) -x=$({3})",
                    NChangesToolPathPropertyName,
                    AssembliesToSnapshotItemName,
                    VersionMetaDataName,
                    TypesToExcludePatternPropertyName));
            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndElement();
        }

        private static void WriteReportTarget(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("Target");
            xmlWriter.WriteAttributeString("Name", "Report");

            xmlWriter.WriteStartElement("Exec");
            xmlWriter.WriteAttributeString(
                "Command",
                string.Format("$({0}) report *-snapshot.xml", NChangesToolPathPropertyName));
            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndElement();
        }

        private static void WriteExcelTarget(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("Target");
            xmlWriter.WriteAttributeString("Name", "Excel");

            xmlWriter.WriteStartElement("Exec");
            xmlWriter.WriteAttributeString(
                "Command",
                string.Format(
                    "$({0}) excel *-report.xml -o=$({1})",
                    NChangesToolPathPropertyName,
                    ExcelOutputPathPropertyName));
            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndElement();
        }

        private static void WriteCleanTarget(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("Target");
            xmlWriter.WriteAttributeString("Name", "Clean");

            xmlWriter.WriteStartElement("Delete");
            xmlWriter.WriteAttributeString(
                "Files",
                string.Format("%({0}.Filename)-%({1})-snapshot.xml", AssembliesToSnapshotItemName, VersionMetaDataName));
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("Delete");
            xmlWriter.WriteAttributeString(
                "Files",
                string.Format("%({0}.Filename)-%({1})-report.xml", AssembliesToSnapshotItemName, VersionMetaDataName));
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("Delete");
            xmlWriter.WriteAttributeString("Files", string.Format("$({0})", ExcelOutputPathPropertyName));
            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndElement();
        }

        public void ReadXml(XmlReader xmlReader)
        {
            var doc = new XmlDocument();
            doc.Load(xmlReader);

            var nsManager = new XmlNamespaceManager(doc.NameTable);
            nsManager.AddNamespace("msbuild", MSBuildNamespace);

            NChangesToolPath = GetPropertyValue(NChangesToolPathPropertyName, doc, nsManager);
            TypesToExcludePattern = GetPropertyValue(TypesToExcludePatternPropertyName, doc, nsManager);
            ExcelOutputPath = GetPropertyValue(ExcelOutputPathPropertyName, doc, nsManager);

            var nodes = doc.SelectNodes("/msbuild:Project/msbuild:ItemGroup/msbuild:" + AssembliesToSnapshotItemName, nsManager);

            if (nodes != null)
            {
                foreach (var node in nodes.Cast<XmlElement>())
                {
                    var path = node.GetAttribute("Include");

                    string version = null;

                    var versionNode = node.SelectSingleNode("msbuild:" + VersionMetaDataName, nsManager);

                    if (versionNode != null)
                    {
                        version = versionNode.InnerText;
                    }

                    AssembliesToSnapshot.Add(new AssemblyToSnapshot
                                             {
                                                 Path = path,
                                                 Version = version
                                             });
                }
            }
        }

        private static string GetPropertyValue(string propertyName, XmlDocument doc, XmlNamespaceManager nsManager)
        {
            var node = doc.SelectSingleNode("/msbuild:Project/msbuild:PropertyGroup/msbuild:" + propertyName, nsManager);

            if (node != null)
            {
                return node.InnerText;
            }

            return null;
        }

        public static void Run(string projectPath, string targetName)
        {
            var msbuildPath = Path.Combine(RuntimeEnvironment.GetRuntimeDirectory(), "MSBuild.exe");

            Process.Start(msbuildPath, projectPath + " /t:" + targetName);
        }
    }

    public class AssemblyToSnapshot
    {
        public string Path { get; set; }
        public string Version { get; set; }
    }
}