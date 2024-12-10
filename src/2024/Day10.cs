namespace AdventOfCode._2024;

internal class Day10 : Problem
{
    public override void Solve()
    {
        var map = new List<int[]>();

        foreach (var line in File.ReadLines(Input))
        {
            map.Add(line.Select(c => c - '0').ToArray());
        }

        var result = 0;

        for (int line = 0; line < map.Count; line++)
        {
            var row = map[line];

            for (int column = 0; column < row.Length; column++)
            {
                if (row[column] == 0)
                {
                    var destinations = new HashSet<Point>();
                    FindPaths(new(line, column), 0, map, destinations);

                    result += destinations.Count;
                }
            }
        }

        Console.WriteLine(result);
    }

    private void FindPaths(Point position, int currentHeight, List<int[]> map, HashSet<Point> destinations)
    {
        if (currentHeight == 9)
        {
            destinations.Add(position);
            return;
        }

        foreach (var offset in Point.GetOffsets())
        {
            var newPosition = position + offset;

            if (newPosition.Line < 0 || newPosition.Line >= map.Count
                || newPosition.Column < 0 || newPosition.Column >= map[0].Length)
            {
                continue;
            }

            if (map[newPosition.Line][newPosition.Column] == currentHeight + 1)
            {
                FindPaths(newPosition, currentHeight + 1, map, destinations);
            }
        }
    }

    public override void Solve2()
    {
        var map = new List<int[]>();

        foreach (var line in File.ReadLines(Input))
        {
            map.Add(line.Select(c => c - '0').ToArray());
        }

        var result = 0;

        for (int line = 0; line < map.Count; line++)
        {
            var row = map[line];

            for (int column = 0; column < row.Length; column++)
            {
                if (row[column] == 0)
                {
                    result += FindPaths2(new(line, column), 0, map);
                }
            }
        }

        Console.WriteLine(result);
    }

    private int FindPaths2(Point position, int currentHeight, List<int[]> map)
    {
        if (currentHeight == 9)
        {
            return 1;
        }

        int count = 0;

        foreach (var offset in Point.GetOffsets())
        {
            var newPosition = position + offset;

            if (newPosition.Line < 0 || newPosition.Line >= map.Count
                || newPosition.Column < 0 || newPosition.Column >= map[0].Length)
            {
                continue;
            }

            if (map[newPosition.Line][newPosition.Column] == currentHeight + 1)
            {
                count += FindPaths2(newPosition, currentHeight + 1, map);
            }
        }

        return count;
    }
}
