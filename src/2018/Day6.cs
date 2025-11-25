namespace AdventOfCode._2018;

internal class Day6 : Problem
{
    public override void Solve()
    {
        var points = File.ReadLines(Input)
            .Select(l => l.Match(@"(\d+), (\d+)").As<int, int>())
            .Select(p => new Point(p.Item2, p.Item1))
            .ToArray();

        var maxWidth = points.MaxBy(p => p.Column).Column + 1;
        var maxHeight = points.MaxBy(p => p.Line).Line + 1;

        var map = Array.Create<int>(maxHeight, maxWidth);

        for (int line = 0; line < maxHeight; line++)
        {
            for (int column = 0; column < maxWidth; column++)
            {
                var index = FindIndexOfClosestPoint((line, column), points);
                map.At((line, column)) = index;
            }
        }

        foreach (var point in Border())
        {
            if (map.At(point) >= 0)
            {
                FloodFill(point, -1);
            }
        }

        int maxSize = 0;

        foreach (var point in map.AllPoints())
        {
            var index = map.At(point);

            if (index < 0)
            {
                continue;
            }

            var size = FloodFill(point, -1);

            if (size > maxSize)
            {
                maxSize = size;
            }
        }

        Console.WriteLine(maxSize);
        return;

        int FloodFill(Point origin, int color)
        {
            var baseColor = map.At(origin);

            if (baseColor == color)
            {
                return 0;
            }

            int size = 0;

            var queue = new Queue<Point>();
            queue.Enqueue(origin);

            while (queue.Count > 0)
            {
                var point = queue.Dequeue();

                if (map.At(point) == color)
                {
                    continue;
                }

                map.At(point) = color;
                size++;

                foreach (var offset in Point.GetOffsets())
                {
                    var newPoint = point + offset;

                    if (newPoint.IsWithinBounds(map) && map.At(newPoint) == baseColor)
                    {
                        queue.Enqueue(newPoint);
                    }
                }
            }

            return size;
        }

        IEnumerable<Point> Border()
        {
            for (int column = 0; column < maxWidth; column++)
            {
                yield return new(0, column);
                yield return new(maxHeight - 1, column);
            }

            for (int line = 0; line < maxHeight; line++)
            {
                yield return new(line, 0);
                yield return new(line, maxWidth - 1);
            }
        }
    }

    private static int FindIndexOfClosestPoint(Point source, Point[] points)
    {
        int index = 0;
        int count = 0;
        int minDistance = int.MaxValue;

        for (int i = 0; i < points.Length; i++)
        {
            var distance = source.ManhattanDistanceTo(points[i]);

            if (distance < minDistance)
            {
                minDistance = distance;
                index = i;
                count = 0;
            }
            else if (distance == minDistance)
            {
                // Two or more points are at the same distance
                count++;
            }
        }

        return count == 0 ? index : -1;
    }

    public override void Solve2()
    {
        const int maxDistance = 10000;

        var points = File.ReadLines(Input)
            .Select(l => l.Match(@"(\d+), (\d+)").As<int, int>())
            .Select(p => new Point(p.Item2, p.Item1))
            .ToArray();

        var maxWidth = points.MaxBy(p => p.Column).Column + 1;
        var maxHeight = points.MaxBy(p => p.Line).Line + 1;

        int size = 0;

        for (int line = 0; line < maxHeight; line++)
        {
            for (int column = 0; column < maxWidth; column++)
            {
                var point = new Point(line, column);

                int totalDistance = 0;

                foreach (var source in points)
                {
                    totalDistance += point.ManhattanDistanceTo(source);

                    if (totalDistance > maxDistance)
                    {
                        break;
                    }
                }

                if (totalDistance < maxDistance)
                {
                    size++;
                }
            }
        }

        Console.WriteLine(size);
    }
}
