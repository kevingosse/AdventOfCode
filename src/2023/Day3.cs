using System.Runtime.InteropServices;

namespace AdventOfCode._2023;

internal class Day3 : Problem
{
    public override void Solve()
    {
        long result = 0;

        var schematic = File.ReadAllLines(Input);

        for (int l = 0; l < schematic.Length; l++)
        {
            bool isPart = false;
            string number = "";

            var line = schematic[l];

            for (int c = 0; c < line.Length; c++)
            {
                if (char.IsDigit(line[c]))
                {
                    number += line[c];

                    if (!isPart)
                    {
                        isPart = GetAdjacentCases(schematic, l, c).Any(p => IsPart(p.value));
                    }
                }
                else
                {
                    if (number.Length > 0 && isPart)
                    {
                        result += number.AsInt32();
                    }

                    number = "";
                    isPart = false;
                }
            }

            if (number.Length > 0 && isPart)
            {
                result += number.AsInt32();
            }
        }

        Console.WriteLine(result);
    }

    public override void Solve2()
    {
        var gears = new Dictionary<(int line, int column), (int count, long ratio)>();

        var schematic = File.ReadAllLines(Input);

        void AddNumber(int value, IEnumerable<(int line, int column)> currentGears)
        {
            foreach (var gear in currentGears)
            {
                ref var values = ref CollectionsMarshal.GetValueRefOrAddDefault(gears, gear, out _);

                values.count++;

                if (values.count == 1)
                {
                    values.ratio = value;
                }
                else if (values.count == 2)
                {
                    values.ratio *= value;
                }
            }
        }

        for (int l = 0; l < schematic.Length; l++)
        {
            string number = "";
            var currentGears = new HashSet<(int line, int column)>();

            var line = schematic[l];

            for (int c = 0; c < line.Length; c++)
            {
                if (char.IsDigit(line[c]))
                {
                    number += line[c];

                    foreach (var (_, position) in GetAdjacentCases(schematic, l, c).Where(g => g.value == '*'))
                    {
                        currentGears.Add(position);
                    }
                }
                else
                {
                    if (number.Length > 0)
                    {
                        AddNumber(number.AsInt32(), currentGears);
                    }

                    number = "";
                    currentGears.Clear();
                }
            }

            if (number.Length > 0)
            {
                AddNumber(number.AsInt32(), currentGears);
            }
        }

        var result = gears.Values.Where(g => g.count == 2).Sum(g => g.ratio);

        Console.WriteLine(result);
    }

    private IEnumerable<(char value, (int line, int column) position)> GetAdjacentCases(string[] map, int line, int column)
    {
        for (int l = Math.Max(line - 1, 0); l <= Math.Min(map.Length - 1, line + 1); l++)
        {
            for (int c = Math.Max(column - 1, 0); c <= Math.Min(map[l].Length - 1, column + 1); c++)
            {
                yield return (map[l][c], (l, c));
            }
        }
    }

    private static bool IsPart(char c)
    {
        return !char.IsDigit(c) && c != '.';
    }
}