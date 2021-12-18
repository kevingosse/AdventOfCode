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
    internal class Day11 : Problem
    {
        public override void Solve()
        {
            var input = new StringBuilder("hxbxwxba");

            while (true)
            {
                if (MeetsRequirements(input))
                {
                    break;
                }

                IncrementString(input);
            }

            Console.WriteLine(input);
        }

        private static bool MeetsRequirements(StringBuilder sb)
        {
            bool firstRequirement = false;

            var pairs = new HashSet<string>();

            for (int i = 0; i < sb.Length; i++)
            {
                if (i >= 2 && sb[i - 1] == sb[i] - 1 && sb[i - 2] == sb[i] - 2)
                {
                    firstRequirement = true;
                }

                if (sb[i] == 'i' || sb[i] == 'o' || sb[i] == 'l')
                {
                    return false;
                }

                if (i >= 1 && sb[i] == sb[i - 1])
                {
                    pairs.Add(new string(sb[i], 2));
                }
            }

            return firstRequirement && pairs.Count >= 2;
        }

        private static void IncrementString(StringBuilder input)
        {
            int index = input.Length - 1;

            while (true)
            {
                if (input[index] == 'z')
                {
                    input[index] = 'a';
                    index--;
                }
                else
                {
                    input[index]++;
                    return;
                }
            }
        }

        public override void Solve2()
        {
            var input = new StringBuilder("hxbxwxba");

            int count = 0;

            while (count < 2)
            {
                IncrementString(input);

                if (MeetsRequirements(input))
                {
                    count++;
                }
            }

            Console.WriteLine(input);
        }
    }
}