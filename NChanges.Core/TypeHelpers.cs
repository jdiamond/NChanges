using System.Text.RegularExpressions;

namespace NChanges.Core
{
    public static class TypeHelpers
    {
        private static readonly Regex GenericTypesRegex = new Regex(
            @",\s*[^,]+\s*,\s*Version=\d+\.\d+\.\d+\.\d+\s*,\s*Culture=[^,]+\s*,\s*PublicKeyToken=[^\]]+",
            RegexOptions.IgnoreCase);

        private static readonly Regex NullablePrefixRegex = new Regex(@"^System.Nullable`1\[\[([^,\]]+)");

        private static readonly Regex GenericTypePrefixRegex = new Regex(@"^([^`]+)`\d+\[(.+)\]$");

        private static readonly Regex GenericTypeParameterRegex = new Regex(@"\[([^]]+)\]");

        public static string CleanUpGenericTypes(string type)
        {
            // Remove the version and other junk so that the parameters can be compared across versions.
            // This only seems to be a problem with generic types (the inner types have the versions).
            return GenericTypesRegex.Replace(type, "");
        }

        public static string NormalizeTypeName(string typeName)
        {
            typeName = CleanUpGenericTypes(typeName);

            var m = NullablePrefixRegex.Match(typeName);

            if (m.Success)
            {
                return NormalizeTypeName(m.Groups[1].Value) + "?";
            }

            m = GenericTypePrefixRegex.Match(typeName);

            if (m.Success)
            {
                var parameters = GenericTypeParameterRegex.Replace(m.Groups[2].Value, n => NormalizeTypeName(n.Groups[1].Value));

                parameters = parameters.Replace(",", ", ");

                return NormalizeTypeName(m.Groups[1].Value) + "<" + parameters + ">";
            }

            if (typeName.EndsWith("[]"))
            {
                return NormalizeTypeName(typeName.Substring(0, typeName.Length - 2)) + "[]";
            }

            if (typeName.StartsWith("[") && typeName.EndsWith("]"))
            {
                typeName = typeName.Substring(1, typeName.Length - 2);
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
            }

            var i = typeName.LastIndexOf(".", System.StringComparison.Ordinal);

            if (i != -1)
            {
                return typeName.Substring(i + 1);
            }

            return typeName;
        }
    }
}