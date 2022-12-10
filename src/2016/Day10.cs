using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace AdventOfCode._2016;

internal class Day10 : Problem
{
    public override void Solve()
    {
        var bots = new Dictionary<int, Bot>();
        var outputs = new Dictionary<int, Bot>();

        foreach (var line in File.ReadLines(Input))
        {
            var match = Regex.Match(line, @"value (\d+) goes to bot (\d+)");

            if (match.Success)
            {
                var (value, botIndex) = match.As<int, int>();
                bots.GetOrAdd(botIndex).Add(value);
            }
            else
            {
                var (source, lowType, low, highType, high) = Regex.Match(line, @"bot (\d+) gives low to (\w+) (\d+) and high to (\w+) (\d+)")
                    .As<int, string, int, string, int>();

                var lowBot = (lowType == "output" ? outputs : bots).GetOrAdd(low);
                var highBot = (highType == "output" ? outputs : bots).GetOrAdd(high);

                var sourceBot = bots.GetOrAdd(source);

                sourceBot.Low = lowBot;
                sourceBot.High = highBot;
            }
        }

        while (true)
        {
            foreach (var (index, bot) in bots)
            {
                if (bot.Count != 2)
                {
                    continue;
                }

                if (bot.Contains(61) && bot.Contains(17))
                {
                    Console.WriteLine(index);
                    return;
                }

                bot.Low.Add(bot.Min());
                bot.High.Add(bot.Max());
                bot.Clear();
            }
        }
    }

    public override void Solve2()
    {
        var bots = new Dictionary<int, Bot>();
        var outputs = new Dictionary<int, Bot>();

        foreach (var line in File.ReadLines(Input))
        {
            var match = Regex.Match(line, @"value (\d+) goes to bot (\d+)");

            if (match.Success)
            {
                var (value, botIndex) = match.As<int, int>();
                bots.GetOrAdd(botIndex).Add(value);
            }
            else
            {
                var (source, lowType, low, highType, high) = Regex.Match(line, @"bot (\d+) gives low to (\w+) (\d+) and high to (\w+) (\d+)")
                    .As<int, string, int, string, int>();

                var lowBot = (lowType == "output" ? outputs : bots).GetOrAdd(low);
                var highBot = (highType == "output" ? outputs : bots).GetOrAdd(high);

                var sourceBot = bots.GetOrAdd(source);

                sourceBot.Low = lowBot;
                sourceBot.High = highBot;
            }
        }

        var output1 = outputs[0];
        var output2 = outputs[1];
        var output3 = outputs[2];

        while (true)
        {
            foreach (var bot in bots.Values)
            {
                if (bot.Count != 2)
                {
                    continue;
                }

                bot.Low.Add(bot.Min());
                bot.High.Add(bot.Max());
                bot.Clear();

                if (output1.Count > 0 && output2.Count > 0 && output3.Count > 0)
                {
                    Console.WriteLine(output1[0] * output2[0] * output3[0]);
                    return;
                }
            }
        }
    }

    private class Bot : List<int>
    {
        public Bot Low { get; set; }
        public Bot High { get; set; }
    }
}