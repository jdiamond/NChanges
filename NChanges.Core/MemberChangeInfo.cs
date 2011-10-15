using System.Xml;

namespace NChanges.Core
{
    public class MemberChangeInfo
    {
        public MemberChangeKind Kind { get; set; }
        public string Version { get; set; }
        public string Old { get; set; }
        public string New { get; set; }

        public MemberChangeInfo Clone()
        {
            return (MemberChangeInfo)MemberwiseClone();
        }

        public void WriteXml(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("change");
            xmlWriter.WriteAttributeString("kind", Kind.ToString());
            xmlWriter.WriteAttributeString("version", Version);

            if (!string.IsNullOrEmpty(Old))
            {
                xmlWriter.WriteAttributeString("old", Old);
            }

            if (!string.IsNullOrEmpty(New))
            {
                xmlWriter.WriteAttributeString("new", New);
            }

            xmlWriter.WriteEndElement();
        }
    }

    public enum MemberChangeKind
    {
        Undefined,
        Added,
        Removed,
        AddedParameter,
        RemovedParameter,
        Obsoleted
    }
}