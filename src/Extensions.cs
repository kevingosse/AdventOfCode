using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal static class Extensions
    {
        public static void Deconstruct<T>(this IEnumerable<T> enumerable, out T value1)
        {
            value1 = enumerable.First();
        }

        public static void Deconstruct<T>(this IEnumerable<T> enumerable, out T value1, out T value2)
        {
            using (var enumerator = enumerable.GetEnumerator())
            {
                value1 = TryEnumerate(enumerator);
                value2 = TryEnumerate(enumerator);
            }
        }

        public static void Deconstruct<T>(this IEnumerable<T> enumerable, out T value1, out T value2, out T value3)
        {
            using (var enumerator = enumerable.GetEnumerator())
            {
                value1 = TryEnumerate(enumerator);
                value2 = TryEnumerate(enumerator);
                value3 = TryEnumerate(enumerator);
            }
        }

        public static List<T[]> GetPermutations<T>(this IEnumerable<T> source)
        {
            var sourceArray = source.ToArray();
            var results = new List<T[]>();
            Permute(sourceArray, 0, sourceArray.Length - 1, results);
            return results;
        }

        private static T TryEnumerate<T>(IEnumerator<T> enumerator)
        {
            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }

            return enumerator.Current;
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
