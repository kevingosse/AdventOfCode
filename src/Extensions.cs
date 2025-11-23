using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace AdventOfCode;

[SuppressMessage("ReSharper", "ConvertToExtensionBlock")]
internal static class Extensions
{
    public static bool IsMatch(this string input, [StringSyntax("Regex")] string pattern)
    {
        return Regex.IsMatch(input, pattern);
    }

    public static Match Match(this string input, [StringSyntax("Regex")] string pattern)
    {
        return Regex.Match(input, pattern);
    }

    public static IEnumerable<(T previous, T current)> EnumerateWithPrevious<T>(this IEnumerable<T> source)
    {
        T? previousValue = default;
        bool firstItem = true;

        foreach (var item in source)
        {
            if (firstItem)
            {
                firstItem = false;
            }
            else
            {
                yield return (previousValue!, item);
            }

            previousValue = item;
        }
    }

    public static (T Max, int Index) IndexOfMax<T>(this IEnumerable<T> values) where T : struct, IComparable<T>
    {
        T? max = null;
        int index = -1;

        int currentIndex = -1;

        foreach (var value in values)
        {
            currentIndex++;

            if (max == null || value.CompareTo(max.Value) > 0)
            {
                max = value;
                index = currentIndex;
            }
        }

        if (max == null)
        {
            throw new InvalidOperationException("Sequence is empty");
        }

        return (max.Value, index);
    }

    public static (T min, T max) MinMax<T>(this (T, T) values) where T : struct, IComparable<T>
    {
        return values.Item1.CompareTo(values.Item2) < 0 ? (values.Item1, values.Item2) : (values.Item2, values.Item1);
    }

    public static (T min, T max) MinMax<T>(this IEnumerable<T> values) where T : struct, IComparable<T>
    {
        T? min = null;
        T? max = null;

        foreach (var value in values)
        {
            if (min == null || value.CompareTo(min.Value) < 0)
            {
                min = value;
            }

            if (max == null || value.CompareTo(max.Value) > 0)
            {
                max = value;
            }
        }

        return (min!.Value, max!.Value);
    }

    public static bool TryGetInt32(this Match match, string key, out int value)
    {
        var group = match.Groups[key];

        if (group.Success)
        {
            value = int.Parse(match.Groups[key].Value);
            return true;
        }

        value = default;
        return false;
    }

    public static int GetInt32(this Match match, string key)
    {
        return int.Parse(match.Groups[key].Value);
    }

    public static int AsInt32(this string value)
    {
        return int.Parse(value);
    }

    public static ref TValue GetOrAdd<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
        where TValue : new()
        where TKey : notnull
    {
        return ref dictionary.GetOrAdd(key, _ => new TValue());
    }

    public static ref TValue GetOrAdd<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, Func<TKey, TValue> factory)
        where TKey : notnull
    {
        ref var value = ref CollectionsMarshal.GetValueRefOrAddDefault(dictionary, key, out var exists);

        if (!exists)
        {
            value = factory(key);
        }

        return ref value!;
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

    public static ref T At<T>(this T[][] array, Point position)
    {
        return ref array[position.Line][position.Column];
    }

    extension (Array array)
    {
        public static T[][] Create<T>(int lines, int columns)
        {
            var result = new T[lines][];

            for (int i = 0; i < lines; i++)
            {
                result[i] = new T[columns];
            }

            return result;
        }
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

    private static void Swap<T>(ref T a, ref T b) => (a, b) = (b, a);
}