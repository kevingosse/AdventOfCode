using System.Text.RegularExpressions;

namespace AdventOfCode._2022
{
    internal class Day5 : Problem
    {
        public override void Solve()
        {
            using var stream = File.OpenRead(Input);
            using var reader = new StreamReader(stream);

            var stackInput = new List<string>();

            string? line;

            while ((line = reader.ReadLine()) != string.Empty)
            {
                stackInput.Add(line!);
            }

            var stacks = new Stack<char>[9].New();

            for (int i = stackInput.Count - 2; i >= 0; i--)
            {
                for (int j = 0; j < 9; j++)
                {
                    var crate = stackInput[i][j * 4 + 1];

                    if (crate != ' ')
                    {
                        stacks[j].Push(crate);
                    }
                }
            }

            while ((line = reader.ReadLine()) != null)
            {
                var (count, origin, destination) = Regex.Match(line, @"move (\d+) from (\d+) to (\d+)").As<int, int, int>();

                for (int i = 0; i < count; i++)
                {
                    stacks[destination - 1].Push(stacks[origin - 1].Pop());
                }
            }

            foreach (var stack in stacks)
            {
                Console.Write(stack.Peek());
            }

            Console.WriteLine();
        }

        public override void Solve2()
        {
            using var stream = File.OpenRead(Input);
            using var reader = new StreamReader(stream);

            var stackInput = new List<string>();

            string? line;

            while ((line = reader.ReadLine()) != string.Empty)
            {
                stackInput.Add(line!);
            }

            var stacks = new Stack<char>[9].New();

            for (int i = stackInput.Count - 2; i >= 0; i--)
            {
                for (int j = 0; j < 9; j++)
                {
                    var crate = stackInput[i][j * 4 + 1];

                    if (crate != ' ')
                    {
                        stacks[j].Push(crate);
                    }
                }
            }

            var buffer = new Stack<char>();

            while ((line = reader.ReadLine()) != null)
            {
                var (count, origin, destination) = Regex.Match(line, @"move (\d+) from (\d+) to (\d+)").As<int, int, int>();

                for (int i = 0; i < count; i++)
                {
                    buffer.Push(stacks[origin - 1].Pop());
                }

                while (buffer.Count > 0)
                {
                    stacks[destination - 1].Push(buffer.Pop());
                }
            }

            foreach (var stack in stacks)
            {
                Console.Write(stack.Peek());
            }

            Console.WriteLine();
        }
    }
}

