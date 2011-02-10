using System;
using System.IO;
using System.Xml;

namespace NChanges.Core.Tests
{
    public static class XML
    {
        public static string UseWriter(Action<XmlWriter> action)
        {
            var stringWriter = new StringWriter();
            var xmlWriter = new XmlTextWriter(stringWriter)
                            {
                                Formatting = Formatting.Indented
                            };

            action(xmlWriter);

            return stringWriter.GetStringBuilder().ToString();
        }

        public static void UseReader(Action<XmlReader> action, string xml)
        {
            var stringReader = new StringReader(xml);
            var xmlReader = new XmlTextReader(stringReader);

            action(xmlReader);
        }
    }
}