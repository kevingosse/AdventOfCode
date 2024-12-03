using System.Text.RegularExpressions;

namespace AdventOfCode._2024;

internal class Day3 : Problem
{
    public override void Solve()
    {
        var input = File.ReadAllText(Input);
        long result = 0;

        foreach (Match match in Regex.Matches(input, @"mul\((\d+),(\d+)\)"))
        {
            var (op1, op2) = match.As<int, int>();
            result += op1 * op2;
        }

        Console.WriteLine(result);
    }

    public override void Solve2()
    {
        var input = File.ReadAllText(Input);
        
        long result = 0;
        bool enabled = true;

        foreach (Match match in Regex.Matches(input, @"mul\((\d+),(\d+)\)|(do\(\)|don't\(\))"))
        {
            if (match.Groups[3].Success)
            {
                enabled = match.Groups[3].Value == "do()";
                continue;
            }

            if (!enabled)
            {
                continue;
            }

            var (op1, op2) = match.As<int, int>();
            result += op1 * op2;
        }

        Console.WriteLine(result);
    }
}
