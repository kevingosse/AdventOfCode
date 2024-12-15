namespace AdventOfCode._2017;

internal class Day5 : Problem
{
    public override void Solve()
    {
        var offsets = File.ReadLines(Input).Select(int.Parse).ToArray();

        int steps = 0;
        int index = 0;

        do
        {
            steps++;
            ref var jump = ref offsets[index];
            index = index + jump;
            jump++;
        }
        while (index < offsets.Length && index >= 0);

        Console.WriteLine(steps);
    }

    public override void Solve2()
    {
        var offsets = File.ReadLines(Input).Select(int.Parse).ToArray();

        int steps = 0;
        int index = 0;

        do
        {
            steps++;
            ref var jump = ref offsets[index];
            index = index + jump;
            jump += jump >= 3 ? -1 : 1;
        }
        while (index < offsets.Length && index >= 0);

        Console.WriteLine(steps);
    }
}
