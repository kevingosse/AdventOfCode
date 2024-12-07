namespace AdventOfCode._2024;

internal class Day7 : Problem
{
    public override void Solve()
    {
        long result = 0;

        foreach (var line in File.ReadLines(Input))
        {
            var parts = line.Split(':');
            var expectedResult = long.Parse(parts[0]);
            var values = parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

            if (IsEquationSolvable(expectedResult, 0, values))
            {
                result += expectedResult;
            }
        }

        Console.WriteLine(result);
    }

    private bool IsEquationSolvable(long expectedResult, long currentResult, Span<int> values)
    {
        if (values.Length == 0)
        {
            return expectedResult == currentResult;
        }

        if (currentResult > expectedResult)
        {
            return false;
        }

        return IsEquationSolvable(expectedResult, currentResult + values[0], values[1..])
            || IsEquationSolvable(expectedResult, currentResult * values[0], values[1..]);
    }

    public override void Solve2()
    {
        long result = 0;

        foreach (var line in File.ReadLines(Input))
        {
            var parts = line.Split(':');
            var expectedResult = long.Parse(parts[0]);
            var values = parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

            if (IsEquationSolvableWithConcatenation(expectedResult, 0, values))
            {
                result += expectedResult;
            }
        }

        Console.WriteLine(result);
    }

    private bool IsEquationSolvableWithConcatenation(long expectedResult, long currentResult, Span<int> values)
    {
        if (values.Length == 0)
        {
            return expectedResult == currentResult;
        }

        if (currentResult > expectedResult)
        {
            return false;
        }

        return IsEquationSolvableWithConcatenation(expectedResult, currentResult + values[0], values[1..])
            || IsEquationSolvableWithConcatenation(expectedResult, currentResult * values[0], values[1..])
            || IsEquationSolvableWithConcatenation(expectedResult, long.Parse(currentResult.ToString() + values[0].ToString()), values[1..]);
    }
}
