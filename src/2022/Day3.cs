namespace AdventOfCode._2022
{
    internal class Day3 : Problem
    {
        public override void Solve()
        {
            long totalPriority = 0;
            
            foreach (var line in File.ReadLines(Input))
            {
                var part1 = line[..(line.Length / 2)];
                var part2 = line[(line.Length / 2)..];

                var commonLetters = part1.Intersect(part2);
                totalPriority += commonLetters.Sum(GetPriority);
            }

            Console.WriteLine(totalPriority);
        }

        public override void Solve2()
        {
            long totalPriority = 0;

            foreach (var chunk in File.ReadLines(Input).Chunk(3))
            {
                var badge = chunk.IntersectMany().Single();
                totalPriority += GetPriority(badge);
            }

            Console.WriteLine(totalPriority);
        }

        private static int GetPriority(char letter) => char.IsLower(letter) ? (letter - 'a' + 1) : (letter - 'A' + 27);
    }
}
