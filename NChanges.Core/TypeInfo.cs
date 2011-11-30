using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace NChanges.Core
{
    [DebuggerDisplay("{Name}")]
    public class TypeInfo
    {
        private List<MemberInfo> _members = new List<MemberInfo>();
        private List<TypeChangeInfo> _changes = new List<TypeChangeInfo>();

        public string Name { get; set; }
        public string Namespace { get; set; }
        public TypeKind Kind { get; set; }
        public bool Obsolete { get; set; }
        public string ObsoleteMessage { get; set; }
        public ICollection<MemberInfo> Members { get { return _members; } }
        public ICollection<TypeChangeInfo> Changes { get { return _changes; } }

        public void ReadType(Type type)
        {
            Name = type.Name;
            Namespace = type.Namespace;

            if (type.IsSubclassOf(typeof(Delegate))) // Delegates are classes so this has to appear before IsClass!
            {
                Kind = TypeKind.Delegate;
            }
            else if (type.IsClass)
            {
                Kind = TypeKind.Class;
            }
            else if (type.IsInterface)
            {
                Kind = TypeKind.Interface;
            }
            else if (type.IsEnum) // Enums are value types so this has to appear before IsValueType!
            {
                Kind = TypeKind.Enum;
            }
            else if (type.IsValueType)
            {
                Kind = TypeKind.ValueType;
            }

            var obsoleteAttribute = (ObsoleteAttribute)Attribute.GetCustomAttribute(type, typeof(ObsoleteAttribute));

            if (obsoleteAttribute != null)
            {
                Obsolete = true;
                ObsoleteMessage = obsoleteAttribute.Message;
            }

            foreach (var member in type.GetMembers())
            {
                if (MemberIsNotDeclaredOnThisType(member, type) ||
                    MemberIsSpecialMethodButNotConstructor(member) ||
                    MemberIsSpecialField(member) ||
                    MemberIsOverride(member))
                {
                    continue;
                }

                var memberInfo = new MemberInfo();
                memberInfo.ReadMember(member);

                _members.Add(memberInfo);
            }
        }

        private static bool MemberIsNotDeclaredOnThisType(System.Reflection.MemberInfo member, Type type)
        {
            return !ReferenceEquals(member.DeclaringType, type);
        }

        private static bool MemberIsSpecialMethodButNotConstructor(System.Reflection.MemberInfo member)
        {
            return (member is MethodBase) &&
                   ((MethodBase)member).IsSpecialName &&
                   !(member is ConstructorInfo);
        }

        private static bool MemberIsSpecialField(System.Reflection.MemberInfo member)
        {
            return (member is FieldInfo) &&
                   (((FieldInfo)member).Attributes & FieldAttributes.RTSpecialName) == FieldAttributes.RTSpecialName;
        }

        private static bool MemberIsOverride(System.Reflection.MemberInfo member)
        {
            if (member is MethodInfo)
            {
                var method = (MethodInfo)member;

                if (method.IsVirtual && !ReferenceEquals(method.GetBaseDefinition(), method))
                {
                    return true;
                }
            }

            if (member is PropertyInfo)
            {
                var property = (PropertyInfo)member;

                var getMethod = property.GetGetMethod();
                var setMethod = property.GetGetMethod();

                if ((getMethod == null || (getMethod.IsVirtual && MemberIsOverride(getMethod))) &&
                    (setMethod == null || (setMethod.IsVirtual && MemberIsOverride(setMethod))))
                {
                    return true;
                }
            }

            return false;
        }

        public void WriteXml(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("type");
            xmlWriter.WriteAttributeString("name", Name);
            xmlWriter.WriteAttributeString("namespace", Namespace);
            xmlWriter.WriteAttributeString("kind", Kind.ToString());

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

            foreach (var member in Members)
            {
                member.WriteXml(xmlWriter);
            }

            xmlWriter.WriteEndElement();
        }

        public void ReadXml(XmlReader xmlReader)
        {
            Name = xmlReader.GetAttribute("name");
            Namespace = xmlReader.GetAttribute("namespace");
            Kind = (TypeKind)Enum.Parse(typeof(TypeKind), xmlReader.GetAttribute("kind"));
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
                            var typeChangeInfo = new TypeChangeInfo();
                            typeChangeInfo.ReadXml(xmlReader);
                            Changes.Add(typeChangeInfo);
                        }
                        else if (childReader.Name == "member")
                        {
                            var member = new MemberInfo();
                            member.ReadXml(xmlReader);
                            Members.Add(member);
                        }
                    }
                }
            }
        }

        public TypeInfo Clone()
        {
            var newType = (TypeInfo)MemberwiseClone();
            newType._members = new List<MemberInfo>(Members.Select(m => m.Clone()));
            newType._changes = new List<TypeChangeInfo>(Changes.Select(c => c.Clone()));
            return newType;
        }
    }

    public enum TypeKind
    {
        Undefined,
        Class,
        Interface,
        Enum,
        ValueType,
        Delegate
    }
}