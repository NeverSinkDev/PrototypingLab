using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FuncCollection.CommonExtensions
{
    public static class ObjectExtensions
    {
        public static T ToObject<T>(this IDictionary<string, object> source)
            where T : class, new()
        {
            var someObject = new T();

            foreach (var item in source)
            {
                someObject.GetType().GetTypeInfo().GetProperty(item.Key).SetValue(someObject, item.Value);
            }

            return someObject;
        }

        public static IDictionary<string, object> AsDictionary(this object source, BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
        {
            return source.GetType().GetTypeInfo().DeclaredProperties.ToDictionary
            (
                propInfo => propInfo.Name,
                propInfo => propInfo.GetValue(source, null)
            );

        }
    }
}

