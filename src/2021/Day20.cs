using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2021
{
    internal class Day20 : Problem
    {
        private string _algorithm;
        private char[,] _picture;

        public override void Solve()
        {
            var lines = File.ReadAllLines(Input);

            _algorithm = lines[0];

            var input = new List<string>(lines[2..]);

            _picture = new char[input.Count, input[0].Length];

            for (int line = 0; line < input.Count; line++)
            {
                for (int column = 0; column < input[line].Length; column++)
                {
                    _picture[line, column] = input[line][column];
                }
            }

            var result = ApplyAlgorithm(_picture, _algorithm, '.');

            result = ApplyAlgorithm(result, _algorithm, '#');

            Console.WriteLine(CountPixels(result));
        }

        private int CountPixels(char[,] picture)
        {
            int count = 0;

            for (int line = 0; line < picture.GetLength(0); line++)
            {
                for (int column = 0; column < picture.GetLength(1); column++)
                {
                    if (picture[line, column] == '#')
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        private char[,] ApplyAlgorithm(char[,] picture, string algorithm, char defaultValue)
        {
            var newPicture = new char[picture.GetLength(0) + 2, picture.GetLength(1) + 2];

            for (int line = 0; line < newPicture.GetLength(0); line++)
            {
                for (int column = 0; column < newPicture.GetLength(1); column++)
                {
                    var index = GetIndex(picture, line - 1, column - 1, defaultValue);

                    newPicture[line, column] = algorithm[index];
                }
            }

            return newPicture;
        }

        private int ToNumber(StringBuilder sb)
        {
            int result = 0;

            for (int i = 0; i < sb.Length; i++)
            {
                var nextBit = sb[i];

                if (nextBit == '.')
                {
                    continue;
                }

                result |= 1 << sb.Length - i - 1;
            }

            return result;
        }

        private int GetIndex(char[,] picture, int line, int column, char defaultValue)
        {
            var value = new StringBuilder(9);

            for (int l = line - 1; l <= line + 1; l++)
            {
                for (int c = column - 1; c <= column + 1; c++)
                {
                    value.Append(GetValue(picture, l, c, defaultValue));
                }
            }

            return ToNumber(value);
        }

        private char GetValue(char[,] picture, int line, int column, char defaultValue)
        {
            if (line < 0 || column < 0 || line >= picture.GetLength(0) || column >= picture.GetLength(1))
            {
                return defaultValue;
            }

            return picture[line, column];
        }

        public override void Solve2()
        {
            var result = _picture;

            for (int i = 0; i < 50; i++)
            {
                result = ApplyAlgorithm(result, _algorithm, i % 2 == 0 ? '.' : '#');
            }

            Console.WriteLine(CountPixels(result));
        }
    }
}