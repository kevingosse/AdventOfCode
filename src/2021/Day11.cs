using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2021
{
    internal class Day11 : Problem
    {
        public override void Solve()
        {
            var matrix = ReadMatrix();

            int flashes = 0;

            for (int step = 0; step < 100; step++)
            {
                IncrementMatrix(matrix);

                bool stabilized = false;

                while (!stabilized)
                {
                    stabilized = true;

                    for (int l = 0; l < 10; l++)
                    {
                        for (int c = 0; c < 10; c++)
                        {
                            if (matrix[l, c] >= 10)
                            {
                                matrix[l, c] = -1;
                                IncrementNeighbors(matrix, l, c);
                                stabilized = false;
                                flashes++;
                            }
                        }
                    }
                }

                NormalizeMatrix(matrix);
            }

            Console.WriteLine(flashes);
        }

        public override void Solve2()
        {
            var matrix = ReadMatrix();

            int flashes = 0;

            int step = 0;

            while (true)
            {
                step++;
                IncrementMatrix(matrix);

                bool stabilized = false;

                while (!stabilized)
                {
                    stabilized = true;

                    for (int l = 0; l < 10; l++)
                    {
                        for (int c = 0; c < 10; c++)
                        {
                            if (matrix[l, c] >= 10)
                            {
                                matrix[l, c] = -1;
                                IncrementNeighbors(matrix, l, c);
                                stabilized = false;
                                flashes++;
                            }
                        }
                    }
                }

                if (NormalizeMatrix(matrix))
                {
                    Console.WriteLine(step);
                    return;
                }
            }
        }

        private bool NormalizeMatrix(int[,] matrix)
        {
            bool isSynchronized = true;

            for (int l = 0; l < 10; l++)
            {
                for (int c = 0; c < 10; c++)
                {
                    ref var value = ref matrix[l, c];

                    if (value == -1)
                    {
                        value = 0;
                    }
                    else
                    {
                        isSynchronized = false;
                    }
                }
            }

            return isSynchronized;
        }

        private void IncrementNeighbors(int[,] matrix, int line, int column)
        {
            for (int l = line - 1; l <= line + 1; l++)
            {
                for (int c = column - 1; c <= column + 1; c++)
                {
                    if (l < 0 || l >= 10)
                    {
                        continue;
                    }

                    if (c < 0 || c >= 10)
                    {
                        continue;
                    }

                    if (l == line && c == column)
                    {
                        continue;
                    }

                    ref var value = ref matrix[l, c];

                    if (value != -1)
                    {
                        value++;
                    }
                }
            }
        }

        private void IncrementMatrix(int[,] matrix)
        {
            for (int l = 0; l < 10; l++)
            {
                for (int c = 0; c < 10; c++)
                {
                    matrix[l, c]++;
                }
            }
        }

        private int[,] ReadMatrix()
        {
            var matrix = new int[10, 10];

            int l = 0;

            foreach (var line in File.ReadLines(Input))
            {
                for (int c = 0; c < line.Length; c++)
                {
                    matrix[l, c] = line[c] - '0';
                }

                l++;
            }

            return matrix;
        }
    }
}