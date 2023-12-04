namespace AdventOfCode._2023;

internal class Day4 : Problem
{
    public override void Solve()
    {
        long result = 0;

        foreach (var line in File.ReadLines(Input))
        {
            var score = 0;

            var parts = line.Split(':')[1].Split('|');

            var winningNumbers = parts[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);
            var numbers = new HashSet<int>(parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse));

            foreach (var number in winningNumbers)
            {
                if (numbers.Contains(number))
                {
                    score = score == 0 ? 1 : score * 2;
                }
            }

            result += score;
        }

        Console.WriteLine(result);
    }

    public override void Solve2()
    {
        var cardValues = new List<int>();

        foreach (var line in File.ReadLines(Input))
        {
            var parts = line.Split(':')[1].Split('|');

            var winningNumbers = parts[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);
            var numbers = new HashSet<int>(parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse));

            cardValues.Add(winningNumbers.Count(numbers.Contains));
        }

        var cardCounts = new int[cardValues.Count];
        Array.Fill(cardCounts, 1);

        for (int i = 0; i < cardCounts.Length; i++)
        {
            for (int j = 1; j <= cardValues[i]; j++)
            {
                cardCounts[i + j] += cardCounts[i];
            }
        }

        Console.WriteLine(cardCounts.Sum());
    }
}