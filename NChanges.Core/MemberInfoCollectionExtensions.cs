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

        public static MemberInfo Get(this ICollection<MemberInfo> source, MemberInfo memberInfo)
        {
            return source.Single(m => m.Name == memberInfo.Name && MatchParameter(m, memberInfo));
        }

        private static bool MatchParameter(MemberInfo current, MemberInfo previous)
        {
            if (current.Parameters.Count() != previous.Parameters.Count())
            {
                return false;
            }

            for (int i = 0; i < current.Parameters.Count(); i++)
            {
                if (current.Parameters[i].Type != previous.Parameters[i].Type)
                {
                    return false;
                }
            }

            return true;
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