using System.Runtime.InteropServices;

namespace AdventOfCode._2024;

internal class Day18 : Problem
{
    private const int Width = 71;
    private const int Height = 71;

    public override void Solve()
    {
        var map = new char[Height][];

        for (int i = 0; i < Height; i++)
        {
            map[i] = new char[Width];
        }

        foreach ((int x, int y) in File.ReadLines(Input).Take(1024).Select(l => l.Split(',').As<int, int>()))
        {
            var point = new Point(y, x);
            map.At(point) = '#';
        }

        int result = FindBestPath(map, new());
        Console.WriteLine(result);
    }

    public override void Solve2()
    {
        var input = File.ReadLines(Input).Select(l => l.Split(',').As<int, int>()).ToArray();

        var first = 0;
        var last = input.Length - 1;

        while (first < last)
        {
            var middle = (first + last) / 2;

            var map = new char[Height][];

            for (int i = 0; i < Height; i++)
            {
                map[i] = new char[Width];
            }

            for (int i = 0; i < middle; i++)
            {
                map.At((input[i].Item2, input[i].Item1)) = '#';
            }

            if (FindBestPath(map, new()) == int.MaxValue)
            {
                last = middle;
            }
            else
            {
                first = middle + 1;
            }
        }

        var result = input[first - 1];
        Console.WriteLine($"{result.Item1},{result.Item2}");
    }

    private int FindBestPath(char[][] map, Dictionary<Point, int> cache)
    {
        var queue = new Queue<(Point position, int score)>();
        queue.Enqueue(((0, 0), 0));

        int bestScore = int.MaxValue;

        while (queue.Count > 0)
        {
            var (position, score) = queue.Dequeue();
            ref var previousScore = ref CollectionsMarshal.GetValueRefOrAddDefault(cache, position, out var exists);

            if (exists && previousScore <= score)
            {
                // We've been here with a lower score, no point in exploring this branch
                continue;
            }

            previousScore = score;

            if (position == (Height - 1, Width - 1))
            {
                if (score < bestScore)
                {
                    bestScore = score;
                }

                continue;
            }

            foreach (var offset in Point.Offsets)
            {
                var newPosition = position + offset;

                if (newPosition.IsWithinBounds(map) && map.At(newPosition) != '#')
                {
                    queue.Enqueue((newPosition, score + 1));
                }
            }
        }

        return bestScore;
    }
}
