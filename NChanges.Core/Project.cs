using System;
using System.Collections.Generic;
using System.Xml;

namespace NChanges.Core
{
    public class Project
    {
        private readonly List<AssemblyToSnapshot> _assembliesToSnapshot = new List<AssemblyToSnapshot>();

        public string NChangesToolPath { get; set; }
        public string TypesToExclude { get; set; }
        public ICollection<AssemblyToSnapshot> AssembliesToSnapshot { get { return _assembliesToSnapshot; } }

        public void WriteXml(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("Project", "http://schemas.microsoft.com/developer/msbuild/2003");

            WriteProperties(xmlWriter);
            WriteAssembliesToSnapshot(xmlWriter);
            WriteTargets(xmlWriter);

            xmlWriter.WriteEndElement();
        }

        private void WriteProperties(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("PropertyGroup");

            xmlWriter.WriteElementString("NChangesTool", NChangesToolPath);
            xmlWriter.WriteElementString("TypesToExclude", TypesToExclude);

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

        private void WriteTargets(XmlWriter writer)
        {
        }
    }

    public class AssemblyToSnapshot
    {
        public string Path { get; set; }
        public string Version { get; set; }
    }
}