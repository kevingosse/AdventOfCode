using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2021
{
    internal class Day10 : Problem
    {
        public override void Solve()
        {
            int score = 0;

            var points = new Dictionary<char, int>
            {
                { ')', 3 },
                { ']', 57 },
                { '}', 1197 },
                { '>', 25137  }
            };

            foreach (var line in File.ReadLines(Input))
            {
                char? error = null;

                var stack = new Stack<char>();

                foreach (var c in line)
                {
                    if (c == '[' || c == '(' || c == '{' || c == '<')
                    {
                        stack.Push(c);
                        continue;
                    }

                    var opener = stack.Pop();

                    if ((c == ']' && opener == '[')
                        || (c == ')' && opener == '(')
                        || (c == '}' && opener == '{')
                        || c == '>' && opener == '<')
                    {
                        continue;
                    }

                    error = c;
                    break;
                }

                if (error != null)
                {
                    score += points[error.Value];
                }
            }

            Console.WriteLine(score);
        }

        public override void Solve2()
        {
            var scores = new List<long>();

            var pairs = new List<(char opener, char closer, int score)>
            {
                ('(', ')', 1),
                ('[', ']', 2),
                ('{', '}', 3),
                ('<', '>', 4)
            };

            var openers = pairs.ToDictionary(p => p.opener, p => p);
            var closers = pairs.ToDictionary(p => p.closer, p => p);

            foreach (var line in File.ReadAllLines(Input))
            {
                if (!IsValidLine(line))
                {
                    continue;
                }

                var errors = new List<char>();

                var stack = new Stack<char>();

                foreach (var c in line)
                {
                    if (openers.ContainsKey(c))
                    {
                        stack.Push(c);
                        continue;
                    }

                    var opener = stack.Pop();

                    var expectedOpener = closers[c];

                    if (expectedOpener.opener == opener)
                    {
                        continue;
                    }

                    while (opener != expectedOpener.opener)
                    {
                        errors.Add(openers[opener].closer);

                        opener = stack.Pop();
                    }
                }

                while (stack.Count > 0)
                {
                    errors.Add(openers[stack.Pop()].closer);
                }

                long score = 0;

                foreach (var error in errors)
                {
                    score *= 5;
                    score += closers[error].score;
                }

                scores.Add(score);
            }

            var result = scores.OrderBy(s => s).Skip(scores.Count / 2).First();

            Console.WriteLine(result);
        }

        private bool IsValidLine(string line)
        {
            char? error = null;

            var stack = new Stack<char>();

            foreach (var c in line)
            {
                if (c == '[' || c == '(' || c == '{' || c == '<')
                {
                    stack.Push(c);
                    continue;
                }

                var opener = stack.Pop();

                if ((c == ']' && opener == '[')
                    || (c == ')' && opener == '(')
                    || (c == '}' && opener == '{')
                    || c == '>' && opener == '<')
                {
                    continue;
                }

                error = c;
                break;
            }

            return error == null;
        }
    }
}