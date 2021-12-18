using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2021
{
    internal class Day3 : Problem
    {
        public override void Solve()
        {
            List<List<int>>? columns = null;

            foreach (var line in File.ReadLines(Input))
            {
                if (columns == null)
                {
                    columns = new List<List<int>>();

                    for (int i = 0; i < line.Length; i++)
                    {
                        columns.Add(new List<int>());
                    }
                }

                for (int i = 0; i < line.Length; i++)
                {
                    columns[i].Add(line[i] == '1' ? 1 : 0);
                }
            }

            string bits = "";

            foreach (var column in columns!)
            {
                var ones = column.Count(v => v == 1);

                if (ones > column.Count / 2)
                {
                    bits += "1";
                }
                else
                {
                    bits += "0";
                }
            }

            var gamma = Convert.ToUInt32(bits, 2);
            var epsilon = Convert.ToUInt32(new string(bits.Select(c => c == '1' ? '0' : '1').ToArray()), 2);

            Console.WriteLine($"{gamma} * {epsilon} = {gamma * epsilon}");

        }

        public override void Solve2()
        {
            List<List<char>>? columns = null;
            var numbers = new List<string>();

            foreach (var line in File.ReadLines(Input))
            {
                numbers.Add(line);

                if (columns == null)
                {
                    columns = new List<List<char>>();

                    for (int i = 0; i < line.Length; i++)
                    {
                        columns.Add(new List<char>());
                    }
                }

                for (int i = 0; i < line.Length; i++)
                {
                    columns[i].Add(line[i]);
                }
            }

            string bits = "";

            foreach (var column in columns!)
            {
                var ones = column.Count(v => v == '1');

                if (ones >= column.Count / 2)
                {
                    bits += "1";
                }
                else
                {
                    bits += "0";
                }
            }

            var rawOxygen = FindNumber(numbers, GetMoreFrequent, '1');
            var rawCo2 = FindNumber(numbers, GetLeastFrequent, '0');

            var oxygen = Convert.ToUInt32(rawOxygen, 2);
            var co2 = Convert.ToUInt32(rawCo2, 2);

            Console.WriteLine($"{oxygen} * {co2} = {oxygen * co2}");
        }

        private string FindNumber(List<string> numbers, SearchFunction search, char tie)
        {
            var length = numbers[0].Length;

            for (int i = 0; i < length; i++)
            {
                var bit = search(numbers, i, tie);

                numbers = numbers.Where(n => n[i] == bit).ToList();

                if (numbers.Count == 1)
                {
                    break;
                }
            }

            return numbers[0];
        }

        private delegate char SearchFunction(List<string> numbers, int column, char tie);

        private char GetLeastFrequent(List<string> numbers, int column, char tie)
        {
            var values = numbers.Select(n => n[column]).ToList();

            var ones = values.Count(v => v == '1');
            var zeros = values.Count - ones;

            if (ones > zeros)
            {
                return '0';
            }
            else if (ones < zeros)
            {
                return '1';
            }
            else
            {
                return tie;
            }
        }

        private char GetMoreFrequent(List<string> numbers, int column, char tie)
        {
            var values = numbers.Select(n => n[column]).ToList();

            var ones = values.Count(v => v == '1');
            var zeros = values.Count - ones;

            if (ones > zeros)
            {
                return '1';
            }
            else if (ones < zeros)
            {
                return '0';
            }
            else
            {
                return tie;
            }
        }
    }
}
