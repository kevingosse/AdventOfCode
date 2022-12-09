using Newtonsoft.Json.Linq;
using System.Data.Common;
using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode._2022
{
    internal class Day8 : Problem
    {
        private static readonly (int line, int column)[] Vectors =
        {
            (1, 0),
            (-1, 0),
            (0, 1),
            (0, -1)
        };

        public override void Solve()
        {
            var map = File.ReadLines(Input).Select(l => l.Select(c => c - '0').ToArray()).ToArray();

            int height = map.Length;
            int width = map[0].Length;

            int count = height * 2 + (width - 2) * 2;

            for (int line = 1; line < height - 1; line++)
            {
                for (int column = 1; column < width - 1; column++)
                {
                    if (Vectors.Any(v => CheckVisibility(map, line, column, v)))
                    {
                        count++;
                    }
                }
            }

            Console.WriteLine(count);
        }

        public override void Solve2()
        {
            var map = File.ReadLines(Input).Select(l => l.Select(c => c - '0').ToArray()).ToArray();

            int height = map.Length;
            int width = map[0].Length;

            int maxScore = 0;

            for (int line = 0; line < height; line++)
            {
                for (int column = 0; column < width; column++)
                {
                    if (line == 1 && column == 2)
                    {

                    }

                    int score = Vectors.Select(v => CountVisibilityFrom(map, line, column, v)).Aggregate((s1, s2) => s1 * s2);

                    if (score > maxScore)
                    {
                        maxScore = score;
                    }
                }
            }

            Console.WriteLine(maxScore);
        }

        private static bool CheckVisibility(int[][] map, int line, int column, (int line, int column) offset)
        {
            var value = map[line][column];

            line += offset.line;
            column += offset.column;

            while (line >= 0 && line < map.Length && column >= 0 && column < map[0].Length)
            {
                if (map[line][column] >= value)
                {
                    return false;
                }

                line += offset.line;
                column += offset.column;
            }

            return true;
        }

        private static int CountVisibilityFrom(int[][] map, int line, int column, (int line, int column) offset)
        {
            var value = map[line][column];

            line += offset.line;
            column += offset.column;

            int count = 0;

            while (line >= 0 && line < map.Length && column >= 0 && column < map[0].Length)
            {
                count += 1;

                if (map[line][column] >= value)
                {
                    break;
                }

                line += offset.line;
                column += offset.column;
            }

            return count;
        }
    }
}
