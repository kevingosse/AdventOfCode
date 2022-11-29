using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2016
{
    internal class Day1 : Problem
    {
        private static readonly (int line, int column)[] Offsets =
        {
            (1, 0),
            (0, 1),
            (-1, 0),
            (0, -1)
        };

        public override void Solve()
        {
            var values = File.ReadAllText(Input).Split(", ");

            (int line, int column) position = (0, 0);

            int currentDirection = 0;

            foreach (var value in values)
            {
                var direction = value[0];
                var steps = int.Parse(value[1..]);

                currentDirection = (currentDirection + (direction == 'R' ? 1 : -1)) % Offsets.Length;

                if (currentDirection < 0)
                {
                    currentDirection = Offsets.Length - 1;
                }

                position.line += Offsets[currentDirection].line * steps;
                position.column += Offsets[currentDirection].column * steps;
            }

            Console.WriteLine(Math.Abs(position.line) + Math.Abs(position.column));
        }

        public override void Solve2()
        {
            var values = File.ReadAllText(Input).Split(", ");

            (int line, int column) position = (0, 0);

            var path = new HashSet<(int line, int column)>();

            int currentDirection = 0;

            foreach (var value in values)
            {
                var direction = value[0];
                var steps = int.Parse(value[1..]);

                currentDirection = (currentDirection + (direction == 'R' ? 1 : -1)) % Offsets.Length;

                if (currentDirection < 0)
                {
                    currentDirection = Offsets.Length - 1;
                }

                for (int i = 0; i < steps; i++)
                {
                    position.line += Offsets[currentDirection].line;
                    position.column += Offsets[currentDirection].column;

                    if (!path.Add(position))
                    {
                        Console.WriteLine(Math.Abs(position.line) + Math.Abs(position.column));
                        return;
                    }
                }
            }

        }
    }
}
