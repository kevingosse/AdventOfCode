namespace AdventOfCode._2024;

internal class Day20 : Problem
{
    public override void Solve()
    {
        var map = File.ReadAllLines(Input);
        var scoreMap = new int[map.Length][];

        Point start = default;
        Point end = default;

        for (int i = 0; i < scoreMap.Length; i++)
        {
            scoreMap[i] = map[i].Select(c => c == '#' ? -1 : int.MaxValue).ToArray();

            var indexOfStart = map[i].IndexOf('S');

            if (indexOfStart > -1)
            {
                start = (i, indexOfStart);
            }

            var indexOfEnd = map[i].IndexOf('E');

            if (indexOfEnd > -1)
            {
                end = (i, indexOfEnd);
            }
        }

        FindBestPath(scoreMap, end);

        var result = FindShortcuts(scoreMap);
        Console.WriteLine(result);
    }

    private void FindBestPath(int[][] map, Point start)
    {
        var queue = new Queue<(Point position, int score)>();
        queue.Enqueue((start, 0));

        while (queue.Count > 0)
        {
            var (position, score) = queue.Dequeue();

            ref var previousScore = ref map.At(position);

            if (previousScore <= score)
            {
                // We've been here with a lower score, no point in exploring this branch
                continue;
            }

            previousScore = score;

            foreach (var offset in Point.Offsets)
            {
                var newPosition = position + offset;

                if (newPosition.IsWithinBounds(map) && map.At(newPosition) != -1)
                {
                    queue.Enqueue((newPosition, score + 1));
                }
            }
        }
    }

    private long FindShortcuts(int[][] map)
    {
        long result = 0;

        for (int line = 0; line < map.Length; line++)
        {
            for (int column = 0; column < map[line].Length; column++)
            {
                if (map.At((line, column)) != -1)
                {
                    result += FindShortcuts(map, (line, column));
                }
            }
        }

        return result;
    }

    private long FindShortcuts(int[][] map, Point position)
    {
        long result = 0;

        foreach (var offset in Point.GetOffsets())
        {
            var start = position + offset;

            if (start.IsWithinBounds(map) && map.At(start) == -1)
            {
                foreach (var offset2 in Point.GetOffsets())
                {
                    var end = start + offset2;

                    if (position != end && end.IsWithinBounds(map) && map.At(end) != -1)
                    {
                        // The shortcut is valid, how much do we save?
                        var score = map.At(position) - map.At(end) - 2;

                        if (score >= 100)
                        {
                            result++;
                        }
                    }
                }
            }
        }

        return result;
    }

    public override void Solve2()
    {
        var map = File.ReadAllLines(Input);
        var scoreMap = new int[map.Length][];

        Point start = default;
        Point end = default;

        for (int i = 0; i < scoreMap.Length; i++)
        {
            scoreMap[i] = map[i].Select(c => c == '#' ? -1 : int.MaxValue).ToArray();

            var indexOfStart = map[i].IndexOf('S');

            if (indexOfStart > -1)
            {
                start = (i, indexOfStart);
            }

            var indexOfEnd = map[i].IndexOf('E');

            if (indexOfEnd > -1)
            {
                end = (i, indexOfEnd);
            }
        }

        FindBestPath(scoreMap, end);

        var shortcuts = new HashSet<(Point, Point)>();

        for (int line = 0; line < scoreMap.Length; line++)
        {
            for (int column = 0; column < scoreMap[line].Length; column++)
            {
                foreach (var shortcut in FindShortcuts2(scoreMap, (line, column)))
                {
                    shortcuts.Add(shortcut);
                }
            }
        }

        Console.WriteLine(shortcuts.Count);
    }

    private IEnumerable<(Point, Point)> FindShortcuts2(int[][] map, Point start)
    {
        const int targetScore = 100;

        if (map.At(start) == -1)
        {
            yield break;
        }

        var queue = new Queue<(Point position, int spentTime)>();

        foreach (var offset in Point.Offsets)
        {
            var shortcutStart = start + offset;

            if (!shortcutStart.IsWithinBounds(map))
            {
                continue;
            }

            var path = new Dictionary<Point, int>();
            queue.Clear();

            queue.Enqueue((shortcutStart, 1));

            while (queue.Count > 0)
            {
                var (position, spentTime) = queue.Dequeue();

                if (!position.IsWithinBounds(map))
                {
                    continue;
                }

                if (map.At(position) != -1)
                {
                    var score = map.At(start) - map.At(position) - spentTime;

                    if (score >= targetScore)
                    {
                        yield return (start, position);
                    }
                }

                if (spentTime == 20)
                {
                    continue;
                }

                if (path.TryGetValue(position, out var minSpentTime))
                {
                    if (minSpentTime <= spentTime)
                    {
                        // We've already been there with similar or worst remaining time
                        continue;
                    }
                }

                path[position] = spentTime;

                foreach (var direction in Point.Offsets)
                {
                    queue.Enqueue((position + direction, spentTime + 1));
                }
            }
        }
    }
}
