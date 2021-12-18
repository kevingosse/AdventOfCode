using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2021
{
    internal class Day15 : Problem
    {
        public override void Solve()
        {
            var lines = File.ReadAllLines(Input);

            var matrix = new byte[lines.Length, lines[0].Length];

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    matrix[i, j] = (byte)(lines[i][j] - '0');
                }
            }

            var start = new Point(0, 0);
            var end = new Point(lines.Length - 1, lines[0].Length - 1);

            var queue = new Queue<Point>();

            var cameFrom = new Dictionary<Point, Point>();

            var gScore = new Dictionary<Point, int> { { start, 0 } };

            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                if (current == end)
                {
                    break;
                }

                foreach (var neighbor in GetNeighbors(current, matrix, lines.Length, lines[0].Length))
                {
                    var tentative_gscore = gScore[current] + matrix[neighbor.line, neighbor.column];

                    if (!gScore.ContainsKey(neighbor) || tentative_gscore < gScore[neighbor])
                    {
                        cameFrom[neighbor] = current;
                        gScore[neighbor] = tentative_gscore;

                        if (!queue.Contains(neighbor))
                        {
                            queue.Enqueue(neighbor);
                        }
                    }
                }
            }

            var c = end;

            int total = matrix[end.line, end.column];

            while (c != start)
            {
                c = cameFrom[c];
                total += matrix[c.line, c.column];
            }

            Console.WriteLine(total - matrix[start.line, start.column]);
        }

        private IEnumerable<Point> GetNeighbors(Point position, byte[,] matrix, int height, int width)
        {
            if (position.line > 0)
            {
                yield return position with { line = position.line - 1 };
            }

            if (position.line < height - 1)
            {
                yield return position with { line = position.line + 1 };
            }

            if (position.column > 0)
            {
                yield return position with { column = position.column - 1 };
            }

            if (position.column < height - 1)
            {
                yield return position with { column = position.column + 1 };
            }
        }

        private readonly record struct Point(int line, int column);

        public override void Solve2()
        {
            var lines = File.ReadAllLines(Input);

            int height = lines.Length * 5;
            int width = lines[0].Length * 5;

            var matrix = new byte[height, width];

            for (int x = 0; x < 5; x++)
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    for (int y = 0; y < 5; y++)
                    {
                        for (int j = 0; j < lines[0].Length; j++)
                        {
                            var value = (lines[i][j] - '0') + x + y;

                            if (value > 9)
                            {
                                value -= 9;
                            }

                            matrix[i + (x * lines.Length), j + (y * lines[0].Length)] = (byte)value;
                        }
                    }
                }
            }

            var start = new Point(0, 0);
            var end = new Point(height - 1, width - 1);

            var queue = new PriorityQueue<Point, int>();

            var cameFrom = new Dictionary<Point, Point>();

            var gScore = new Dictionary<Point, int> { { start, 0 } };

            int maxCount = 0;

            var elements = new HashSet<Point>();

            queue.Enqueue(start, 0);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                if (current == end)
                {
                    break;
                }

                elements.Remove(current);

                foreach (var neighbor in GetNeighbors(current, matrix, height, width))
                {
                    var tentative_gscore = gScore[current] + matrix[neighbor.line, neighbor.column];

                    if (!gScore.ContainsKey(neighbor) || tentative_gscore < gScore[neighbor])
                    {
                        cameFrom[neighbor] = current;
                        gScore[neighbor] = tentative_gscore;

                        if (elements.Add(neighbor))
                        {
                            queue.Enqueue(neighbor, tentative_gscore);

                            if (queue.Count > maxCount)
                            {
                                maxCount = queue.Count;
                            }
                        }
                    }
                }
            }

            int total = 0;

            var c = end;

            while (c != start)
            {
                total += matrix[c.line, c.column];
                c = cameFrom[c];
            }

            Console.WriteLine(total);
        }
    }
}