namespace AdventOfCode._2025;

internal class Day3 : Problem
{
    public override void Solve()
    {
        long result = 0;

        foreach (var bank in File.ReadLines(Input))
        {
            var (firstDigit, index) = bank[..^1].IndexOfMax();
            var secondDigit = bank[(index + 1)..].Max();

            result += (firstDigit - '0') * 10 + secondDigit - '0';
        }

        Console.WriteLine(result);
    }

    public override void Solve2()
    {
        long result = 0;

        foreach (var bank in File.ReadLines(Input))
        {
            int index = 0;

            for (int remainingDigits = 12; remainingDigits > 0; remainingDigits--)
            {
                var (digit, newIndex) = bank[index..^(remainingDigits - 1)].IndexOfMax();
                result += (digit - '0') * (long)Math.Pow(10, remainingDigits - 1);
                index += newIndex + 1;
            }
        }

        Console.WriteLine(result);
    }
}
