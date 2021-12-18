using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2021
{
    internal class Day8 : Problem
    {
        public override void Solve()
        {
            var expectedLengthes = new[] { 2, 4, 3, 7 };

            int count = 0;

            foreach (var line in File.ReadLines(Input))
            {
                var values = line.Split('|')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

                foreach (var value in values)
                {
                    if (expectedLengthes.Contains(value.Length))
                    {
                        count++;
                    }
                }
            }

            Console.WriteLine(count);
        }

        public override void Solve2()
        {
            int sum = 0;

            foreach (var line in File.ReadLines(Input))
            {
                var fragments = line.Split('|');

                var mappings = fragments[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var values = fragments[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

                sum += GetOutput2(mappings, values);
            }

            Console.WriteLine(sum);
        }

        [DebuggerDisplay("{IsSolved} - {Mapping}")]
        private class SolvedMapping
        {
            public string Mapping { get; set; }
            public bool IsSolved { get; set; }
        }

        private int GetOutput2(string[] mappings, string[] values)
        {
            var digits = BuildDigits();

            var permutations = new Dictionary<char, List<char>>();

            var allValues = mappings.Concat(values).ToArray();

            var solvedMappings = mappings.Select(m => new SolvedMapping {  Mapping = m }).ToList();

            var passes = new Dictionary<int, Solver>[]
            {
                new()
                {
                    { 1, IsOne },
                    { 4, IsFour },
                    { 7, IsSeven },
                    { 8, IsEight }
                },

                new()
                {
                    { 0, IsZero },
                    { 3, IsThree },
                    { 6, IsSix },
                    { 9, IsNine }
                },

                new()
                {
                    { 5, IsFive },
                },

                new()
                {
                    { 2, IsTwo },
                }
            };


            var solvedDigits = new Digit[10];

            foreach (var pass in passes)
            {
                foreach (var solver in pass)
                {
                    foreach (var value in solvedMappings)
                    {
                        if (value.IsSolved)
                        {
                            continue;
                        }

                        if (solver.Value(value.Mapping, solvedDigits))
                        {
                            solvedDigits[solver.Key] = new Digit(solver.Key, value.Mapping);
                            value.IsSolved = true;
                            break;
                        }
                    }
                }
            }

            var result = "";

            foreach (var value in values)
            {
                var digit = solvedDigits.Single(d => d.Segments.Length == value.Length && d.Segments.Intersect(value).Count() == d.Segments.Length);

                result += digit.Value;
            }

            return int.Parse(result);
        }

        private delegate bool Solver(string value, Digit[] solvedDigits);

        private bool HasDigitsFrom(int digit, string value, Digit[] solvedDigits)
        {
            if (solvedDigits[digit] == null)
            {
                return false;
            }

            return solvedDigits[digit].Segments.Intersect(value).Count() == solvedDigits[digit].Segments.Length;
        }

        private bool HasSomeDigitsFrom(int digit, string value, Digit[] solvedDigits, int expectedNumberOfDigits)
        {
            if (solvedDigits[digit] == null)
            {
                return false;
            }

            return solvedDigits[digit].Segments.Intersect(value).Count() == expectedNumberOfDigits;
        }

        // 1 - 4 - 7 - 8

        private bool IsZero(string value, Digit[] solvedDigits)
        {
            return value.Length == 6
                && HasDigitsFrom(1, value, solvedDigits)
                && !HasDigitsFrom(4, value, solvedDigits)
                && HasDigitsFrom(7, value, solvedDigits);
        }

        private bool IsOne(string value, Digit[] solvedDigits)
        {
            return value.Length == 2;
        }

        private bool IsTwo(string value, Digit[] solvedDigits)
        {
            return value.Length == 5
                && !HasDigitsFrom(1, value, solvedDigits)
                && !HasDigitsFrom(4, value, solvedDigits)
                && HasSomeDigitsFrom(7, value, solvedDigits, 2);
        }

        private bool IsThree(string value, Digit[] solvedDigits)
        {
            return value.Length == 5
                && HasDigitsFrom(1, value, solvedDigits)
                && !HasDigitsFrom(4, value, solvedDigits)
                && HasDigitsFrom(7, value, solvedDigits);
        }

        private bool IsFour(string value, Digit[] solvedDigits)
        {
            return value.Length == 4;
        }

        private bool IsFive(string value, Digit[] solvedDigits)
        {
            return value.Length == 5
            //    && !HasDigitsFrom(1, value, solvedDigits)
            //    && !HasDigitsFrom(4, value, solvedDigits)
                && HasSomeDigitsFrom(9, value, solvedDigits, 5);
        }

        private bool IsSix(string value, Digit[] solvedDigits)
        {
            return value.Length == 6
                && !HasDigitsFrom(1, value, solvedDigits)
                && !HasDigitsFrom(4, value, solvedDigits)
                && !HasDigitsFrom(9, value, solvedDigits);
        }

        private bool IsSeven(string value, Digit[] solvedDigits)
        {
            return value.Length == 3;
        }

        private bool IsEight(string value, Digit[] solvedDigits)
        {
            return value.Length == 7;
        }

        private bool IsNine(string value, Digit[] solvedDigits)
        {
            return value.Length == 6
                && HasDigitsFrom(1, value, solvedDigits)
                && HasDigitsFrom(4, value, solvedDigits)
                && HasDigitsFrom(7, value, solvedDigits);
        }

        private int GetOutput(string[] mappings, string[] values)
        {
            var digits = BuildDigits();

            var permutations = new Dictionary<char, List<char>>();

            var allValues = mappings.Concat(values).ToArray();

            bool madeProgress = true;

            while (madeProgress)
            {
                madeProgress = false;

                foreach (var value in allValues)
                {
                    var matchingDigits = digits.Where(d => d.Segments.Length == value.Length).ToList();

                    if (matchingDigits.Count > 1)
                    {
                        for (int i = matchingDigits.Count - 1; i >= 0; i--)
                        {
                            var match = matchingDigits[i];

                            // Check if match makes sense
                            var validPermutations = permutations.Where(p => value.Contains(p.Key) && p.Value != null).ToList();

                            bool isValid = true;

                            foreach (var vp in validPermutations)
                            {
                                if (match.Segments.Intersect(vp.Value).Count() != vp.Value.Count)
                                {
                                    isValid = false;
                                    break;
                                }
                            }

                            if (!isValid)
                            {
                                matchingDigits.RemoveAt(i);
                            }
                        }
                    }

                    if (matchingDigits.Count == 1)
                    {
                        madeProgress = true;

                        var match = matchingDigits.Single();

                        foreach (var c in value)
                        {
                            if (!permutations.TryGetValue(c, out var permutation))
                            {
                                permutations.Add(c, new List<char>(match.Segments));
                            }
                            else
                            {
                                permutations[c] = permutation.Intersect(match.Segments).ToList();
                            }
                        }
                    }
                }

            }


            var sum = 0;

            foreach (var value in values)
            {
                var permuted = new string(value.Select(s => permutations[s].Single()).OrderBy(s => s).ToArray());

                var digit = digits.Single(d => d.Segments == permuted);

                sum += digit.Value;
            }

            return 0;

        }

        private static Digit[] BuildDigits() => new Digit[]
        {
            new(0, "abcefg"),
            new(1, "cf"),
            new(2, "acdeg"),
            new(3, "acdfg"),
            new(4, "bcdf"),
            new(5, "abdfg"),
            new(6, "abdefg"),
            new(7, "acf"),
            new(8, "abcdefg"),
            new(9, "abcdfg"),
        };

        [DebuggerDisplay("{Value} - {Segments}")]
        private class Digit
        {
            public Digit(int value, string segments)
            {
                Value = value;
                Segments = segments;
            }

            public int Value { get; }

            public string Segments { get; }
        }
    }
}
