namespace AdventOfCode._2024;

internal class Day6 : Problem
{
    public override void Solve()
    {
        var map = File.ReadLines(Input).Select(line => line.ToCharArray()).ToArray();

        // Find starting position
        Point position = default;

        for (int line = 0; line < map.Length && position == default; line++)
        {
            for (int column = 0; column < map[line].Length && position == default; column++)
            {
                if (map[line][column] == '^')
                {
                    position = new(line, column);
                }
            }
        }

        var directions = new Point[] { new(-1, 0), new(0, 1), new(1, 0), new(0, -1) };
        var currentDirection = 0;

        while (true)
        {
            var newPosition = position + directions[currentDirection];

            if (newPosition.Line < 0 || newPosition.Line >= map.Length
                || newPosition.Column < 0 || newPosition.Column >= map[position.Line].Length)
            {
                break;
            }

            if (map[newPosition.Line][newPosition.Column] == '#')
            {
                currentDirection = (currentDirection + 1) % 4;
                continue;
            }

            map[newPosition.Line][newPosition.Column] = 'X';
            position = newPosition;
        }

        Console.WriteLine(map.SelectMany(l => l).Count(c => c is 'X' or '^'));
    }

    public override void Solve2()
    {
        var map = File.ReadLines(Input).Select(line => line.ToCharArray()).ToArray();

        // Find starting position
        Point position = default;

        for (int line = 0; line < map.Length && position == default; line++)
        {
            for (int column = 0; column < map[line].Length && position == default; column++)
            {
                if (map[line][column] == '^')
                {
                    position = new(line, column);
                }
            }
        }

        var startingPosition = position;

        static bool IsLoop(char[][] map, Point position, Point obstacle)
        {
            var path = new HashSet<(Point position, int direction)>();

            var directions = new Point[] { new(-1, 0), new(0, 1), new(1, 0), new(0, -1) };
            var currentDirection = 0;

            while (true)
            {
                var newPosition = position + directions[currentDirection];

                if (newPosition.Line < 0 || newPosition.Line >= map.Length
                    || newPosition.Column < 0 || newPosition.Column >= map[position.Line].Length)
                {
                    return false;
                }

                if (map[newPosition.Line][newPosition.Column] == '#'
                    || newPosition == obstacle)
                {
                    currentDirection = (currentDirection + 1) % 4;
                    continue;
                }

                if (!path.Add((position, currentDirection)))
                {
                    return true;
                }

                position = newPosition;
            }
        }

        var directions = new Point[] { new(-1, 0), new(0, 1), new(1, 0), new(0, -1) };
        var currentDirection = 0;

        var path = new HashSet<Point>();

        while (true)
        {
            var newPosition = position + directions[currentDirection];

            if (newPosition.Line < 0 || newPosition.Line >= map.Length
                || newPosition.Column < 0 || newPosition.Column >= map[position.Line].Length)
            {
                break;
            }

            if (map[newPosition.Line][newPosition.Column] == '#')
            {
                currentDirection = (currentDirection + 1) % 4;
                continue;
            }

            if (map[newPosition.Line][newPosition.Column] != '^')
            {
                path.Add(newPosition);
            }

            position = newPosition;
        }

        Console.WriteLine(path.Count(p => IsLoop(map, startingPosition, p)));
    }

    private record struct Point(int Line, int Column)
    {
        public static Point operator +(Point a, Point b)
        {
            return new Point { Line = a.Line + b.Line, Column = a.Column + b.Column };
        }
    }
}
