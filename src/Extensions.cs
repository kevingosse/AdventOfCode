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

        private static T TryEnumerate<T>(IEnumerator<T> enumerator)
        {
            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }

            return enumerator.Current;
        }
    }
}
