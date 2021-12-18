using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015
{
    internal class Day3 : Problem
    {
        public override void Solve()
        {
            var directions = File.ReadAllText(Input);

            var visitedLocations = new HashSet<Point>();

            var offsets = new Dictionary<char, Point>()
            {
                {'>', new(0, 1) },
                {'<', new(0, -1) },
                {'^', new(-1, 0) },
                {'v', new(1, 0) }
            };

            var position = new Point(0, 0);

            visitedLocations.Add(position);

            foreach (var direction in directions)
            {
                var offset = offsets[direction];

                position = new Point(position.line + offset.line, position.column + offset.column);

                visitedLocations.Add(position);
            }

            Console.WriteLine(visitedLocations.Count);
        }

        private record struct Point(int line, int column);

        public override void Solve2()
        {
            var directions = File.ReadAllText(Input);

            var visitedLocations = new HashSet<Point>();

            var offsets = new Dictionary<char, Point>()
            {
                {'>', new(0, 1) },
                {'<', new(0, -1) },
                {'^', new(-1, 0) },
                {'v', new(1, 0) }
            };

            var positions = new Point[] { new(0, 0), new(0, 0) };

            visitedLocations.Add(new(0, 0));

            int order = 0;

            foreach (var direction in directions)
            {
                order = order == 0 ? 1 : 0;

                var (line, column) = offsets[direction];

                ref var position = ref positions[order];

                position = new Point(position.line + line, position.column + column);

                visitedLocations.Add(position);
            }

            Console.WriteLine(visitedLocations.Count);
        }
    }
}