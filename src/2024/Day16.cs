using System.Collections.Immutable;
using System.Runtime.InteropServices;

namespace AdventOfCode._2024;

internal class Day16 : Problem
{
    public override void Solve()
    {
        var map = File.ReadLines(Input).Select(l => l.ToCharArray()).ToArray();

        Point FindStartingPosition()
        {
            for (int line = 0; line < map.Length; line++)
            {
                for (int column = 0; column < map[line].Length; column++)
                {
                    if (map.At((line, column)) == 'S')
                    {
                        return (line, column);
                    }
                }
            }

            throw new InvalidOperationException();
        }

        var startPosition = FindStartingPosition();

        var result = FindBestPath(map, startPosition, 0, new());
        Console.WriteLine(result);
    }

    private int FindBestPath(char[][] map, Point startPosition, Direction startDirection, Dictionary<(Point, Direction), int> cache)
    {
        var queue = new Queue<(Point position, Direction direction, int score)>();
        queue.Enqueue((startPosition, startDirection, 0));

        int bestScore = int.MaxValue;

        while (queue.Count > 0)
        {
            var (position, direction, score) = queue.Dequeue();

            ref var previousScore = ref CollectionsMarshal.GetValueRefOrAddDefault(cache, (position, direction), out var exists);

            if (exists && previousScore <= score)
            {
                // We've been here with a lower score, no point in exploring this branch
                continue;
            }

            previousScore = score;

            if (map.At(position) == 'E')
            {
                if (score < bestScore)
                {
                    bestScore = score;
                }

                continue;
            }

            var newPosition = position + direction;

            if (newPosition.IsWithinBounds(map) && map.At(newPosition) != '#')
            {
                queue.Enqueue((newPosition, direction, score + 1));
            }

            queue.Enqueue((position, direction.Left(), score + 1000));
            queue.Enqueue((position, direction.Right(), score + 1000));
        }

        return bestScore;
    }

    public override void Solve2()
    {
        var map = File.ReadLines(Input).Select(l => l.ToCharArray()).ToArray();

        Point FindPosition(char value)
        {
            for (int line = 0; line < map.Length; line++)
            {
                for (int column = 0; column < map[line].Length; column++)
                {
                    if (map.At((line, column)) == value)
                    {
                        return (line, column);
                    }
                }
            }

            throw new InvalidOperationException();
        }

        var startPosition = FindPosition('S');

        var fullPaths = new Dictionary<(Point, Direction), ImmutableHashSet<Point>>();
        var bestScore = FindBestPath2(map, startPosition, 0, new(), fullPaths);

        var endPosition = FindPosition('E');

        // There could have been multiple but it turns out there's only one, so let's not bother doing it right
        var path = fullPaths.Single(kvp => kvp.Key.Item1 == endPosition);

        Console.WriteLine(path.Value.Count);
    }

    private int FindBestPath2(char[][] map, Point startPosition, Direction startDirection, Dictionary<(Point, Direction), int> cache, Dictionary<(Point, Direction), ImmutableHashSet<Point>> fullPaths)
    {
        var queue = new Queue<(Point position, Direction direction, Point previousPosition, Direction previousDirection, int score)>();
        queue.Enqueue((startPosition, startDirection, default, 0, 0));

        int bestScore = int.MaxValue;

        while (queue.Count > 0)
        {
            var (position, direction, previousPosition, previousDirection, score) = queue.Dequeue();

            ref var previousScore = ref CollectionsMarshal.GetValueRefOrAddDefault(cache, (position, direction), out var exists);

            if (exists && previousScore < score)
            {
                // We've been here with a lower score, no point in exploring this branch
                continue;
            }

            ref var path = ref CollectionsMarshal.GetValueRefOrAddDefault(fullPaths, (position, direction), out exists);

            if (!fullPaths.TryGetValue((previousPosition, previousDirection), out var previousPath))
            {
                previousPath = [];
            }

            path = previousPath.Add(position);
            previousScore = score;

            if (map.At(position) == 'E')
            {
                if (score < bestScore)
                {
                    bestScore = score;
                }

                continue;
            }

            var newPosition = position + direction;

            if (newPosition.IsWithinBounds(map) && map.At(newPosition) != '#')
            {
                queue.Enqueue((newPosition, direction, position, direction, score + 1));
            }

            queue.Enqueue((position, direction.Left(), position, direction, score + 1000));
            queue.Enqueue((position, direction.Right(), position, direction, score + 1000));
        }

        return bestScore;
    }
}
