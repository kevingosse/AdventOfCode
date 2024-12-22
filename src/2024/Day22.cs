using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace AdventOfCode._2024;

internal class Day22 : Problem
{
    public override void Solve()
    {
        BigInteger result = 0;

        foreach (var line in File.ReadLines(Input))
        {
            var number = BigInteger.Parse(line);
            result += ComputeSecretNumber(number).Last();
        }

        Console.WriteLine(result);
    }

    private IEnumerable<BigInteger> ComputeSecretNumber(BigInteger number)
    {
        const int Modulo = 16777216;

        for (int i = 0; i < 2000; i++)
        {
            number = (number * 64) ^ number;
            number = number % Modulo;

            number = (number / 32) ^ number;
            number = number % Modulo;

            number = (number * 2048) ^ number;
            number = number % Modulo;

            yield return number;
        }
    }

    public override void Solve2()
    {
        var allResults = new Dictionary<Sequence, int[]>();

        var lines = File.ReadAllLines(Input);

        for (int lineIndex = 0; lineIndex < lines.Length; lineIndex++)
        {
            var line = lines[lineIndex];
            var initialNumber = BigInteger.Parse(line);

            Sequence sequence = 0;
            var previousNumber = sbyte.Parse(initialNumber.ToString()[^1..]);

            int i = -1;

            foreach (var number in ComputeSecretNumber(initialNumber))
            {
                i++;
                var b = sbyte.Parse(number.ToString()[^1..]);
                sequence = unchecked(((uint)sequence) << 8 | (byte)(b - previousNumber));

                if (i < 4)
                {
                    previousNumber = b;
                    continue;
                }

                ref var list = ref CollectionsMarshal.GetValueRefOrAddDefault(allResults, sequence, out var exists);

                if (!exists)
                {
                    list = new int[lines.Length];
                    Array.Fill(list, -1);
                }

                if (list![lineIndex] == -1)
                {
                    list![lineIndex] = b;
                }

                previousNumber = b;
            }
        }

        var result = allResults.Values.Select(v => v.Where(n => n != -1).Sum()).Max();
        Console.WriteLine(result);
    }

    [InlineArray(4)]
    private unsafe struct Sequence : IEquatable<Sequence>
    {
        private sbyte _value;

        public static implicit operator uint(Sequence sequence)
        {
            var ptr = &sequence;
            return *(uint*)ptr;
        }

        public static implicit operator Sequence(uint value)
        {
            var ptr = &value;
            return *(Sequence*)ptr;
        }

        public readonly bool Equals(Sequence other)
        {
            return (uint)this == (uint)other;
        }

        public override readonly int GetHashCode()
        {
            return ((uint)this).GetHashCode();
        }

        public override readonly bool Equals(object? obj)
        {
            return obj is Sequence seq && Equals(seq);
        }
    }
}
