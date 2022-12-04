namespace AdventOfCode._2022
{
    internal class Day4 : Problem
    {
        public override void Solve()
        {
            int count = 0;

            foreach (var line in File.ReadLines(Input))
            {
                var (range1, range2) = line.Split(',').Select(Range.Parse).OrderByDescending(r => r.End - r.Start);

                if (range2.Start >= range1.Start && range2.End <= range1.End)
                {
                    count += 1;
                }
            }

            Console.WriteLine(count);
        }

        public override void Solve2()
        {
            int count = 0;

            foreach (var line in File.ReadLines(Input))
            {
                var (range1, range2) = line.Split(',').Select(Range.Parse);

                if (range1.OverlapsWith(range2))
                {
                    count += 1;
                }
            }

            Console.WriteLine(count);
        }

        private record Range(int Start, int End)
        {
            public static Range Parse(string input)
            {
                var (start, end) = input.Split('-').Select(int.Parse);
                return new Range(start, end);
            }

            public bool OverlapsWith(Range range) => Start >= range.Start && Start <= range.End
                                                     || range.Start >= Start && range.Start <= End;
        }
    }
}
