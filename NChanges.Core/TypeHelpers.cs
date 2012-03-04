using System.Text.RegularExpressions;

namespace NChanges.Core
{
    public static class TypeHelpers
    {
        private static readonly Regex NullableRegex = new Regex(@"^System.Nullable`1\[\[([^,\]]+)");

        public static string NormalizeTypeName(string typeName)
        {
            var m = NullableRegex.Match(typeName);

            if (m.Success)
            {
                return NormalizeTypeName(m.Groups[1].Value) + "?";
            }

            if (typeName.EndsWith("[]"))
            {
                return NormalizeTypeName(typeName.Substring(0, typeName.Length - 2)) + "[]";
            }

            switch (typeName)
            {
                case "System.Boolean":
                    return "bool";
                case "System.Byte":
                    return "byte";
                case "System.SByte":
                    return "sbyte";
                case "System.Char":
                    return "char";
                case "System.Decimal":
                    return "decimal";
                case "System.Double":
                    return "double";
                case "System.Single":
                    return "float";
                case "System.Int32":
                    return "int";
                case "System.UInt32":
                    return "uint";
                case "System.Int64":
                    return "long";
                case "System.Object":
                    return "object";
                case "System.Int16":
                    return "short";
                case "System.UInt16":
                    return "ushort";
                case "System.String":
                    return "string";
                case "System.Void":
                    return "void";

                default:
                    return typeName;
            }
        }
    }
}