using System.Globalization;
using System.Text.RegularExpressions;

namespace AdventOfCode._2018;

internal class Day4 : Problem
{
    public override void Solve()
    {
        var lines = File.ReadLines(Input)
            .Select(l => l.Split(']'))
            .Select(v => (Date: DateTime.ParseExact(v[0], "[yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), Item: v[1].Trim()))
            .OrderBy(v => v.Date);

        var allIntervals = new Dictionary<int, int[]>();

        int currentGuard = 0;
        int start = 0;

        foreach (var line in lines)
        {
            if (line.Item.StartsWith("Guard"))
            {
                currentGuard = Regex.Match(line.Item, @"Guard #(?<id>\d+)").As<int>();
                continue;
            }

            var intervals = allIntervals.GetOrAdd(currentGuard, _ => new int[60]);

            if (line.Item.StartsWith("wakes"))
            {
                for (int i = start; i < line.Date.Minute; i++)
                {
                    ++intervals[i];
                }
            }
            else
            {
                start = line.Date.Minute;
            }
        }

        var maxGuard = allIntervals
            .OrderByDescending(kvp => kvp.Value.Sum())
            .First();

        var maxMinute = maxGuard.Value.IndexOfMax().Index;

        Console.WriteLine(maxGuard.Key * maxMinute);
    }

    public override void Solve2()
    {
        var lines = File.ReadLines(Input)
            .Select(l => l.Split(']'))
            .Select(v => (Date: DateTime.ParseExact(v[0], "[yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), Item: v[1].Trim()))
            .OrderBy(v => v.Date);

        var allIntervals = new Dictionary<int, int[]>();

        int currentGuard = 0;
        int start = 0;
        (int Guard, int Minute, int Value) max = default;

        foreach (var line in lines)
        {
            if (line.Item.StartsWith("Guard"))
            {
                currentGuard = Regex.Match(line.Item, @"Guard #(?<id>\d+)").As<int>();
                continue;
            }

            var intervals = allIntervals.GetOrAdd(currentGuard, _ => new int[60]);

            if (line.Item.StartsWith("wakes"))
            {
                for (int i = start; i < line.Date.Minute; i++)
                {
                    var value = ++intervals[i];

                    if (value > max.Value)
                    {
                        max = (currentGuard, i, value);
                    }
                }
            }
            else
            {
                start = line.Date.Minute;
            }
        }

        Console.WriteLine(max.Guard * max.Minute);
    }
}
