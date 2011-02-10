using System.Collections.Generic;
using System.Linq;

namespace NChanges.Core
{
    public static class MemberInfoCollectionExtensions
    {
        public static MemberInfo Get(this ICollection<MemberInfo> source, string name)
        {
            return source.Single(m => m.Name == name);
        }

        public static bool Contains(this ICollection<MemberInfo> source, string name)
        {
            return source.Any(m => m.Name == name);
        }

        public static bool IsOverloaded(this ICollection<MemberInfo> source, string name)
        {
            return source.Count(m => m.Name == name) > 1;
        }
    }
}