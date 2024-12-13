using System.Text.RegularExpressions;

namespace AdventOfCode._2024;

internal class Day13 : Problem
{
    public override void Solve()
    {
        long result = 0;

        var lines = File.ReadAllLines(Input);

        for (int i = 0; i < lines.Length; i += 4)
        {
            var factors1 = Regex.Match(lines[i], @"Button A: X\+(\d+), Y\+(\d+)").As<int, int>();
            var factors2 = Regex.Match(lines[i + 1], @"Button B: X\+(\d+), Y\+(\d+)").As<int, int>();
            var targets = Regex.Match(lines[i + 2], @"Prize: X=(\d+), Y=(\d+)").As<int, int>();

            for (int j = 0; j < 100; j++)
            {
                var val1 = (targets.Item1 - factors2.Item1 * j) / factors1.Item1;
                var val2 = (targets.Item2 - factors2.Item2 * j) / factors1.Item2;

                if (val1 == val2
                    && (factors1.Item1 * val1 + factors2.Item1 * j == targets.Item1)
                    && (factors1.Item2 * val1 + factors2.Item2 * j == targets.Item2))
                {
                    result += 3 * val1 + j;
                    break;
                }
            }
        }

        Console.WriteLine(result);
    }

    public override void Solve2()
    {
        long result = 0;

        var lines = File.ReadAllLines(Input);

        for (int i = 0; i < lines.Length; i += 4)
        {
            var factors1 = Regex.Match(lines[i], @"Button A: X\+(\d+), Y\+(\d+)").As<decimal, decimal>();
            var factors2 = Regex.Match(lines[i + 1], @"Button B: X\+(\d+), Y\+(\d+)").As<decimal, decimal>();
            var targets = Regex.Match(lines[i + 2], @"Prize: X=(\d+), Y=(\d+)").As<decimal, decimal>();

            targets.Item1 += 10000000000000;
            targets.Item2 += 10000000000000;

            decimal b = (factors1.Item1 * targets.Item2 - factors1.Item2 * targets.Item1)
                / (factors1.Item1 * factors2.Item2 - factors2.Item1 * factors1.Item2);

            decimal a = (targets.Item1 - factors2.Item1 * b) / factors1.Item1;

            if (decimal.Floor(a) != a || decimal.Floor(b) != b)
            {
                continue;
            }

            result += 3 * (long)a + (long)b;
        }

        Console.WriteLine(result);
    }
}
