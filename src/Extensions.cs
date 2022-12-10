﻿using System.Text.RegularExpressions;

namespace AdventOfCode
{
    internal static class Extensions
    {
        public static TValue GetOrAdd<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
            where TValue : new() 
            where TKey : notnull
        {
            if (!dictionary.TryGetValue(key, out var value))
            {
                value = new();
                dictionary.Add(key, value);
            }

            return value;
        }

        public static IEnumerable<T> IntersectMany<T>(this IEnumerable<IEnumerable<T>> source)
        {
            return source.Aggregate((s1, s2) => s1.Intersect(s2));
        }

        public static T[] New<T>(this T[] array) where T : new()
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = new();
            }

            return array;
        }

        public static (T1, T2) As<T1, T2>(this Array array)
            where T1 : IConvertible
            where T2 : IConvertible
        {
            var value1 = (T1)Convert.ChangeType(array.GetValue(0)!, typeof(T1));
            var value2 = (T2)Convert.ChangeType(array.GetValue(1)!, typeof(T2));

            return (value1, value2);
        }

        public static T As<T>(this Match match)
            where T : IConvertible
        {
            return (T)Convert.ChangeType(match.Groups[1].Value, typeof(T));
        }

        public static (T1, T2) As<T1, T2>(this Match match)
            where T1 : IConvertible
            where T2 : IConvertible
        {
            var value1 = (T1)Convert.ChangeType(match.Groups[1].Value, typeof(T1));
            var value2 = (T2)Convert.ChangeType(match.Groups[2].Value, typeof(T2));

            return (value1, value2);
        }

        public static (T1, T2, T3) As<T1, T2, T3>(this Match match)
            where T1 : IConvertible
            where T2 : IConvertible
            where T3 : IConvertible
        {
            var value1 = (T1)Convert.ChangeType(match.Groups[1].Value, typeof(T1));
            var value2 = (T2)Convert.ChangeType(match.Groups[2].Value, typeof(T2));
            var value3 = (T3)Convert.ChangeType(match.Groups[3].Value, typeof(T3));

            return (value1, value2, value3);
        }

        public static (T1, T2, T3, T4) As<T1, T2, T3, T4>(this Match match)
            where T1 : IConvertible
            where T2 : IConvertible
            where T3 : IConvertible
            where T4 : IConvertible
        {
            var value1 = (T1)Convert.ChangeType(match.Groups[1].Value, typeof(T1));
            var value2 = (T2)Convert.ChangeType(match.Groups[2].Value, typeof(T2));
            var value3 = (T3)Convert.ChangeType(match.Groups[3].Value, typeof(T3));
            var value4 = (T4)Convert.ChangeType(match.Groups[4].Value, typeof(T4));

            return (value1, value2, value3, value4);
        }

        public static (T1, T2, T3, T4, T5) As<T1, T2, T3, T4, T5>(this Match match)
            where T1 : IConvertible
            where T2 : IConvertible
            where T3 : IConvertible
            where T4 : IConvertible
            where T5 : IConvertible
        {
            var value1 = (T1)Convert.ChangeType(match.Groups[1].Value, typeof(T1));
            var value2 = (T2)Convert.ChangeType(match.Groups[2].Value, typeof(T2));
            var value3 = (T3)Convert.ChangeType(match.Groups[3].Value, typeof(T3));
            var value4 = (T4)Convert.ChangeType(match.Groups[4].Value, typeof(T4));
            var value5 = (T5)Convert.ChangeType(match.Groups[5].Value, typeof(T5));

            return (value1, value2, value3, value4, value5);
        }

        public static void Deconstruct<T>(this IEnumerable<T> enumerable, out T? value1)
        {
            value1 = enumerable.First();
        }

        public static void Deconstruct<T>(this IEnumerable<T> enumerable, out T? value1, out T? value2)
        {
            using var enumerator = enumerable.GetEnumerator();
            value1 = TryEnumerate(enumerator);
            value2 = TryEnumerate(enumerator);
        }

        public static void Deconstruct<T>(this IEnumerable<T> enumerable, out T? value1, out T? value2, out T? value3)
        {
            using var enumerator = enumerable.GetEnumerator();
            value1 = TryEnumerate(enumerator);
            value2 = TryEnumerate(enumerator);
            value3 = TryEnumerate(enumerator);
        }

        public static List<T[]> GetPermutations<T>(this IEnumerable<T> source)
        {
            var sourceArray = source.ToArray();
            var results = new List<T[]>();
            Permute(sourceArray, 0, sourceArray.Length - 1, results);
            return results;
        }

        private static T? TryEnumerate<T>(IEnumerator<T> enumerator)
        {
            return enumerator.MoveNext() ? enumerator.Current : default;
        }

        private static void Permute<T>(T[] elements, int recursionDepth, int maxDepth, ICollection<T[]> results)
        {
            if (recursionDepth == maxDepth)
            {
                results.Add(elements.ToArray());
                return;
            }

            for (var i = recursionDepth; i <= maxDepth; i++)
            {
                Swap(ref elements[recursionDepth], ref elements[i]);
                Permute(elements, recursionDepth + 1, maxDepth, results);
                Swap(ref elements[recursionDepth], ref elements[i]);
            }
        }

        private static void Swap<T>(ref T a, ref T b)
        {
            T tmp = a;
            a = b;
            b = tmp;
        }
    }
}
