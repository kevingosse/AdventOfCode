using System.Diagnostics;
using AdventOfCode;

Problem GetProblem(int? year = null, int? day = null)
{
    var allProblems = typeof(Program).Assembly.GetTypes()
        .Where(t => t.IsSubclassOf(typeof(Problem)))
        .Select(Activator.CreateInstance)
        .Cast<Problem>()
        .ToArray();

    year ??= allProblems
        .Select(p => p.Year)
        .OrderByDescending(p => p)
        .First();

    day ??= allProblems
        .Where(p => p.Year == year)
        .Select(p => p.Day)
        .OrderByDescending(p => p)
        .First();

    var type = Type.GetType($"AdventOfCode._{year}.Day{day}");

    if (type == null)
    {
        throw new Exception($"Could not find specified day and year");
    }

    return (Problem)Activator.CreateInstance(type)!;
}



var problem = GetProblem();

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