using System.Xml;

namespace NChanges.Core
{
    public class TypeChangeInfo
    {
        public TypeChangeKind Kind { get; set; }
        public string Version { get; set; }

        public TypeChangeInfo Clone()
        {
            return (TypeChangeInfo)MemberwiseClone();
        }

        public void WriteXml(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("change");
            xmlWriter.WriteAttributeString("kind", Kind.ToString());
            xmlWriter.WriteAttributeString("version", Version);
            xmlWriter.WriteEndElement();
        }
    }

    public enum TypeChangeKind
    {
        Undefined,
        Added,
        Removed
    }
}