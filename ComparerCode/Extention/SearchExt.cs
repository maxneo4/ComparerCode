using System;
using System.Collections.Generic;
using System.Linq;

namespace ComparerCode.Extention
{
    public static class SearchExt
    {
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> enumerable)
        {
            HashSet<T> hashSet = new HashSet<T>();
            foreach (var item in enumerable)
            {
                hashSet.Add(item);
            }
            return hashSet;
        }

        public static HashSet<T> ToHashSet<S,T>(this IEnumerable<S> enumerable, Func<S,T> func)
        {
            HashSet<T> hashSet = new HashSet<T>();
            foreach (var item in enumerable)
            {
                hashSet.Add(func(item));
            }
            return hashSet;
        }

        public static List<T> WhereNotIn<S, T>(this IEnumerable<T> enumerable, IEnumerable<S> toCompare, Func<S, T> func)
        {
            HashSet<T> compareHs = toCompare.ToHashSet(func);
            return enumerable.Where(e => !compareHs.Contains(e)).ToList();
        }

        public static List<T> WhereIn<S, T>(this IEnumerable<T> enumerable, IEnumerable<S> toCompare, Func<S, T> func)
        {
            HashSet<T> compareHs = toCompare.ToHashSet(func);
            return enumerable.Where(e => compareHs.Contains(e)).ToList();
        }

        public static List<T> WherePropertyNotIn<S, T>(this IEnumerable<T> enumerable, Func<T, S> func, IEnumerable<S> toCompare)
        {            
            HashSet<S> hsToCompare = toCompare.ToHashSet();
            return enumerable.Where(e => !hsToCompare.Contains(func(e))).ToList();
        }

        public static List<T> WherePropertyIn<S, T>(this IEnumerable<T> enumerable, Func<T, S> func, IEnumerable<S> toCompare)
        {
            HashSet<S> hsToCompare = toCompare.ToHashSet();
            return enumerable.Where(e => hsToCompare.Contains(func(e))).ToList();
        }
    }
}
