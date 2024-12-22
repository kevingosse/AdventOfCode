using System.Numerics;
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
        var allResults = new Dictionary<uint, int[]>();
        var lines = File.ReadAllLines(Input);

        for (int lineIndex = 0; lineIndex < lines.Length; lineIndex++)
        {
            var line = lines[lineIndex];
            var initialNumber = BigInteger.Parse(line);

            uint sequence = 0;
            var previousNumber = sbyte.Parse(initialNumber.ToString()[^1..]);

            int i = -1;

            foreach (var number in ComputeSecretNumber(initialNumber))
            {
                i++;
                var b = sbyte.Parse(number.ToString()[^1..]);
                sequence = unchecked(sequence << 8 | (byte)(b - previousNumber));

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
}
