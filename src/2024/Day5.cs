namespace AdventOfCode._2024;

internal class Day5 : Problem
{
    public override void Solve()
    {
        var lines = File.ReadLines(Input).GetEnumerator();

        var rules = new HashSet<(int, int)>();

        while (lines.MoveNext())
        {
            var line = lines.Current;

            if (line == string.Empty)
            {
                break;
            }

            rules.Add(line.Split('|').As<int, int>());
        }

        long result = 0;

        while (lines.MoveNext())
        {
            var line = lines.Current;

            if (line == string.Empty)
            {
                break;
            }

            var values = line.Split(',').Select(int.Parse).ToArray();

            bool sorted = true;

            for (int i = 0; i < values.Length; i++)
            {
                for (int j = i + 1; j < values.Length; j++)
                {
                    if (rules.Contains((values[j], values[i])))
                    {
                        sorted = false;
                        goto endloop;
                    }
                }
            }

        endloop:
            if (sorted)
            {
                result += values[values.Length / 2];
            }
        }

        Console.WriteLine(result);
    }

    public override void Solve2()
    {
        var lines = File.ReadLines(Input).GetEnumerator();

        var rules = new HashSet<(int, int)>();

        while (lines.MoveNext())
        {
            var line = lines.Current;

            if (line == string.Empty)
            {
                break;
            }

            rules.Add(line.Split('|').As<int, int>());
        }

        long result = 0;

        while (lines.MoveNext())
        {
            var line = lines.Current;

            if (line == string.Empty)
            {
                break;
            }

            var values = line.Split(',').Select(int.Parse).ToArray();

            bool sorted = true;

        start:
            for (int i = 0; i < values.Length; i++)
            {
                for (int j = i + 1; j < values.Length; j++)
                {
                    if (rules.Contains((values[j], values[i])))
                    {
                        (values[i], values[j]) = (values[j], values[i]);
                        sorted = false;
                        goto start;
                    }
                }
            }

            if (!sorted)
            {
                result += values[values.Length / 2];

            }
        }

        Console.WriteLine(result);
    }
}
