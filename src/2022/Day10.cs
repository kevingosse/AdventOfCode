using System.Text.RegularExpressions;

namespace AdventOfCode._2022;

internal class Day10 : Problem
{
    private List<int> _cycles;

    public override void Solve()
    {
        int register = 1;
        _cycles = new List<int> { register };
        
        foreach (var instruction in File.ReadLines(Input))
        {
            if (instruction == "noop")
            {
                _cycles.Add(register);
                continue;
            }

            var value = Regex.Match(instruction, @"addx (\-*\w+)").As<int>();

            _cycles.Add(register);

            register += value;

            _cycles.Add(register);
        }

        long result = 0;

        foreach (var cycle in new[] { 20, 60, 100, 140, 180, 220 })
        {
            result += cycle * _cycles[cycle - 1];
        }

        Console.WriteLine(result);
    }

    public override void Solve2()
    {
        int position = 0;

        foreach (var cycle in _cycles)
        {
            Console.Write(Math.Abs(cycle - position) <= 1 ? '#' : ' ');

            position = (position + 1) % 40;

            if (position == 0)
            {
                Console.WriteLine();
            }
        }
    }
}