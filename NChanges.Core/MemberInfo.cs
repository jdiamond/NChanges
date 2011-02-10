using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace NChanges.Core
{
    public class MemberInfo
    {
        private List<ParameterInfo> _parameters = new List<ParameterInfo>();
        private List<MemberChangeInfo> _changes = new List<MemberChangeInfo>();

        public string Name { get; set; }
        public MemberKind Kind { get; set; }
        public IList<ParameterInfo> Parameters { get { return _parameters; } }
        public ICollection<MemberChangeInfo> Changes { get { return _changes; } }

        public void ReadMember(System.Reflection.MemberInfo memberInfo)
        {
            Name = memberInfo.Name;

            if (memberInfo is ConstructorInfo)
            {
                Kind = MemberKind.Constructor;
                ReadParameters(((ConstructorInfo)memberInfo).GetParameters());
            }
            else if (memberInfo is MethodInfo)
            {
                Kind = MemberKind.Method;
                ReadParameters(((MethodInfo)memberInfo).GetParameters());
            }
            else if (memberInfo is PropertyInfo)
            {
                Kind = MemberKind.Property;
            }
            else if (memberInfo is EventInfo)
            {
                Kind = MemberKind.Event;
            }
            else if (memberInfo is FieldInfo)
            {
                Kind = MemberKind.Field;
            }
        }

        private void ReadParameters(IEnumerable<System.Reflection.ParameterInfo> parameters)
        {
            foreach (var pi in parameters)
            {
                Parameters.Add(new ParameterInfo
                               {
                                   Name = pi.Name,
                                   Type = pi.ParameterType.FullName
                               });
            }
        }

        public void WriteXml(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("member");

            xmlWriter.WriteAttributeString("name", Name);
            xmlWriter.WriteAttributeString("kind", Kind.ToString());

            foreach (var change in Changes)
            {
                change.WriteXml(xmlWriter);
            }

            foreach (var parameter in Parameters)
            {
                parameter.WriteXml(xmlWriter);
            }

            xmlWriter.WriteEndElement();
        }

        public void ReadXml(XmlReader xmlReader)
        {
            Name = xmlReader.GetAttribute("name");
            Kind = (MemberKind)Enum.Parse(typeof(MemberKind), xmlReader.GetAttribute("kind"));

            if (xmlReader.ReadToDescendant("param"))
            {
                do
                {
                    var parameter = new ParameterInfo();
                    parameter.ReadXml(xmlReader);
                    Parameters.Add(parameter);
                }
                while (xmlReader.ReadToNextSibling("param"));
            }
        }

        public MemberInfo Clone()
        {
            var newMember = (MemberInfo)MemberwiseClone();
            newMember._parameters = new List<ParameterInfo>(Parameters.Select(p => p.Clone()));
            newMember._changes = new List<MemberChangeInfo>(Changes.Select(c => c.Clone()));
            return newMember;
        }
    }

    public enum MemberKind
    {
        Undefined,
        Constructor,
        Method,
        Property,
        Event,
        Field
    }
}