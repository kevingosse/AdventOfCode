using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2024;

internal class Day1 : Problem
{
    public override void Solve()
    {
        var firstList = new List<int>();
        var secondList = new List<int>();

        foreach (var line in File.ReadLines(Input))
        {
            var (first, second) = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);
            firstList.Add(first);
            secondList.Add(second);
        }

        firstList.Sort();
        secondList.Sort();

        long result = 0;

        foreach (var (first, second) in firstList.Zip(secondList))
        {
            result += Math.Abs(first - second);
        }

        Console.WriteLine(result);
    }

    public override void Solve2()
    {
        var firstList = new List<int>();
        var secondList = new Dictionary<int, int>();

        foreach (var line in File.ReadLines(Input))
        {
            var (first, second) = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);
            firstList.Add(first);
            
            CollectionsMarshal.GetValueRefOrAddDefault(secondList, second, out _)++;
        }

        long result = 0;

        foreach (var element in firstList)
        {
            if (secondList.TryGetValue(element, out var count))
            {
                result += element * count;
            }
        }

        Console.WriteLine(result);
    }
}
