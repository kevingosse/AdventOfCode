namespace AdventOfCode._2025;

internal class Day1 : Problem
{
    public override void Solve()
    {
        int current = 50;
        int count = 0;

        foreach (var (direction, value) in File.ReadLines(Input)
            .Select(l => l.Match(@"(\w)(\d+)").As<string, int>()))
        {
            current = (current + (direction == "R" ? value : -value)).Mod(100);

            if (current == 0)
            {
                count++;
            }
        }

        Console.WriteLine(count);
    }

    public override void Solve2()
    {
        // -> 6623
        int current = 50;
        int count = 0;

        foreach (var (direction, value) in File.ReadLines(Input)
                     .Select(l => l.Match(@"(\w)(\d+)").As<string, int>()))
        {
            if (direction == "R")
            {
                current += value;
                count += current / 100;
                current %= 100;
            }
            else
            {
                count += value / 100;
                bool isZero = current == 0;
                current -= value % 100;

                if (current <= 0 && !isZero)
                {
                    count++;
                }

                current = current.Mod(100);
            }
        }

        Console.WriteLine(count);
    }
}
