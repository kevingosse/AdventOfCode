using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2021
{
    internal class Day14 : Problem
    {
        public override void Solve()
        {
            var lines = File.ReadAllLines(Input);

            var formulas = new Dictionary<string, string>();

            var polymer = lines[0];

            foreach (var line in lines.Skip(2))
            {
                var (start, end) = line.Split(" -> ");
                formulas.Add(start, end);
            }

            for (int step = 0; step < 10; step++)
            {
                var newPolymer = new StringBuilder();

                newPolymer.Append(polymer[0]);

                for (int i = 1; i < polymer.Length; i++)
                {
                    var pair = string.Concat(polymer[i - 1], polymer[i]);

                    newPolymer.Append(formulas[pair]);
                    newPolymer.Append(polymer[i]);
                }

                polymer = newPolymer.ToString();
            }

            var occurences = polymer.GroupBy(c => c)
                .Select(g => g.Count())
                .OrderByDescending(g => g)
                .ToList();

            Console.WriteLine(occurences.First() - occurences.Last());
        }

        public override void Solve2()
        {
            var lines = File.ReadAllLines(Input);

            var formulas = new Dictionary<string, char>();

            var polymer = lines[0];

            foreach (var line in lines.Skip(2))
            {
                var (start, end) = line.Split(" -> ");
                formulas.Add(start, end.Single());
            }

            var counts = new Dictionary<char, long>();
            var cache = new Cache();

            for (int i = 1; i < polymer.Length; i++)
            {
                var result = Polymerize(string.Concat(polymer[i - 1], polymer[i]), 39, formulas, cache);

                foreach (var kvp in result)
                {
                    CollectionsMarshal.GetValueRefOrAddDefault(counts, kvp.Key, out _) += kvp.Value;
                }
            }

            foreach (var c in polymer)
            {
                CollectionsMarshal.GetValueRefOrAddDefault(counts, c, out _)++;
            }

            var occurences = counts.Values
                .OrderByDescending(g => g)
                .ToList();

            Console.WriteLine(occurences.First() - occurences.Last());
        }

        private Dictionary<char, long> Polymerize(string polymer, int steps, Dictionary<string, char> formulas, Cache cache)
        {
            var result = formulas[polymer];

            if (steps == 0)
            {
                return new Dictionary<char, long> { { result, 1 } };
            }
            else if (cache.TryGetValue((polymer, steps), out var value))
            {
                return value;
            }
            else
            {
                var res1 = Polymerize(string.Concat(polymer[0], result), steps - 1, formulas, cache);
                var res2 = Polymerize(string.Concat(result, polymer[1]), steps - 1, formulas, cache);

                var counts = new Dictionary<char, long>(res1);

                foreach (var kvp in res2)
                {
                    CollectionsMarshal.GetValueRefOrAddDefault(counts, kvp.Key, out _) += kvp.Value;
                }

                CollectionsMarshal.GetValueRefOrAddDefault(counts, result, out _)++;

                cache.Add((polymer, steps), counts);

                return counts;
            }
        }

        private class Cache : Dictionary<(string pair, int steps), Dictionary<char, long>>
        {
        }
    }
}