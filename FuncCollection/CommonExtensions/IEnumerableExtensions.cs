using System;
using System.Collections.Generic;

namespace FuncCollection.CommonExtensions
{
    public static class IEnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (T item in enumeration)
            {
                action(item);
            }
        }

        /// <summary>
        /// Did I just reimplement select?
        /// </summary>
        public static IEnumerable<K> ForEachTransform<T,K>(this IEnumerable<T> enumeration, Func<T,K> func)
        {
            foreach (T item in enumeration)
            {
                yield return func(item);
            }
        }

        public static IEnumerable<K> SelectWithBorderCases<T, K>(this IList<T> collection, Func<T, K> func, Func<T, K> firstfunc, Func<T,K> lastfunc)
        {
            for (int i = 0; i < collection.Count; i++)
            {
                if (i == 0)
                {
                    yield return firstfunc(collection[i]);
                }
                else if (i == collection.Count -1)
                {
                    yield return lastfunc(collection[i]);
                }
                else
                {
                    yield return func(collection[i]);
                }
            }
        }

        public static IEnumerable<T> SaveTo<T>(this IEnumerable<T> enumeration, ICollection<T> saveLocation, bool clearFirst = false)
        {
            if (clearFirst)
            {
                saveLocation.Clear();
            }

            foreach (var item in enumeration)
            {
                saveLocation.Add(item);
                yield return item;
            }
        }

        public static IEnumerable<T> ProcessEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (var item in enumeration)
            {
                action(item);
                yield return item;
            }
        }

        public static IEnumerable<T> ProcessList<T>(this IEnumerable<T> enumeration, Action<IEnumerable<T>> action)
        {
            action(enumeration);
            return enumeration;
        }
    }
}
