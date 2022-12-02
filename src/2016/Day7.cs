namespace AdventOfCode._2016
{
    internal class Day7 : Problem
    {
        public override void Solve()
        {
            int count = 0;

            foreach (var line in File.ReadLines(Input))
            {
                var parts = line.Split('[', ']');
                var supernet = parts.Where((_, i) => i % 2 == 0);
                var hypernet = parts.Where((_, i) => i % 2 == 1);

                if (supernet.Any(IsAbba) && !hypernet.Any(IsAbba))
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
                var parts = line.Split('[', ']');
                var supernet = parts.Where((_, i) => i % 2 == 0);
                var hypernet = parts.Where((_, i) => i % 2 == 1);

                if (GetAba(supernet).Any(a => FindBab(hypernet, a)))
                {
                    count += 1;
                }
            }

            Console.WriteLine(count);

        }

        private static bool IsAbba(string part)
        {
            for (int i = 0; i <= part.Length - 4; i++)
            {
                if (part[i] != part[i + 1] && part[i + 3] == part[i] && part[i + 1] == part[i + 2])
                {
                    return true;
                }
            }

            return false;
        }

        private static IEnumerable<string> GetAba(IEnumerable<string> parts)
        {
            foreach (var part in parts)
            {
                for (int i = 0; i <= part.Length - 3; i++)
                {
                    if (part[i] == part[i + 2] && part[i] != part[i + 1])
                    {
                        yield return part[i..(i + 3)];
                    }
                }
            }
        }

        private static bool FindBab(IEnumerable<string> parts, string aba)
        {
            foreach (var part in parts)
            {
                for (int i = 0; i <= part.Length - 3; i++)
                {
                    if (part[i] == aba[1] && part[i + 1] == aba[0] && part[i + 2] == aba[1])
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
