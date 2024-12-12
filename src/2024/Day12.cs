using System.Diagnostics;

namespace AdventOfCode._2024;

internal class Day12 : Problem
{
    public override void Solve()
    {
        var map = File.ReadLines(Input).Select(s => s.ToCharArray()).ToArray();

        var result = 0;

        for (int line = 0; line < map.Length; line++)
        {
            for (int column = 0; column < map[line].Length; column++)
            {
                if (map[line][column] != '.')
                {
                    result += FloodFill(map, new(line, column));
                }
            }
        }

        Console.WriteLine(result);
    }

    private int FloodFill(char[][] map, Point startPosition)
    {
        var startColor = map.At(startPosition);

        int borders = 0;
        int area = 0;

        var covered = new HashSet<Point>();

        var queue = new Queue<Point>();
        queue.Enqueue(startPosition);

        while (queue.Count > 0)
        {
            var position = queue.Dequeue();

            if (!position.IsWithinBounds(map))
            {
                borders++;
                continue;
            }

            var color = map.At(position);

            if (color != startColor)
            {
                borders++;
                continue;
            }

            if (!covered.Add(position))
            {
                continue;
            }

            area++;

            foreach (var offset in Point.GetOffsets())
            {
                queue.Enqueue(position + offset);
            }
        }

        foreach (var point in covered)
        {
            map.At(point) = '.';
        }

        return borders * area;
    }

    public override void Solve2()
    {
        // 875206 too low

        var map = File.ReadLines(Input).Select(s => s.ToCharArray()).ToArray();

        var result = 0;

        for (int line = 0; line < map.Length; line++)
        {
            for (int column = 0; column < map[line].Length; column++)
            {
                if (map[line][column] != '.')
                {
                    result += FloodFill2(map, new(line, column));
                }
            }
        }

        Console.WriteLine(result);
    }

    private int FloodFill2(char[][] map, Point startPosition)
    {
        var startColor = map.At(startPosition);

        int area = 0;

        var covered = new HashSet<Point>();

        var queue = new Queue<Point>();
        queue.Enqueue(startPosition);

        while (queue.Count > 0)
        {
            var position = queue.Dequeue();

            if (!position.IsWithinBounds(map))
            {
                continue;
            }

            var color = map.At(position);

            if (color != startColor)
            {
                continue;
            }

            if (!covered.Add(position))
            {
                continue;
            }

            area++;

            foreach (var offset in Point.GetOffsets())
            {
                queue.Enqueue(position + offset);
            }
        }

        var borders = LeftHand(map, new(covered));

        foreach (var point in covered)
        {
            map.At(point) = '.';
        }

        return borders * area;
    }

    private int LeftHand(char[][] map, HashSet<Point> allPoints)
    {
        int turns = 0;

        while (allPoints.Count > 0)
        {
            var startPosition = allPoints.First();
            allPoints.Remove(startPosition);

            var startColor = map.At(startPosition);

            bool IsValid(Point position)
            {
                return position.IsWithinBounds(map) && map.At(position) == startColor;
            }

            var directions = Point.GetOffsets().ToArray();

            int GetLeftHand(int direction)
            {
                var leftHand = direction - 1;
                if (leftHand < 0)
                {
                    leftHand = directions.Length - 1;
                }
                return leftHand;
            }

            if (Point.GetOffsets().Select(p => p + startPosition).All(IsValid))
            {
                // No walls
                continue;
            }

            int startDirection = 0;

            for (int i = 0; i < directions.Length; i++)
            {
                if (IsValid(startPosition + directions[i])
                    && !IsValid(directions[GetLeftHand(i)] + startPosition))
                {
                    startDirection = i;
                    break;
                }
            }

            Point position = startPosition;

            var direction = startDirection;

            do
            {
                allPoints.Remove(position);

                var newPosition = position + directions[direction];

                if (IsValid(newPosition))
                {
                    position = newPosition;

                    var offset = directions[GetLeftHand(direction)];
                    if (IsValid(directions[GetLeftHand(direction)] + newPosition))
                    {
                        // No wall on our left, we need to turn
                        direction = GetLeftHand(direction);
                        turns++;
                        continue;
                    }

                    continue;
                }

                // We hit a wall, we need to turn
                direction = (direction + 1) % directions.Length;
                turns++;
                continue;
            }
            while (position != startPosition || direction != startDirection);

        }

        return turns;
    }
}
