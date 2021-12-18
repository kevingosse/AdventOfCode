using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2015
{
    internal class Day10 : Problem
    {
        public override void Solve()
        {
            var input = "1113222113";

            for (int step = 0; step < 40; step++)
            {
                var result = new StringBuilder();

                char? previous = null;
                int count = 0;

                foreach (var c in input)
                {
                    if (previous == null || c == previous)
                    {
                        previous = c;
                        count++;
                        continue;
                    }

                    result.Append(count);
                    result.Append(previous);
                    count = 1;
                    previous = c;
                }

                result.Append(count);
                result.Append(previous);

                input = result.ToString();
            }

            Console.WriteLine(input.Length);
        }

        public override void Solve2()
        {
            var input = "1113222113";

            for (int step = 0; step < 50; step++)
            {
                var result = new StringBuilder();

                char? previous = null;
                int count = 0;

                foreach (var c in input)
                {
                    if (previous == null || c == previous)
                    {
                        previous = c;
                        count++;
                        continue;
                    }

                    result.Append(count);
                    result.Append(previous);
                    count = 1;
                    previous = c;
                }

                result.Append(count);
                result.Append(previous);

                input = result.ToString();
            }

            Console.WriteLine(input.Length);
        }
    }
}