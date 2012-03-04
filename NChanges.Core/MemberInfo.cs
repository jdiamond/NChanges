using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace NChanges.Core
{
    [DebuggerDisplay("{Name}")]
    public class MemberInfo
    {
        private List<ParameterInfo> _parameters = new List<ParameterInfo>();
        private List<MemberChangeInfo> _changes = new List<MemberChangeInfo>();

        public string Name { get; set; }
        public MemberKind Kind { get; set; }
        public string Type { get; set; }
        public bool Obsolete { get; set; }
        public string ObsoleteMessage { get; set; }
        public IList<ParameterInfo> Parameters { get { return _parameters; } }
        public ICollection<MemberChangeInfo> Changes { get { return _changes; } }

        public void ReadMember(System.Reflection.MemberInfo memberInfo)
        {
            Name = memberInfo.Name;

            if (memberInfo is ConstructorInfo)
            {
                Kind = MemberKind.Constructor;
                Type = "";
                ReadParameters(((ConstructorInfo)memberInfo).GetParameters());
            }
            else if (memberInfo is MethodInfo)
            {
                Kind = MemberKind.Method;
                Type = ((MethodInfo)memberInfo).ReturnType.FullName;
                ReadParameters(((MethodInfo)memberInfo).GetParameters());
            }
            else if (memberInfo is PropertyInfo)
            {
                Kind = MemberKind.Property;
                Type = ((PropertyInfo)memberInfo).PropertyType.FullName;
            }
            else if (memberInfo is EventInfo)
            {
                Kind = MemberKind.Event;
                Type = ((EventInfo)memberInfo).EventHandlerType.FullName;
            }
            else if (memberInfo is FieldInfo)
            {
                Kind = MemberKind.Field;
                Type = ((FieldInfo)memberInfo).FieldType.FullName;
            }

            var obsoleteAttribute = (ObsoleteAttribute)Attribute.GetCustomAttribute(memberInfo, typeof(ObsoleteAttribute));

            if (obsoleteAttribute != null)
            {
                Obsolete = true;
                ObsoleteMessage = obsoleteAttribute.Message;
            }
        }

        private void ReadParameters(IEnumerable<System.Reflection.ParameterInfo> parameters)
        {
            foreach (var pi in parameters)
            {
                Parameters.Add(new ParameterInfo
                               {
                                   Name = pi.Name,
                                   Type = TypeHelpers.CleanUpGenericTypes(pi.ParameterType.FullName ?? pi.ParameterType.Name)
                               });
            }
        }

        public void WriteXml(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("member");

            xmlWriter.WriteAttributeString("name", Name);
            xmlWriter.WriteAttributeString("kind", Kind.ToString());
            xmlWriter.WriteAttributeString("type", Type);

            if (Obsolete)
            {
                xmlWriter.WriteAttributeString("obsolete", Obsolete.ToString());
            }

            if (ObsoleteMessage != null)
            {
                xmlWriter.WriteAttributeString("obsoleteMessage", ObsoleteMessage);
            }

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
            Type = xmlReader.GetAttribute("type");
            Obsolete = string.Equals(xmlReader.GetAttribute("obsolete"), "true", StringComparison.OrdinalIgnoreCase);
            ObsoleteMessage = xmlReader.GetAttribute("obsoleteMessage");

            if (!xmlReader.IsEmptyElement)
            {
                var childReader = xmlReader.ReadSubtree();

                while (childReader.Read())
                {
                    if (childReader.NodeType == XmlNodeType.Element)
                    {
                        if (childReader.Name == "change")
                        {
                            var memberChangeInfo = new MemberChangeInfo();
                            memberChangeInfo.ReadXml(xmlReader);
                            Changes.Add(memberChangeInfo);
                        }
                        else if (childReader.Name == "param")
                        {
                            var parameter = new ParameterInfo();
                            parameter.ReadXml(xmlReader);
                            Parameters.Add(parameter);
                        }
                    }
                }
            }
        }

        public MemberInfo Clone()
        {
            var newMember = (MemberInfo)MemberwiseClone();
            newMember._parameters = new List<ParameterInfo>(Parameters.Select(p => p.Clone()));
            newMember._changes = new List<MemberChangeInfo>(Changes.Select(c => c.Clone()));
            return newMember;
        }

        public void UpdateParameters(MemberInfo memberInfo)
        {
            _parameters = new List<ParameterInfo>(memberInfo.Parameters.Select(p => p.Clone()));
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