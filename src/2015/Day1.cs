using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015
{
    internal class Day1 : Problem
    {
        public override void Solve()
        {
            var input = File.ReadAllText(Input);

            Console.WriteLine(input.Count(c => c == '(') - input.Count(c => c == ')'));
        }

        public override void Solve2()
        {
            var input = File.ReadAllText(Input);

            var level = 0;

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '(')
                {
                    level++;
                }
                else
                {
                    level--;
                }

                if (level == -1)
                {
                    Console.WriteLine(i + 1);
                    return;
                }
            }
        }
    }
}