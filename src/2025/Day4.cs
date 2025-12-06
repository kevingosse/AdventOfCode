namespace AdventOfCode._2025;

internal class Day4 : Problem
{
    public override void Solve()
    {
        var map = File.ReadLines(Input).Select(l => l.ToCharArray()).ToArray();

        int result = 0;

        foreach (var position in map.AllPoints())
        {
            if (map.At(position) != '@')
            {
                continue;
            }

            int rolls = 0;

            foreach (var offset in Point.GetOffsetsWithDiagonals())
            {
                var point = position + offset;

                if (point.IsWithinBounds(map) && map.At(point) == '@')
                {
                    rolls++; 
                }
            }

            if (rolls < 4)
            {
                result++;
            }
        }

        Console.WriteLine(result);
    }

    public override void Solve2()
    {
        var map = File.ReadLines(Input).Select(l => l.ToCharArray()).ToArray();

        int result = 0;
        int removed;

        do
        {
            removed = 0;

            foreach (var position in map.AllPoints())
            {
                if (map.At(position) != '@')
                {
                    continue;
                }

                int rolls = 0;

                foreach (var offset in Point.GetOffsetsWithDiagonals())
                {
                    var point = position + offset;

                    if (point.IsWithinBounds(map) && map.At(point) == '@')
                    {
                        rolls++;
                    }
                }

                if (rolls < 4)
                {
                    removed++;
                    map.At(position) = '.';
                }
            }

            result += removed;
        }
        while (removed > 0);

        Console.WriteLine(result);
    }
}
