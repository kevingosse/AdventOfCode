namespace AdventOfCode._2024;

internal class Day25 : Problem
{
    public override void Solve()
    {
        var keys = new List<Item>();
        var locks = new List<Item>();

        foreach (var item in LoadItems())
        {
            if (item.IsKey)
            {
                keys.Add(item);
            }
            else
            {
                locks.Add(item);
            }
        }

        long result = 0;

        foreach (var k in keys)
        {
            foreach (var l in locks)
            {
                bool fits = true;

                for (int i = 0; i < 5; i++)
                {
                    if (k.Weights[i] + l.Weights[i] > 5)
                    {
                        fits = false;
                        break;
                    }
                }

                if (fits)
                {
                    result++;
                }
            }
        }

        Console.WriteLine(result);
    }

    private IEnumerable<Item> LoadItems()
    {
        using var reader = File.OpenText(Input);

        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            var item = new Item();

            if (line[0] == '.')
            {
                item.IsKey = true;
                Array.Fill(item.Weights, 5);
            }

            var weights = Enumerable.Repeat(0, 5).Select(_ => reader.ReadLine()!).ToArray();

            for (int i = 0; i < item.Weights.Length; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    var value = weights[j][i];

                    if (item.IsKey)
                    {
                        if (value == '#')
                        {
                            break;
                        }

                        item.Weights[i]--;
                    }
                    else
                    {
                        if (value == '.')
                        {
                            break;
                        }

                        item.Weights[i]++;
                    }                    
                }
            }

            reader.ReadLine();
            reader.ReadLine();

            yield return item;
        }
    }

    private struct Item
    {
        public bool IsKey;
        public int[] Weights = new int[5];

        public Item()
        {
        }
    }
}
