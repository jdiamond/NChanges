using System.Collections.Generic;
using System.Reflection;
using System.Xml;

namespace NChanges.Core
{
    public class AssemblyInfo
    {
        private readonly List<TypeInfo> _types = new List<TypeInfo>();

        public string Name { get; set; }
        public string Version { get; set; }
        public ICollection<TypeInfo> Types { get { return _types; } }

        public void ReadAssembly(Assembly assembly)
        {
            Name = assembly.GetName().Name;
            Version = assembly.GetName().Version.ToString();

            foreach (var type in assembly.GetTypes())
            {
                if (type.IsPublic)
                {
                    var typeInfo = new TypeInfo();
                    typeInfo.ReadType(type);
                    _types.Add(typeInfo);
                }
            }
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