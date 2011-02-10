using System.Collections.Generic;
using System.Linq;

namespace NChanges.Core
{
    public static class TypeInfoCollectionExtensions
    {
        public static TypeInfo Get(this ICollection<TypeInfo> source, string name)
        {
            return source.Single(t => t.Name == name);
        }

        public static bool Contains(this ICollection<TypeInfo> source, string name)
        {
            return source.Any(t => t.Name == name);
        }
    }
}