using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015
{
    internal class Day5 : Problem
    {
        public override void Solve()
        {
            int count = 0;

            foreach (var line in File.ReadLines(Input))
            {
                if (line.Contains("ab") || line.Contains("cd") || line.Contains("pq") || line.Contains("xy"))
                {
                    continue;
                }

                bool containsPair = false;
                int vowels = 0;

                for (int i = 0; i < line.Length; i++)
                {
                    var c = line[i];

                    if (c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u')
                    {
                        vowels++;
                    }

                    if (i > 0 && line[i - 1] == c)
                    {
                        containsPair = true;
                    }
                }

                if (vowels >= 3 && containsPair)
                {
                    count++;
                }
            }

            Console.WriteLine(count);
        }

        public override void Solve2()
        {
            int count = 0;

            foreach (var line in File.ReadLines(Input))
            {
                bool requirement1 = false;
                bool requirement2 = false;

                for (int i = 0; i < line.Length; i++)
                {
                    if (i >= 2 && line[i - 2] == line[i])
                    {
                        requirement1 = true;
                    }

                    if (i >= 1)
                    {
                        var pair = string.Concat(line[i - 1], line[i]);

                        var indexOf = line.LastIndexOf(pair);

                        if (indexOf != i - 1 && indexOf != i)
                        {
                            requirement2 = true;
                        }
                    }
                }

                if (requirement1 && requirement2)
                {
                    count++;
                }
            }

            Console.WriteLine(count);
        }
    }
}