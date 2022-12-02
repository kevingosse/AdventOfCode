using System.Runtime.InteropServices;

namespace AdventOfCode._2016
{
    internal class Day6 : Problem
    {
        public override void Solve()
        {
            var letters = new Dictionary<char, int>[8].New();
            
            foreach (var line in File.ReadLines(Input))
            {
                for (int i = 0; i < line.Length; i++)
                {
                    CollectionsMarshal.GetValueRefOrAddDefault(letters[i], line[i], out _) += 1;
                }
            }

            foreach (var letter in letters)
            {
                Console.Write(letter.MaxBy(k => k.Value).Key);
            }

            Console.WriteLine();
        }

        public override void Solve2()
        {
            var letters = new Dictionary<char, int>[8].New();

            foreach (var line in File.ReadLines(Input))
            {
                for (int i = 0; i < line.Length; i++)
                {
                    CollectionsMarshal.GetValueRefOrAddDefault(letters[i], line[i], out _) += 1;
                }
            }

            foreach (var letter in letters)
            {
                Console.Write(letter.MinBy(k => k.Value).Key);
            }

            Console.WriteLine();
        }
    }
}
