using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2021
{
    internal class Day13 : Problem
    {
        private const int MaxSize = 1400;

        public override void Solve()
        {
            var matrix = new byte[MaxSize, MaxSize];
            var instructions = new List<string>();

            bool matrixDefinition = true;

            foreach (var line in File.ReadLines(Input))
            {
                if (line.Length == 0)
                {
                    matrixDefinition = false;
                    continue;
                }

                if (matrixDefinition)
                {
                    var (x, y) = line.Split(',').Select(int.Parse);

                    matrix[x, y] = 1;
                }
                else
                {
                    instructions.Add(line.Replace("fold along ", string.Empty));
                }
            }

            var instruction = instructions.First();

            var (axis, rawOffset) = instruction.Split('=');
            var offset = int.Parse(rawOffset);

            var sizeX = MaxSize;
            var sizeY = MaxSize;

            if (axis == "x")
            {
                FoldX(matrix, MaxSize, MaxSize, offset);
                sizeX = offset;
            }

            int count = 0;

            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    if (matrix[i, j] == 1)
                    {
                        count++;
                    }
                }
            }

            Console.WriteLine(count);
        }

        private void FoldX(byte[,] matrix, int sizeX, int sizeY, int offset)
        {
            for (int i = 0; i < offset; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    if (matrix[i, j] == 1)
                    {
                        continue;
                    }

                    matrix[i, j] = matrix[offset + (offset - i), j];
                }
            }
        }

        private void FoldY(byte[,] matrix, int sizeX, int sizeY, int offset)
        {
            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < offset; j++)
                {
                    if (matrix[i, j] == 1)
                    {
                        continue;
                    }

                    matrix[i, j] = matrix[i, offset + (offset - j)];
                }
            }
        }

        public override void Solve2()
        {
            var matrix = new byte[MaxSize, MaxSize];
            var instructions = new List<string>();

            bool matrixDefinition = true;

            foreach (var line in File.ReadLines(Input))
            {
                if (line.Length == 0)
                {
                    matrixDefinition = false;
                    continue;
                }

                if (matrixDefinition)
                {
                    var (x, y) = line.Split(',').Select(int.Parse);

                    matrix[x, y] = 1;
                }
                else
                {
                    instructions.Add(line.Replace("fold along ", string.Empty));
                }
            }

            var sizeX = MaxSize;
            var sizeY = MaxSize;

            foreach (var instruction in instructions)
            {
                var (axis, rawOffset) = instruction.Split('=');
                var offset = int.Parse(rawOffset);                    

                if (axis == "x")
                {
                    FoldX(matrix, sizeX, sizeY, offset);
                    sizeX = offset;
                }
                else
                {
                    FoldY(matrix, sizeX, sizeY, offset);
                    sizeY = offset;
                }
            }

            for (int y = 0; y < sizeY; y++)
            {
                for (int x = 0; x < sizeX; x++)
                {
                    Console.Write(matrix[x, y] == 1 ? '▓' : ' ');
                }

                Console.WriteLine();
            }
        }
    }
}