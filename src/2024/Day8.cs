namespace AdventOfCode._2024;

internal class Day8 : Problem
{
    public override void Solve()
    {
        var antennas = new List<(Point position, char frequency)>();

        var lines = File.ReadAllLines(Input);

        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                if (lines[i][j] != '.')
                {
                    antennas.Add((new(i, j), lines[i][j]));
                }
            }
        }

        var antinodes = new HashSet<Point>();

        bool IsWithinBounds(Point point)
        {
            return point.Line >= 0 && point.Line < lines.Length && point.Column >= 0 && point.Column < lines[0].Length;
        }

        for (int i = 0; i < antennas.Count; i++)
        {
            for (int j = i + 1; j < antennas.Count; j++)
            {
                var first = antennas[i];
                var second = antennas[j];

                if (first.frequency != second.frequency)
                {
                    continue;
                }

                var distance = second.position - first.position;

                var antinode1 = first.position - distance;

                if (IsWithinBounds(antinode1))
                {
                    antinodes.Add(antinode1);
                }

                var antinode2 = second.position + distance;

                if (IsWithinBounds(antinode2))
                {
                    antinodes.Add(antinode2);
                }
            }
        }

        Console.WriteLine(antinodes.Count);
    }

    public override void Solve2()
    {
        var antennas = new List<(Point position, char frequency)>();

        var lines = File.ReadAllLines(Input);

        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                if (lines[i][j] != '.')
                {
                    antennas.Add((new(i, j), lines[i][j]));
                }
            }
        }

        var antinodes = new HashSet<Point>();

        bool IsWithinBounds(Point point)
        {
            return point.Line >= 0 && point.Line < lines.Length && point.Column >= 0 && point.Column < lines[0].Length;
        }

        for (int i = 0; i < antennas.Count; i++)
        {
            for (int j = i + 1; j < antennas.Count; j++)
            {
                var first = antennas[i];
                var second = antennas[j];

                if (first.frequency != second.frequency)
                {
                    continue;
                }

                var distance = second.position - first.position;

                var start = first.position;

                while (IsWithinBounds(start))
                {
                    antinodes.Add(start);
                    start -= distance;
                }

                start = second.position;

                while (IsWithinBounds(start))
                {
                    antinodes.Add(start);
                    start += distance;
                }
            }
        }

        Console.WriteLine(antinodes.Count);
    }

    private record struct Point(int Line, int Column)
    {
        public static Point operator +(Point a, Point b)
        {
            return new Point { Line = a.Line + b.Line, Column = a.Column + b.Column };
        }

        public static Point operator -(Point a, Point b)
        {
            return new Point { Line = a.Line - b.Line, Column = a.Column - b.Column };
        }
    }
}
