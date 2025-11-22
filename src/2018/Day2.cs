namespace AdventOfCode._2018;

internal class Day2 : Problem
{
    public override void Solve()
    {
        int count2 = 0;
        int count3 = 0;

        foreach (var line in File.ReadLines(Input))
        {
            var groups = line.GroupBy(c => c)
                .Select(g => g.Count())
                .ToArray();

            if (groups.Any(g => g == 2))
            {
                count2++;
            }

            if (groups.Any(g => g == 3))
            {
                count3++;
            }
        }

        Console.WriteLine(count2 * count3);
    }

    public override void Solve2()
    {
        var ids = File.ReadAllLines(Input);

        for (int i = 0; i < ids.Length; i++)
        {
            for (int j = i + 1; j < ids.Length; j++)
            {
                if (CompareIds(ids[i], ids[j]))
                {
                    Console.WriteLine(string.Concat(ids[i].Zip(ids[j])
                        .Where(pair => pair.First == pair.Second)
                        .Select(pair => pair.First)));
                    return;
                }
            }
        }

        static bool CompareIds(string id1, string id2)
        {
            bool oneDifference = false;

            for (int i = 0; i < id1.Length; i++)
            {
                if (id1[i] != id2[i])
                {
                    if (oneDifference)
                    {
                        return false;
                    }

                    oneDifference = true;
                }
            }

            return oneDifference;
        }
    }
}
