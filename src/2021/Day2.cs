using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2021
{
    internal class Day2 : Problem
    {
        public override void Solve()
        {
            int horizontal = 0;
            int depth = 0;

            foreach (var line in File.ReadLines(Input))
            {
                var values = line.Split(' ');

                var order = values[0];
                var value = int.Parse(values[1]);

                switch (order)
                {
                    case "forward":
                        horizontal += value;
                        break;

                    case "down":
                        depth += value;
                        break;

                    case "up":
                        depth -= value;
                        break;

                    default:
                        throw new Exception(order);
                }
            }

            Console.WriteLine(horizontal * depth);
        }

        public override void Solve2()
        {
            int horizontal = 0;
            int depth = 0;
            int aim = 0;

            foreach (var line in File.ReadLines(Input))
            {
                var values = line.Split(' ');

                var order = values[0];
                var value = int.Parse(values[1]);

                switch (order)
                {
                    case "forward":
                        horizontal += value;
                        depth += aim * value;
                        break;

                    case "down":
                        aim += value;
                        break;

                    case "up":
                        aim -= value;
                        break;

                    default:
                        throw new Exception(order);
                }
            }

            Console.WriteLine(horizontal * depth);

        }
    }
}
