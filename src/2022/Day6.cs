namespace AdventOfCode._2022
{
    internal class Day6 : Problem
    {
        public override void Solve()
        {
            var input = File.ReadAllText(Input);

            for (int i = 0; i < input.Length - 4; i++)
            {
                if (input[i..(i + 4)].Distinct().Count() == 4)
                {
                    Console.WriteLine(i + 4);
                    return;
                }
            }
        }

        public override void Solve2()
        {
            var input = File.ReadAllText(Input);

            for (int i = 0; i < input.Length - 14; i++)
            {
                if (input[i..(i + 14)].Distinct().Count() == 14)
                {
                    Console.WriteLine(i + 14);
                    return;
                }
            }
        }
    }
}
