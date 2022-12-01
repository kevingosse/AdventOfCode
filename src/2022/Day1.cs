namespace AdventOfCode._2022
{
    internal class Day1 : Problem
    {
        public override void Solve()
        {
            int max = 0;
            int current = 0;

            foreach (var line in File.ReadLines(Input))
            {
                if (line != string.Empty)
                {
                    current += int.Parse(line);
                }
                else
                {
                    if (current > max)
                    {
                        max = current;
                    }

                    current = 0;
                }
            }
            
            Console.WriteLine(Math.Max(max, current));
        }

        public override void Solve2()
        {
            var calories = File.ReadAllText(Input).Split("\n\n")
                .Select(v => v.Split('\n', StringSplitOptions.RemoveEmptyEntries))
                .Select(v => v.Select(int.Parse).Sum());

            Console.WriteLine(calories.OrderDescending().Take(3).Sum());
        }
    }
}
