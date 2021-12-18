using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2021
{
    internal class Day5 : Problem
    {
        public override void Solve()
        {
            var lines = new List<(Point start, Point end)>();

            foreach (var line in File.ReadLines(Input))
            {
                var values = line.Split(" -> ");

                lines.Add((Point.Parse(values[0]), Point.Parse(values[1])));
            }

            var matrix = new int[1000, 1000];

            foreach (var line in lines)
            {
                DrawLine(line, matrix, false);
            }

            int count = 0;

            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    if (matrix[i, j] >= 2)
                    {
                        count++;
                    }
                }
            }

            Console.WriteLine(count);
        }

        public override void Solve2()
        {
            var lines = new List<(Point start, Point end)>();

            foreach (var line in File.ReadLines(Input))
            {
                var values = line.Split(" -> ");

                lines.Add((Point.Parse(values[0]), Point.Parse(values[1])));
            }

            var matrix = new int[1000, 1000];

            foreach (var line in lines)
            {
                DrawLine(line, matrix, true);
            }

            int count = 0;

            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    if (matrix[i, j] >= 2)
                    {
                        count++;
                    }
                }
            }

            Console.WriteLine(count);
        }

        private static void DrawLine((Point start, Point end) line, int[,] matrix, bool diagonals)
        {
            if (line.start.X == line.end.X)
            {
                // Vertical
                var start = line.start.Y;
                var end = line.end.Y;

                if (start > end)
                {
                    (start, end) = (end, start);
                }

                for (int i = start; i <= end; i++)
                {
                    matrix[line.start.X, i]++;
                }
            }
            else if (line.start.Y == line.end.Y)
            {
                // Horizontal
                var start = line.start.X;
                var end = line.end.X;

                if (start > end)
                {
                    (start, end) = (end, start);
                }

                for (int i = start; i <= end; i++)
                {
                    matrix[i, line.start.Y]++;
                }
            }
            else if (diagonals)
            {
                // Diagonal
                int offsetX = 0;

                if (line.start.X < line.end.X)
                {
                    offsetX = 1;
                }
                else if (line.start.X > line.end.X)
                {
                    offsetX = -1;
                }

                int offsetY = 0;

                if (line.start.Y < line.end.Y)
                {
                    offsetY = 1;
                }
                else if (line.start.Y > line.end.Y)
                {
                    offsetY = -1;
                }

                var start = line.start;

                do
                {
                    matrix[start.X, start.Y]++;

                    start.X += offsetX;
                    start.Y += offsetY;

                }
                while (start != line.end);

                matrix[start.X, start.Y]++;
            }
        }

        record struct Point(int X, int Y)
        {
            public static Point Parse(string text)
            {
                var values = text.Split(',');

                return new Point(int.Parse(values[0]), int.Parse(values[1]));
            }
        }

        
    }
}
