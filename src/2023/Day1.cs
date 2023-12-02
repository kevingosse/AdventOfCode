namespace AdventOfCode._2023;

internal class Day1 : Problem
{
    private static readonly string[] Numbers = ["one", "two", "three", "four", "five", "six", "seven", "eight", "nine"];

    public override void Solve()
    {
        long sum = 0;

        foreach (var line in File.ReadLines(Input))
        {
            var digits = line.Where(char.IsDigit);
            sum += int.Parse($"{digits.First()}{digits.Last()}");
        }

        Console.WriteLine(sum);
    }

    public override void Solve2()
    {
        long sum = 0;

        foreach (var line in File.ReadLines(Input))
        {
            sum += $"{FindFirstNumber(line)}{FindLastNumber(line)}".AsInt32();
        }

        Console.WriteLine(sum);
    }

    private static string FindFirstNumber(string line)
    {
        for (int i = 0; i < line.Length; i++)
        {
            if (char.IsDigit(line[i]))
            {
                return line[i].ToString();
            }

            for (int j = 0; j < Numbers.Length; j++)
            {
                if (line[i..].StartsWith(Numbers[j]))
                {
                    return (j + 1).ToString();
                }
            }
        }

        throw new InvalidOperationException("Not found");
    }

    private static string FindLastNumber(string line)
    {
        for (int i = line.Length - 1; i >= 0; i--)
        {
            if (char.IsDigit(line[i]))
            {
                return line[i].ToString();
            }

            for (int j = 0; j < Numbers.Length; j++)
            {
                if (line[i..].StartsWith(Numbers[j]))
                {
                    return (j + 1).ToString();
                }
            }
        }

        throw new InvalidOperationException("Not found");
    }
}