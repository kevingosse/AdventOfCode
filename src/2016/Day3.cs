using System.Text.RegularExpressions;

namespace AdventOfCode._2016
{
    internal class Day3 : Problem
    {
        public override void Solve()
        {
            int count = 0;

            foreach (var line in File.ReadLines(Input))
            {
                var (side1, side2, side3) = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).Order();

                if (side1 + side2 > side3)
                {
                    count += 1;
                }
            }

            Console.WriteLine(count);
        }

        public override void Solve2()
        {
            int count = 0;

            foreach (var triangle in GetTriangles())
            {
                var (side1, side2, side3) = triangle.Order();

                if (side1 + side2 > side3)
                {
                    count += 1;
                }
            }

            Console.WriteLine(count);
        }

        private IEnumerable<int[]> GetTriangles()
        {
            foreach (var group in File.ReadLines(Input).Select(l => l.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse)).Chunk(3))
            {
                foreach (var elements in group[0].Zip(group[1], group[2]))
                {
                    yield return new[] { elements.First, elements.Second, elements.Third };
                }
            }
        }

    }
}
