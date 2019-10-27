using System;
using System.Linq;
using System.Reflection;

namespace CommonModules.ViewGeneration
{
    public static class ElementMatchingLibrary
    {
        public static Func<string, MemberInfo, bool> CreateSimpleStringMatchFunction(string value)
        {
            return new Func<string, MemberInfo, bool>((s, v) => value == s);
        }

        public static Func<string, MemberInfo, bool> CreateSimpleStringAndSyncMatchFunction(string value, bool async)
        {
            return new Func<string, MemberInfo, bool>((s, v) => value == s && v.CustomAttributes.Any(x => x.AttributeType.Name.ToLower().Contains("async") == async));
        }
    }
}
