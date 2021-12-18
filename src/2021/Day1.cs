using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2021
{
    internal class Day1 : Problem
    {
        public override void Solve()
        {
            int? value = null;
            int increments = 0;

            foreach (var line in File.ReadLines(Input))
            {
                int newValue = int.Parse(line);

                if (value != null && value < newValue)
                {
                    increments++;
                }

                value = newValue;
            }

            Console.WriteLine(increments);
        }

        public override void Solve2()
        {
            var values = File.ReadLines(Input).Select(int.Parse).ToList();

            int? previousWindow = null;
            int increments = 0;

            foreach (var value in GetSlidingWindows(values))
            {
                if (previousWindow != null && previousWindow < value)
                {
                    increments++;
                }

                previousWindow = value;
            }

            Console.WriteLine(increments);
        }

        private IEnumerable<int> GetSlidingWindows(List<int> values)
        {
            var windows = new Queue<Window>();

            foreach (var value in values)
            {
                windows.Enqueue(new Window());

                foreach (var window in windows)
                {
                    window.Add(value);
                }

                if (windows.Peek().Count == 3)
                {
                    var window = windows.Dequeue();
                    yield return window.Value;
                }
            }
        }

        private class Window
        {
            public int Count { get; set; }
            public int Value { get; set; }

            public void Add(int value)
            {
                Count++;
                Value += value;
            }

            public int Reset()
            {
                Count = 0;
                var value = Value;
                Value = 0;
                return value;
            }

        }
    }
}
