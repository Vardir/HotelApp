using System;
using System.Collections.Generic;

namespace Hotel.Core.Extensions
{
    public static class EnumerableHelpers
    {
        public static void AddRange<T>(this IList<T> list, IEnumerable<T> elements)
        {
            foreach (var elem in elements)
            {
                list.Add(elem);
            }
        }
        public static IEnumerable<T> Select<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            foreach (var element in collection)
                if (predicate(element))
                    yield return element;
        }
    }
}