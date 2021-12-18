using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2021
{
    internal class Day17 : Problem
    {
        public override void Solve()
        {
            var range = ParseInput();

            var negativeY = range.endY;

            if (negativeY > 0)
            {
                negativeY *= -1;
            }

            int maxHeight = 0;

            for (int x = 0; x < range.endX; x++)
            {
                for (int y = negativeY; y < 500; y++)
                {
                    if (ComputeTrajectory(range, x, y, out var height))
                    {
                        if (height > maxHeight)
                        {
                            maxHeight = height;
                        }
                    }
                }
            }

            Console.WriteLine(maxHeight);
        }

        private record struct Range(int startX, int endX, int startY, int endY);
        private record struct Position(int x, int y);

        private bool ComputeTrajectory(Range range, int x, int y, out int maxHeight)
        {
            maxHeight = y;

            bool IsInRange(Position position)
            {
                return position.x >= range.startX && position.x <= range.endX
                    && position.y >= range.startY && position.y <= range.endY;
            }

            bool found = false;

            var position = new Position(0, 0);

            while (true)
            {
                if (IsInRange(position))
                {
                    found = true;
                }

                if (position.y > maxHeight)
                {
                    maxHeight = position.y;
                }

                position.x += x;
                position.y += y;

                if (x > 0)
                {
                    x--;
                }
                else if (x < 0)
                {
                    x++;
                }

                y--;

                if (position.x > range.endX)
                {
                    break;
                }

                if( position.x < range.startX && x == 0)
                {
                    break;
                }

                if (y < 0 && position.y < range.startY)
                {
                    break;
                }
            }

            return found;
        }

        private Range ParseInput()
        {
            var input = File.ReadAllText(Input);

            input = input.Replace("target area: x=", "");

            var values = input.Split(", ");

            var valX = values[0].Split("..");

            var startX = int.Parse(valX[0]);
            var endX = int.Parse(valX[1]);

            var valY = values[1].Replace("y=", "").Split("..");

            var startY = int.Parse(valY[0]);
            var endY = int.Parse(valY[1]);

            return new Range(startX, endX, startY, endY);
        }

        private record struct Point(int x, int y);

        public override void Solve2()
        {
            var range = ParseInput();

            var negativeY = range.endY;

            if (negativeY > 0)
            {
                negativeY *= -1;
            }

            int count = 0;

            for (int x = 0; x <= range.endX; x++)
            {
                for (int y = -500; y < 500; y++)
                {
                    if (ComputeTrajectory(range, x, y, out _))
                    {
                        count++;
                    }
                }
            }

            Console.WriteLine(count);

        }
    }
}