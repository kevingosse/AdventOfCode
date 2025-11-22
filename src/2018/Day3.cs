using System.Text.RegularExpressions;

namespace AdventOfCode._2018;

internal class Day3 : Problem
{
    public override void Solve()
    {
        var map = Array.Create<int>(1000, 1000);
        int result = 0;

        foreach (var claim in File.ReadLines(Input))
        {
            // #1 @ 704,926: 5x4
            var match = Regex.Match(claim, @"#\d+ @ (?<x>\d+),(?<y>\d+): (?<width>\d+)x(?<height>\d+)");
            var (x, y, width, height) = match.As<int, int, int, int>();

            for (int column = 0; column < width; column++)
            {
                for (int line = 0; line < height; line++)
                {
                    ref var position = ref map.At((line + y, column + x));

                    if (position == 1)
                    {
                        result++;
                    }

                    position++;
                }
            }
        }

        Console.WriteLine(result);
    }

    public override void Solve2()
    {
        var claims = new List<Claim>();

        foreach (var claim in File.ReadLines(Input))
        {
            // #1 @ 704,926: 5x4
            var match = Regex.Match(claim, @"#(?<id>\d+) @ (?<x>\d+),(?<y>\d+): (?<width>\d+)x(?<height>\d+)");
            var (id, x, y, width, height) = match.As<int, int, int, int, int>();

            claims.Add(new Claim(id, x, y, width, height));
        }

        var candidates = claims.Where(c => !c.Overlaps);

        foreach (var claim in candidates)
        {
            foreach (var otherClaim in claims)
            {
                if (claim.Id == otherClaim.Id)
                {
                    continue;
                }

                if (claim.IntersectsWith(otherClaim))
                {
                    claim.Overlaps = true;
                    otherClaim.Overlaps = true;
                }
            }
        }

        Console.WriteLine(candidates.Single().Id);
    }

    private record Claim(int Id, int X, int Y, int Width, int Height)
    {
        public bool Overlaps { get; set; }

        public bool IntersectsWith(Claim claim)
        {
            return X < claim.X + claim.Width &&
                   X + Width > claim.X &&
                   Y < claim.Y + claim.Height &&
                   Y + Height > claim.Y;
        }
    }
}