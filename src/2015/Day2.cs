using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015
{
    internal class Day2 : Problem
    {
        public override void Solve()
        {
            long total = 0;

            foreach (var line in File.ReadLines(Input))
            {
                var values = line.Split('x').Select(int.Parse).ToArray();

                var minSide = int.MaxValue;

                for (int i = 0; i < 3; i++)
                {
                    var side = values[i] * values[(i + 1) % 3];

                    if (side < minSide)
                    {
                        minSide = side;
                    }

                    total += side * 2;
                }

                total += minSide;
            }

            Console.WriteLine(total);
        }

        public override void Solve2()
        {
            long total = 0;

            foreach (var line in File.ReadLines(Input))
            {
                var values = line.Split('x').Select(int.Parse).ToArray();

                total += values.OrderBy(s => s).Take(2).Sum() * 2;
                total += values[0] * values[1] * values[2];
            }

            Console.WriteLine(total);
        }
    }
}