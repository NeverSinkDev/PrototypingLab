using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RapidProtoCore.Core.Utility
{
    public static class AttributeUtility
    {
        /// <summary>
        /// Gets a list of propertyinfos for each Property in a object with a custom attribute
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static List<MemberInfo> CreateAttributedPropertyList(object item)
        {
            return item.GetType()?.GetProperties()?.Where(x => x.CustomAttributes != null).ToList<MemberInfo>();
        }

        /// <summary>
        /// Gets a list of propertyinfos for each Property in a object with a custom attribute
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static List<MemberInfo> CreateAttributedMethodList(object item)
        {
            return item.GetType()?.GetMethods()?.Where(m => m.GetCustomAttribute<ActionPerformerAttribute>() != null).ToList<MemberInfo>();
        }

        /// <summary>
        /// Creates a dictionary of properties on a Class with attribute-marked properties. Groups the property by a PrimaryAttribute. 
        /// </summary>
        /// <typeparam name="TPrimaryAttribute"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public static Dictionary<string, List<MemberInfo>> CreateAttributedPropertyDictionary<TPrimaryAttribute>(object item)
        {
            return CreateMemberDictionary<TPrimaryAttribute>(CreateAttributedPropertyList(item));
        }

        /// <summary>
        /// Creates a dictionary of properties on a Class with attribute-marked properties. Groups the property by a PrimaryAttribute. 
        /// </summary>
        /// <typeparam name="TPrimaryAttribute"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public static Dictionary<string, List<MemberInfo>> CreateAttributedMethodDictionary<TPrimaryAttribute>(object item)
        {
            return CreateMemberDictionary<TPrimaryAttribute>(CreateAttributedMethodList(item));
        }

        /// <summary>
        /// Creates a a dictionary with the relevant Primary Attributes
        /// </summary>
        /// <param name="inputList"></param>
        /// <returns></returns>
        public static Dictionary<string, List<MemberInfo>> CreateMemberDictionary<TPrimaryAttribute>(List<MemberInfo> inputList)
        {
            var result = new Dictionary<string, List<MemberInfo>>();
            foreach (var p in inputList)
            {
                if (p.GetCustomAttributes().Any(a => a is TPrimaryAttribute))
                {
                    var att = p.GetCustomAttributes().FirstOrDefault(a => a is TPrimaryAttribute);
                    string attType = att.GetShortTypeString();
                    if (!result.ContainsKey(attType))
                    {
                        result.Add(attType, new List<MemberInfo>());
                    }

                    result[attType].Add(p);
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the last part of a function or method description. Example System.String.ToString would return ToString
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string GetShortTypeString(this object o)
        {
            var typeName = o.GetType().ToString();
            return typeName.Contains(".")
                ? typeName.Substring(typeName.LastIndexOf(".", StringComparison.Ordinal) + 1)
                : typeName;
        }
    }
}
