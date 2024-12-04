namespace AdventOfCode._2024;

internal class Day4 : Problem
{
    public override void Solve()
    {
        var input = File.ReadAllLines(Input);

        long result = 0;

        for (int line = 0; line < input.Length; line++)
        {
            for (int column = 0; column < input[line].Length; column++)
            {
                foreach (var offset in Offsets())
                {
                    if (FindWord(input, "XMAS", line, column, offset))
                    {
                        result++;
                    }
                }
            }
        }

        Console.WriteLine(result);
    }

    public override void Solve2()
    {
        var input = File.ReadAllLines(Input);

        long result = 0;

        for (int line = 1; line < input.Length - 1; line++)
        {
            for (int column = 1; column < input[line].Length - 1; column++)
            {
                if (FindXmas(input, line, column))
                {
                    result++;
                }
            }
        }

        Console.WriteLine(result);
    }

    private bool FindWord(string[] input, string target, int line, int column, (int line, int column) offset)
    {
        var length = target.Length - 1;

        if (line + length * offset.line < 0
            || line + length * offset.line >= input.Length
            || column + length * offset.column < 0
            || column + length * offset.column >= input[line].Length)
        {
            return false;
        }

        for (int i = 0; i < target.Length; i++)
        {
            if (input[line][column] != target[i])
            {
                return false;
            }

            line += offset.line;
            column += offset.column;
        }

        return true;
    }

    private bool FindXmas(string[] input, int line, int column)
    {
        if (input[line][column] != 'A')
        {
            return false;
        }

        var diagonal1 = (input[line - 1][column - 1], input[line + 1][column + 1]);
        var diagonal2 = (input[line - 1][column + 1], input[line + 1][column - 1]);

        return diagonal1.MinMax() == ('M', 'S') && diagonal2.MinMax() == ('M', 'S');
    }

    private IEnumerable<(int line, int column)> Offsets()
    {
        yield return (0, 1);
        yield return (1, 1);
        yield return (1, 0);
        yield return (1, -1);
        yield return (0, -1);
        yield return (-1, -1);
        yield return (-1, 0);
        yield return (-1, 1);
    }
}
