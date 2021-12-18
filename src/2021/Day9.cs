using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2021
{
    internal class Day9 : Problem
    {
        public override void Solve()
        {
            var values = File.ReadLines(Input).Select(l => l.Select(c => c - '0').ToArray()).ToList();

            int nbLines = values.Count;
            int nbColumns = values[0].Length;

            int risk = 0;

            for (int l = 0; l < nbLines; l++)
            {
                for (int c = 0; c < nbColumns; c++)
                {
                    var value = values[l][c];

                    bool isLowPoint = true;

                    if (l > 0 && values[l - 1][c] <= value)
                    {
                        isLowPoint = false;
                    }
                    else if (l < nbLines - 1 && values[l + 1][c] <= value)
                    {
                        isLowPoint = false;
                    }
                    else if (c > 0 && values[l][c - 1] <= value)
                    {
                        isLowPoint = false;
                    }
                    else if (c < nbColumns - 1 && values[l][c + 1] <= value)
                    {
                        isLowPoint = false;
                    }

                    if (isLowPoint)
                    {
                        risk += value + 1;
                    }
                }
            }

            Console.WriteLine(risk);
        }

        public override void Solve2()
        {
            var values = File.ReadLines(Input).Select(l => l.Select(c => c - '0').ToArray()).ToList();

            int nbLines = values.Count;
            int nbColumns = values[0].Length;

            var basins = new List<int>();

            for (int l = 0; l < nbLines; l++)
            {
                for (int c = 0; c < nbColumns; c++)
                {
                    var value = values[l][c];

                    if (value == 9 || value == -1)
                    {
                        continue;
                    }

                    basins.Add(FloodFill(values, l, c, nbLines, nbColumns));
                }
            }

            Console.WriteLine(basins.OrderByDescending(c => c).Take(3).Aggregate(1, (a, b) => a * b));
        }

        private int FloodFill(List<int[]> values, int l, int c, int nbLines, int nbColumns)
        {
            var queue = new Queue<(int line, int column)>();

            queue.Enqueue((l, c));

            int size = 0;

            while (queue.Count > 0)
            {
                var point = queue.Dequeue();

                ref var value = ref values[point.line][point.column];

                if (value == -1 || value == 9)
                {
                    continue;
                }

                value = -1;

                size++;

                if (point.line > 0)
                {
                    queue.Enqueue((point.line - 1, point.column));
                }

                if (point.line < nbLines - 1)
                {
                    queue.Enqueue((point.line + 1, point.column));
                }

                if (point.column > 0)
                {
                    queue.Enqueue((point.line, point.column - 1));
                }

                if (point.column < nbColumns - 1)
                {
                    queue.Enqueue((point.line, point.column + 1));
                }
            }

            return size;
        }
    }
}