using System.Text;

namespace AdventOfCode._2017;

internal class Day10 : Problem
{
    public override void Solve()
    {
        var list = Enumerable.Range(0, 256).ToArray();

        var input = File.ReadAllText(Input).Split(',').Select(int.Parse).ToArray();

        int skipSize = 0;
        int position = 0;

        foreach (var length in input)
        {
            Reverse(list, position, length);
            position = Modulo(position + length + skipSize, list.Length);
            skipSize++;
        }

        Console.WriteLine(list[0] * list[1]);
    }

    public override void Solve2()
    {
        var list = Enumerable.Range(0, 256).ToArray();

        var input = Encoding.ASCII.GetBytes(File.ReadAllText(Input))
            .Select(i => (int)i)
            .Concat([17, 31, 73, 47, 23])
            .ToArray();

        int skipSize = 0;
        int position = 0;

        for (int i = 0; i < 64; i++)
        {
            foreach (var length in input)
            {
                Reverse(list, position, length);
                position = Modulo(position + length + skipSize, list.Length);
                skipSize++;
            }
        }

        var denseHash = list.Chunk(16)
            .Select(chunk => chunk.Aggregate((a, b) => a ^ b))
            .ToArray();

        Console.WriteLine(string.Concat(denseHash.Select(v => v.ToString("x2"))));
    }

    private void Reverse(int[] list, int start, int length)
    {
        int end = start + length - 1;

        while (start < end)
        {
            var startIndex = Modulo(start, list.Length);
            var endIndex = Modulo(end, list.Length);

            (list[startIndex], list[endIndex]) = (list[endIndex], list[startIndex]);
            start++;
            end--;
        }
    }

    private int Modulo(int value, int modulo)
    {
        if (value >= 0)
        {
            return value % modulo;
        }

        while (value < 0)
        {
            value += modulo;
        }

        return value;
    }
}
