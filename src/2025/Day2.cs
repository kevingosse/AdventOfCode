namespace AdventOfCode._2025;

internal class Day2 : Problem
{
    public override void Solve()
    {
        var ranges = File.ReadAllText(Input)
            .Split(',')
            .Select(v => v.Match(@"(\d+)\-(\d+)").As<long, long>());

        long result = 0;

        foreach (var (start, end) in ranges)
        {
            for (long i = start; i <= end; i++)
            {
                var str = i.ToString();

                if (str.Length % 2 != 0)
                {
                    continue;
                }

                if (str[..(str.Length / 2)].Equals(str[(str.Length / 2)..]))
                {
                    result += i;
                }
            }
        }

        Console.WriteLine(result);
    }

    public override void Solve2()
    {
        var ranges = File.ReadAllText(Input)
            .Split(',')
            .Select(v => v.Match(@"(\d+)\-(\d+)").As<long, long>());

        long result = 0;

        foreach (var (start, end) in ranges)
        {
            for (long i = start; i <= end; i++)
            {
                var str = i.ToString();

                for (int patternLength = 1; patternLength <= str.Length / 2; patternLength++)
                {
                    if (str.Length % patternLength != 0)
                    {
                        continue;
                    }

                    var pattern = str[..patternLength];

                    bool isValid = true;

                    for (int j = patternLength; j < str.Length; j+= patternLength)
                    {
                        if (!str[j..(j+patternLength)].Equals(pattern))
                        {
                            isValid = false;
                            break;
                        }
                    }

                    if (isValid)
                    {
                        result += i;
                        break;
                    }
                }
     
            }
        }

        Console.WriteLine(result);
    }
}
