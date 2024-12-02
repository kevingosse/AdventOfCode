namespace AdventOfCode._2024;

internal class Day2 : Problem
{
    public override void Solve()
    {
        int count = 0;

        foreach (var line in File.ReadLines(Input))
        {
            bool isSafe = true;

            var values = line.Split(' ').Select(int.Parse);

            bool? increasing = null;

            foreach (var (previousValue, value) in values.EnumerateWithPrevious())
            {
                if (Math.Abs(value - previousValue) is < 1 or > 3)
                {
                    isSafe = false;
                    break;
                }

                increasing ??= value > previousValue;

                if (increasing == true)
                {
                    if (value < previousValue)
                    {
                        isSafe = false;
                        break;
                    }
                }
                else
                {
                    if (value > previousValue)
                    {
                        isSafe = false;
                        break;
                    }
                }
            }

            if (isSafe)
            {
                count++;
            }
        }

        Console.WriteLine(count);
    }

    public override void Solve2()
    {
        int count = 0;

        static bool IsSafe(int previousValue, int value, bool increasing)
        {
            if (Math.Abs(value - previousValue) is < 1 or > 3)
            {
                return false;
            }

            if (increasing)
            {
                return value > previousValue;
            }

            return value < previousValue;
        }

        static bool AllSafe(int[] values, bool increasing)
        {
            for (int i = 0; i < values.Length - 1; i++)
            {
                if (!IsSafe(values[i], values[i + 1], increasing))
                {
                    return false;
                }
            }
            return true;
        }

        foreach (var line in File.ReadLines(Input))
        {
            var values = line.Split(' ').Select(int.Parse).ToArray();

            bool increasing = values[1] > values[0];

            if (AllSafe(values, increasing))
            {
                count++;
                continue;
            }

            // Can we make it safe by removing one value?
            // Yeah it's O(n²), no I'm too lazy to care
            for (int i = 0; i < values.Length; i++)
            {
                var newValues = values.Where((_, index) => index != i).ToArray();

                if (AllSafe(newValues, newValues[1] > newValues[0]))
                {
                    count++;
                    break;
                }
            }
        }

        Console.WriteLine(count);
    }
}
