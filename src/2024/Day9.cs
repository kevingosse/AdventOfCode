using System.Runtime.InteropServices;

namespace AdventOfCode._2024;

internal class Day9 : Problem
{
    public override void Solve()
    {
        var input = File.ReadAllText(Input);

        int sum = input.Sum(c => c - '0');

        // Build map
        var map = new List<short>(sum);
        short fileId = 0;

        for (int i = 0; i < input.Length; i++)
        {
            var c = input[i];
            var nbBlocks = c - '0';

            short value;

            if (i % 2 == 0)
            {
                value = fileId;
                fileId++;
            }
            else
            {
                value = -1;
            }

            for (int j = 0; j < nbBlocks; j++)
            {
                map.Add(value);
            }
        }

        // Compact
        int start = 0;
        int end = map.Count - 1;

        while (start < end)
        {
            if (map[end] == -1)
            {
                end--;
                continue;
            }

            if (map[start] == -1)
            {
                (map[start], map[end]) = (map[end], map[start]);
            }

            start++;
        }

        // Compute checksum
        var result = map.Select((value, index) => value == -1 ? 0L : index * value).Sum();

        Console.WriteLine(result);
    }

    public override void Solve2()
    {
        var input = File.ReadAllText(Input);

        int sum = input.Sum(c => c - '0');

        // Build map
        var files = new List<(int start, int length, short id)>();
        var freeList = new List<(int start, int length)>();
        short fileId = 0;
        int currentIndex = 0;
        
        for (int i = 0; i < input.Length; i++)
        {
            var nbBlocks = input[i] - '0';

            if (i % 2 == 0)
            {
                files.Add((currentIndex, nbBlocks, fileId));
                fileId++;
            }
            else
            {
                freeList.Add((currentIndex, nbBlocks));
            }

            currentIndex += nbBlocks;
        }

        // Compact
        for (int i = files.Count - 1; i >= 0; i--)
        {
            ref var file = ref CollectionsMarshal.AsSpan(files)[i];

            for (int j = 0; j < freeList.Count; j++)
            {
                ref var freeFile = ref CollectionsMarshal.AsSpan(freeList)[j];

                if (freeFile.start >= file.start)
                {
                    break;
                }

                if (file.length <= freeFile.length)
                {
                    file.start = freeFile.start;
                    freeFile.start += file.length;
                    freeFile.length -= file.length;
                }
            }
        }

        // Compute checksum
        long result = 0;

        foreach (var file in files.OrderBy(f => f.start))
        {
            for (int i = 0; i < file.length; i++)
            {
                result += (file.start + i) * file.id;
            }
        }

        Console.WriteLine(result);
    }
}
