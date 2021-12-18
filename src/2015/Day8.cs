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
    internal class Day8 : Problem
    {
        public override void Solve()
        {
            int total = 0;

            foreach (var line in File.ReadLines(Input))
            {
                var noQuotes = line.Substring(1, line.Length - 2);

                var noDoubleBackslashes = noQuotes.Replace(@"\\", @"\");

                var hexMatches = Regex.Matches(noQuotes, @"\\x[0-9a-f]{2}");

                var quotes = noDoubleBackslashes.Replace(@"\""", "\"");

                int sizeInMemory = quotes.Length - (hexMatches.Count * 3);

                total += line.Length - sizeInMemory;
            }

            Console.WriteLine(total);
        }

        public override void Solve2()
        {
            int total = 0;

            foreach (var line in File.ReadLines(Input))
            {
                var encoded1 = line.Replace(@"\", @"\\");
                var encoded2 = encoded1.Replace("\"", "\\\"");
                var encoded3 = "\"" + encoded2  + "\"";

                total += encoded3.Length - line.Length;
            }

            Console.WriteLine(total);
        }
    }
}