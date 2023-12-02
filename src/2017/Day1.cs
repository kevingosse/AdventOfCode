namespace AdventOfCode._2017;

internal class Day1 : Problem
{
    public override void Solve()
    {
        long total = 0;

        foreach (var line in File.ReadLines(Input))
        {
            for (int i = 0; i < line.Length; i++)
            {
                var c = line[i];
                var next = line[(i + 1) % line.Length];

                if (c == next)
                {
                    total += c - '0';
                }
            }
        }

        Console.WriteLine(total);
    }

    public override void Solve2()
    {
        long total = 0;

        foreach (var line in File.ReadLines(Input))
        {
            for (int i = 0; i < line.Length; i++)
            {
                var c = line[i];
                var next = line[(i + line.Length / 2) % line.Length];

                if (c == next)
                {
                    total += c - '0';
                }
            }
        }

        Console.WriteLine(total);
    }
}