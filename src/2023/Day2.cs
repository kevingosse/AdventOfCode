using System.Text.RegularExpressions;

namespace AdventOfCode._2023;

internal class Day2 : Problem
{
    public override void Solve()
    {
        long result = 0;

        int maxRed = 12;
        int maxGreen = 13;
        int maxBlue = 14;

        var lines = File.ReadAllLines(Input);

        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            bool possible = true;

            var games = line.Split(':')[1].Split(';');

            foreach (var game in games)
            {
                var matches = Regex.Matches(game, @"(?<blue>\d+) blue|(?<red>\d+) red|(?<green>\d+) green");

                foreach (Match match in matches)
                {
                    if ((match.Groups["blue"].Success && match.Groups["blue"].Value.AsInt32() > maxBlue)
                        || (match.Groups["red"].Success && match.Groups["red"].Value.AsInt32() > maxRed)
                        || (match.Groups["green"].Success && match.Groups["green"].Value.AsInt32() > maxGreen))
                    {
                        possible = false;
                        break;
                    }
                }
            }

            if (possible)
            {
                result += i + 1;
            }
        }

        Console.WriteLine(result);
    }

    public override void Solve2()
    {
        long result = 0;

        foreach (var line in File.ReadLines(Input))
        {
            var games = line.Split(':')[1].Split(';');

            int maxRed = 0;
            int maxGreen = 0;
            int maxBlue = 0;

            foreach (var game in games)
            {
                var matches = Regex.Matches(game, @"(?<blue>\d+) blue|(?<red>\d+) red|(?<green>\d+) green");

                foreach (Match match in matches)
                {
                    if (match.TryGetInt32("blue", out var blue))
                    {
                        if (blue > maxBlue)
                        {
                            maxBlue = blue;
                        }
                    }
                    else if (match.TryGetInt32("red", out var red))
                    {
                        if (red > maxRed)
                        {
                            maxRed = red;
                        }
                    }
                    else if (match.TryGetInt32("green", out var green))
                    {
                        if (green > maxGreen)
                        {
                            maxGreen = green;
                        }
                    }
                }
            }

            result += maxBlue * maxRed * maxGreen;
        }

        Console.WriteLine(result);
    }
}