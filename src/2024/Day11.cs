namespace AdventOfCode._2024;

internal class Day11 : Problem
{
    public override void Solve()
    {
        var stones = new LinkedList<long>(File.ReadAllText(Input).Split(' ').Select(long.Parse));

        for (int i = 0; i < 25; i++)
        {
            var stone = stones.First;

            while (stone != null)
            {
                var value = stone.Value;

                if (value == 0)
                {
                    stone.Value = 1;
                }
                else
                {
                    var str = value.ToString();

                    if (str.Length % 2 == 0)
                    {
                        var firstValue = long.Parse(str.AsSpan().Slice(0, str.Length / 2));
                        var secondValue = long.Parse(str.AsSpan().Slice(str.Length / 2));

                        stones.AddBefore(stone, firstValue);
                        stone.Value = secondValue;
                    }
                    else
                    {
                        stone.Value *= 2024;
                    }
                }

                stone = stone.Next;
                continue;
            }
        }

        Console.WriteLine(stones.Count);
    }

    public override void Solve2()
    {
        var stones = File.ReadAllText(Input).Split(' ').Select(long.Parse).ToArray();

        long result = 0;

        foreach (var stone in stones)
        {
            result += ComputeStoneCount(stone, 0, new());
        }

        Console.WriteLine(result);
    }

    private long ComputeStoneCount(long value, int step, Dictionary<(long value, int step), long> knownResults)
    {
        if (step == 75)
        {
            return 1;
        }

        if (knownResults.TryGetValue((value, step), out var count))
        {
            return count;
        }

        if (value == 0)
        {
            count = ComputeStoneCount(1, step + 1, knownResults);
        }
        else
        {
            var str = value.ToString();

            if (str.Length % 2 == 0)
            {
                var firstValue = long.Parse(str[..(str.Length / 2)]);
                var secondValue = long.Parse(str[(str.Length / 2)..]);

                count = ComputeStoneCount(firstValue, step + 1, knownResults)
                    + ComputeStoneCount(secondValue, step + 1, knownResults);
            }
            else
            {
                count = ComputeStoneCount(value * 2024, step + 1, knownResults);
            }
        }

        knownResults.Add((value, step), count);
        return count;
    }
}
