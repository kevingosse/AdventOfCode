namespace AdventOfCode._2017;

internal class Day6 : Problem
{
    public override void Solve()
    {
        var banks = File.ReadAllText(Input)
            .Split('\t')
            .Select(int.Parse)
            .ToArray();

        var knownConfigurations = new HashSet<string>();

        string ComputeHash() => string.Join(' ', banks);

        int steps = 0;

        knownConfigurations.Add(ComputeHash());

        while (true)
        {
            steps++;
            var (value, index) = banks.IndexOfMax();
            banks[index] = 0;

            for (; value > 0; value--)
            {
                index = (index + 1) % banks.Length;
                banks[index]++;
            }

            if (!knownConfigurations.Add(ComputeHash()))
            {
                break;
            }
        }

        Console.WriteLine(steps);
    }

    public override void Solve2()
    {
        var banks = File.ReadAllText(Input)
            .Split('\t')
            .Select(int.Parse)
            .ToArray();

        var knownConfigurations = new Dictionary<string, int>();

        string ComputeHash() => string.Join(' ', banks);

        int steps = 0;

        knownConfigurations.Add(ComputeHash(), 0);

        while (true)
        {
            steps++;
            var (value, index) = banks.IndexOfMax();
            banks[index] = 0;

            for (; value > 0; value--)
            {
                index = (index + 1) % banks.Length;
                banks[index]++;
            }

            var hash = ComputeHash();

            if (knownConfigurations.TryGetValue(hash, out var previousStep))
            {
                Console.WriteLine(steps - previousStep);
                break;
            }

            knownConfigurations.Add(hash, steps);
        }
    }
}
