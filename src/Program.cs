using System.Diagnostics;
using AdventOfCode;

Problem GetProblem(int? year = null, int? day = null)
{
    var allProblems = typeof(Program).Assembly.GetTypes()
        .Where(t => t.IsSubclassOf(typeof(Problem)))
        .Select(Activator.CreateInstance)
        .Cast<Problem>()
        .ToArray();

    year ??= allProblems.Max(p => p.Year);
    day ??= allProblems.Where(p => p.Year == year).Max(p => p.Day);

    return allProblems.First(p => p.Year == year && p.Day == day);
}



var problem = GetProblem(2017);

Console.WriteLine($"Year {problem.Year}, day {problem.Day}");
Console.WriteLine();

Console.WriteLine("Problem 1");
var sw = Stopwatch.StartNew();
problem.Solve();
sw.Stop();
Console.WriteLine($"Finished problem 1 in {sw.ElapsedMilliseconds} ms");

Console.WriteLine();
Console.WriteLine($"-----------------------------");
Console.WriteLine();

Console.WriteLine("Problem 2");
sw.Restart();
problem.Solve2();
sw.Stop();
Console.WriteLine($"Finished problem 2 in {sw.ElapsedMilliseconds} ms");