using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2021
{
    internal class Day7 : Problem
    {
        public override void Solve()
        {
            var values = File.ReadAllText(Input).Split(',').Select(int.Parse).ToArray();

            Array.Sort(values);

            var median = values[values.Length / 2];

            var result = values.Sum(x => Math.Abs(x - median));

            Console.WriteLine(result);
        }

        public override void Solve2()
        {
            var values = File.ReadAllText(Input).Split(',').Select(int.Parse).ToArray();

            int index = 0;
            long? sum = null;

            while (true)
            {
                var newSum = ComputeSum(values, index);

                if (sum != null && newSum > sum.Value)
                {
                    Console.WriteLine($"{index - 1} for a total of {sum}");
                    return;
                }

                sum = newSum;
                index++;
            }
        }

        private static long ComputeSum(int[] array, int pivot)
        {
            long sum = 0;

            foreach (var x in array)
            {
                var diff = Math.Abs(x - pivot);
                sum += (diff * (diff + 1)) / 2;
            }

            return sum;
        }
    }
}
