using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2016
{
    internal class Day2 : Problem
    {
        public override void Solve()
        {
            int digit = 5;
            var code = string.Empty;

            foreach (var line in File.ReadLines(Input))
            {
                foreach (var move in line)
                {
                    if (move == 'U')
                    {
                        if (digit > 3)
                        {
                            digit -= 3;
                        }
                    }
                    else if (move == 'D')
                    {
                        if (digit < 7)
                        {
                            digit += 3;
                        }
                    }
                    else if (move == 'R')
                    {
                        if (digit % 3 != 0)
                        {
                            digit++;
                        }
                    }
                    else if (move == 'L')
                    {
                        if (digit % 3 != 1)
                        {
                            digit--;
                        }
                    }
                }

                code += digit.ToString();
            }

            Console.WriteLine(code);
        }

        public override void Solve2()
        {
            var keypad = new[]
            {
                new[] { '0', '0', '1', '0', '0' },
                new[] { '0', '2', '3', '4', '0' },
                new[] { '5', '6', '7', '8', '9' },
                new[] { '0', 'A', 'B', 'C', '0' },
                new[] { '0', '0', 'D', '0', '0' }
            };

            var code = string.Empty;

            int line = 2;
            int column = 0;

            foreach (var instructions in File.ReadLines(Input))
            {
                foreach (var move in instructions)
                {
                    if (move == 'U')
                    {
                        if (line > 0 && keypad[line - 1][column] != '0')
                        {
                            line -= 1;
                        }
                    }
                    else if (move == 'D')
                    {
                        if (line < 4 && keypad[line + 1][column] != '0')
                        {
                            line += 1;
                        }
                    }
                    else if (move == 'R')
                    {
                        if (column < 4 && keypad[line][column + 1] != '0')
                        {
                            column += 1;
                        }
                    }
                    else if (move == 'L')
                    {
                        if (column > 0 && keypad[line][column - 1] != '0')
                        {
                            column -= 1;
                        }
                    }
                }

                code += keypad[line][column];
            }


            Console.WriteLine(code);
        }
    }
}
