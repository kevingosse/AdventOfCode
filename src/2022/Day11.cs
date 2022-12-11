using System.Text.RegularExpressions;

namespace AdventOfCode._2022;

internal class Day11 : Problem
{
    public override void Solve()
    {
        var monkeys = new Monkey[8].New();

        using var reader = File.OpenText(Input);

        while (!reader.EndOfStream)
        {
            var monkey = monkeys[Regex.Match(reader.ReadLine()!, @"Monkey (\d+)\:").As<int>()];

            monkey.Items = new(reader.ReadLine().Split(':')[1].Split(',').Select(long.Parse).Reverse());

            var (operation, operand) = Regex.Match(reader.ReadLine(), @"Operation\: new = old (.) (.+)").As<string, string>();

            if (int.TryParse(operand, out var factor))
            {
                monkey.Operation = operation == "+" ? i => i + factor : i => i * factor;
            }
            else
            {
                monkey.Operation = i => i * i;
            }

            monkey.Divisor = Regex.Match(reader.ReadLine(), @"Test\: divisible by (\d+)").As<int>();

            monkey.ThrowIfTrue = monkeys[Regex.Match(reader.ReadLine(), @"If true\: throw to monkey (\d+)").As<int>()];
            monkey.ThrowIfFalse = monkeys[Regex.Match(reader.ReadLine(), @"If false\: throw to monkey (\d+)").As<int>()];

            _ = reader.ReadLine();
        }

        for (int i = 0; i < 20; i++)
        {
            foreach (var monkey in monkeys)
            {
                monkey.ProcessItems(null);
            }
        }

        Console.WriteLine(monkeys.Select(m => m.Count).OrderDescending().Take(2)
            .Aggregate((m1, m2) => m1 * m2));
    }

    public override void Solve2()
    {
        var monkeys = new Monkey[8].New();

        using var reader = File.OpenText(Input);

        while (!reader.EndOfStream)
        {
            var monkey = monkeys[Regex.Match(reader.ReadLine()!, @"Monkey (\d+)\:").As<int>()];

            monkey.Items = new(reader.ReadLine().Split(':')[1].Split(',').Select(long.Parse).Reverse());

            var (operation, operand) = Regex.Match(reader.ReadLine(), @"Operation\: new = old (.) (.+)").As<string, string>();

            if (int.TryParse(operand, out var factor))
            {
                monkey.Operation = operation == "+" ? i => i + factor : i => i * factor;
            }
            else
            {
                monkey.Operation = i => i * i;
            }

            monkey.Divisor = Regex.Match(reader.ReadLine(), @"Test\: divisible by (\d+)").As<int>();

            monkey.ThrowIfTrue = monkeys[Regex.Match(reader.ReadLine(), @"If true\: throw to monkey (\d+)").As<int>()];
            monkey.ThrowIfFalse = monkeys[Regex.Match(reader.ReadLine(), @"If false\: throw to monkey (\d+)").As<int>()];

            _ = reader.ReadLine();
        }

        var divisor = monkeys.Select(m => m.Divisor).Aggregate((m1, m2) => m1 * m2);

        for (int i = 0; i < 10000; i++)
        {
            foreach (var monkey in monkeys)
            {
                monkey.ProcessItems(divisor);
            }
        }

        Console.WriteLine(monkeys.Select(m => m.Count).OrderDescending().Take(2)
            .Aggregate((m1, m2) => m1 * m2));

    }

    private class Monkey
    {
        public Stack<long> Items { get; set; }

        public Func<long, long> Operation { get; set; }

        public long Divisor { get; set; }

        public Monkey ThrowIfTrue { get; set; }

        public Monkey ThrowIfFalse { get; set; }

        public long Count { get; private set; }

        public void ProcessItems(long? divisor)
        {
            while (Items.Count > 0)
            {
                Count++;
                var item = Items.Pop();

                item = Operation(item);

                if (divisor == null)
                {
                    item /= 3;
                }
                else
                {
                    item %= divisor.Value;
                }

                (item % Divisor == 0 ? ThrowIfTrue : ThrowIfFalse).Items.Push(item);
            }
        }
    }
}