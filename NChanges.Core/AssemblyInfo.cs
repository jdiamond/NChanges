using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;

namespace NChanges.Core
{
    public class AssemblyInfo
    {
        private readonly List<TypeInfo> _types = new List<TypeInfo>();

        public string Name { get; set; }
        public string Version { get; set; }
        public ICollection<TypeInfo> Types { get { return _types; } }

        public void ReadAssembly(Assembly assembly, string excludePattern = null)
        {
            Name = assembly.GetName().Name;
            Version = assembly.GetName().Version.ToString();

            Regex excludeRegex = null;

            if (!string.IsNullOrEmpty(excludePattern))
            {
                excludeRegex = new Regex(excludePattern);
            }

            foreach (var type in assembly.GetTypes())
            {
                if (type.IsPublic && TypeIsNotExcluded(type, excludeRegex))
                {
                    var typeInfo = new TypeInfo();
                    typeInfo.ReadType(type);
                    _types.Add(typeInfo);
                }
            }
        }

        private bool TypeIsNotExcluded(Type type, Regex excludeRegex)
        {
            return excludeRegex == null || !excludeRegex.IsMatch(type.FullName);
        }

        public void WriteXml(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("assembly");
            xmlWriter.WriteAttributeString("name", Name);
            xmlWriter.WriteAttributeString("version", Version);

            foreach (var type in Types)
            {
                type.WriteXml(xmlWriter);
            }

            xmlWriter.WriteEndElement();

            xmlWriter.Flush();
        }

        public void ReadXml(XmlReader xmlReader)
        {
            while (xmlReader.ReadToFollowing("assembly"))
            {
                Name = xmlReader.GetAttribute("name");
                Version = xmlReader.GetAttribute("version");

                if (xmlReader.ReadToDescendant("type"))
                {
                    do
                    {
                        var type = new TypeInfo();
                        type.ReadXml(xmlReader);
                        Types.Add(type);
                    }
                    while (xmlReader.ReadToFollowing("type"));
                }
            }
        }
    }
}