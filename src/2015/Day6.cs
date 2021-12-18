using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2015
{
    internal class Day6 : Problem
    {
        public override void Solve()
        {
            var lights = new HashSet<Point>();

            foreach (var line in File.ReadLines(Input))
            {
                var match = Regex.Match(line, @"(?<verb>[\w ]+) (?<x1>[0-9]+),(?<y1>[0-9]+) through (?<x2>[0-9]+),(?<y2>[0-9]+)");

                var start = new Point(int.Parse(match.Groups["x1"].Value), int.Parse(match.Groups["y1"].Value));
                var end = new Point(int.Parse(match.Groups["x2"].Value), int.Parse(match.Groups["y2"].Value));
                var verb = match.Groups["verb"].Value;

                for (int x = start.x; x <= end.x; x++)
                {
                    for (int y = start.y; y <= end.y; y++)
                    {
                        var point = new Point(x, y);

                        switch (verb)
                        {
                            case "turn on":
                                lights.Add(point);
                                break;
                            case "turn off":
                                lights.Remove(point);
                                break;
                            case "toggle":
                                if (!lights.Add(point))
                                {
                                    lights.Remove(point);
                                }
                                break;
                        }
                    }
                }
            }

            Console.WriteLine(lights.Count);
        }

        private record struct Point(int x, int y);

        public override void Solve2()
        {
            var lights = new Dictionary<Point, int>();

            foreach (var line in File.ReadLines(Input))
            {
                var match = Regex.Match(line, @"(?<verb>[\w ]+) (?<x1>[0-9]+),(?<y1>[0-9]+) through (?<x2>[0-9]+),(?<y2>[0-9]+)");

                var start = new Point(int.Parse(match.Groups["x1"].Value), int.Parse(match.Groups["y1"].Value));
                var end = new Point(int.Parse(match.Groups["x2"].Value), int.Parse(match.Groups["y2"].Value));
                var verb = match.Groups["verb"].Value;

                for (int x = start.x; x <= end.x; x++)
                {
                    for (int y = start.y; y <= end.y; y++)
                    {
                        var point = new Point(x, y);

                        ref var value = ref CollectionsMarshal.GetValueRefOrAddDefault(lights, point, out _);

                        switch (verb)
                        {
                            case "turn on":
                                value++;
                                break;
                            case "turn off":
                                if (value > 0)
                                {
                                    value--;
                                }
                                break;
                            case "toggle":
                                value += 2;
                                break;
                        }
                    }
                }
            }

            Console.WriteLine(lights.Values.Sum());
        }
    }
}