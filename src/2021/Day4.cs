using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2021
{
    internal class Day4 : Problem
    {
        public override void Solve()
        {
            var lines = File.ReadAllLines(Input);

            var inputs = lines[0].Split(',');

            int i = 2;

            var boards = new List<Board>();

            while (true)
            {
                if (i > lines.Length)
                {
                    break;
                }

                boards.Add(Board.Parse(lines.AsSpan().Slice(i, 5)));

                i += 6;
            }

            foreach (var input in inputs)
            {
                var value = int.Parse(input);

                foreach (var board in boards)
                {
                    var result = board.Check(value);

                    if (result != null)
                    {
                        Console.WriteLine(result);
                        return;
                    }
                }
            }
        }

        public override void Solve2()
        {
            var lines = File.ReadAllLines(Input);

            var inputs = lines[0].Split(',');

            int i = 2;

            var boards = new List<Board>();

            while (true)
            {
                if (i > lines.Length)
                {
                    break;
                }

                boards.Add(Board.Parse(lines.AsSpan().Slice(i, 5)));

                i += 6;
            }

            foreach (var input in inputs)
            {
                var value = int.Parse(input);
                
                foreach (var board in boards)
                {
                    if (board.IsComplete)
                    {
                        continue;
                    }

                    var result = board.Check(value);

                    if (result != null)
                    {
                        Console.WriteLine(result);
                    }
                }
            }

        }

        private class Board
        {
            private int?[,] _values = new int?[5, 5];

            public bool IsComplete { get; private set; }

            public static Board Parse(Span<string> lines)
            {
                var board = new Board();

                for (int l = 0; l < lines.Length; l++)
                {
                    var line = lines[l];

                    var values = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                    for (int c = 0; c < values.Length; c++)
                    {
                        board._values[l, c] = int.Parse(values[c]);
                    }
                }

                return board;
            }

            public int? Check(int value)
            {
                for (int l = 0; l < 5; l++)
                {
                    for (int c = 0; c < 5; c++)
                    {
                        if (_values[l, c] == value)
                        {
                            _values[l, c] = null;

                            if (Bingo())
                            {
                                IsComplete = true;
                                return Score(value);
                            }
                        }
                    }
                }

                return null;
            }

            public bool Bingo()
            {
                bool bingo = false;

                // Check rows
                for (int l = 0; l < 5; l++)
                {
                    bingo = true;

                    for (int c = 0; c < 5; c++)
                    {
                        if (_values[l, c] != null)
                        {
                            bingo = false;
                            break;
                        }
                    }

                    if (bingo)
                    {
                        return true;
                    }
                }

                // Check columns
                for (int c = 0; c < 5; c++)
                {
                    bingo = true;

                    for (int l = 0; l < 5; l++)
                    {
                        if (_values[l, c] != null)
                        {
                            bingo = false;
                            break;
                        }
                    }

                    if (bingo)
                    {
                        return true;
                    }
                }

                return false;
            }

            public int Score(int value)
            {
                int sum = 0;

                for (int l = 0; l < 5; l++)
                {
                    for (int c = 0; c < 5; c++)
                    {
                        sum += _values[l, c] ?? 0;
                    }
                }

                return sum * value;
            }
        }
    }
}
