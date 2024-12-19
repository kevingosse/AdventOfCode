namespace AdventOfCode._2024;

internal class Day19 : Problem
{
    public override void Solve()
    {
        var input = File.ReadAllLines(Input);
        var patterns = new HashSet<string>(input[0].Split(", "));
        var cache = new Dictionary<string, long>();

        long result = 0;

        foreach (var pattern in input.Skip(2))
        {
            if (CanApplyPattern(patterns, pattern, cache) > 0)
            {
                result++;
            }
        }

        Console.WriteLine(result);
    }

    public override void Solve2()
    {
        var input = File.ReadAllLines(Input);
        var patterns = new HashSet<string>(input[0].Split(", "));
        var cache = new Dictionary<string, long>();

        long result = 0;

        foreach (var pattern in input.Skip(2))
        {
            result += CanApplyPattern(patterns, pattern, cache);
        }

        Console.WriteLine(result);
    }

    private long CanApplyPattern(HashSet<string> patterns, ReadOnlySpan<char> input, Dictionary<string, long> cache)
    {
        if (input.Length == 0)
        {
            return 1;
        }

        var cacheLookup = cache.GetAlternateLookup<ReadOnlySpan<char>>();

        if (cacheLookup.TryGetValue(input, out var cachedScore))
        {
            return cachedScore;
        }

        var patternsLookup = patterns.GetAlternateLookup<ReadOnlySpan<char>>();
        long result = 0;

        for (int i = 1; i <= input.Length; i++)
        {
            if (patternsLookup.Contains(input[0..i]))
            {
                var score = CanApplyPattern(patterns, input[i..], cache);
                result += score;
                cacheLookup[input[i..]] = score;
            }
        }

        return result;
    }
}
