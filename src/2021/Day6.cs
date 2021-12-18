using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2021
{
    internal class Day6 : Problem
    {
        public override void Solve()
        {
            var init = File.ReadAllText(Input).Split(',').Select(int.Parse).ToList();

            var fishes = init;

            for (int turn = 0; turn < 80; turn++)
            {
                for (int i = fishes.Count - 1; i >= 0; i--)
                {
                    fishes[i]--;

                    if (fishes[i] == -1)
                    {
                        fishes[i] = 6;
                        fishes.Add(8);
                    }
                }
            }

            Console.WriteLine(fishes.Count);
        }

        public override void Solve2()
        {
            var init = File.ReadAllText(Input).Split(',').Select(int.Parse).ToList();

            var fishes = new long[9];

            foreach (var f in init)
            {
                fishes[f]++;
            }

            for (int turn = 0; turn < 256; turn++)
            {
                var mature = fishes[0];

                fishes[1..].CopyTo(fishes, 0);

                fishes[8] = mature;
                fishes[6] += mature;
            }

            Console.WriteLine(fishes.Sum());
        }
    }
}
