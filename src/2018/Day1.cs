namespace AdventOfCode._2018;

internal class Day1 : Problem
{
    public override void Solve()
    {
        var result = File.ReadLines(Input)
            .Select(int.Parse)
            .Sum();

        Console.WriteLine(result);
    }

    public override void Solve2()
    {
        var pastValues = new HashSet<int>();

        int value = 0;

        while (true)
        {
            foreach (var offset in File.ReadLines(Input).Select(int.Parse))
            {
                value += offset;

                if (!pastValues.Add(value))
                {
                    goto found;
                }
            }
        }

        found:
        Console.WriteLine(value);
    }
}