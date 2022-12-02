namespace AdventOfCode._2022
{
    internal class Day2 : Problem
    {
        public override void Solve()
        {
            long score = 0;

            foreach (var line in File.ReadLines(Input))
            {
                var (rawMove1, rawMove2) = line.Split(' ');

                var move1 = rawMove1[0] - 'A';
                var move2 = rawMove2[0] - 'X';

                score += ComputeScore(move1, move2);
            }

            Console.WriteLine(score);
        }

        public override void Solve2()
        {
            long score = 0;

            foreach (var line in File.ReadLines(Input))
            {
                var (rawMove1, intention) = line.Split(' ');

                var move1 = rawMove1[0] - 'A';
                var move2 = move1;

                if (intention == "X")
                {
                    move2 = (move2 + 2) % 3;
                }
                else if (intention == "Z")
                {
                    move2 = (move2 + 1) % 3;
                }

                score += ComputeScore(move1, move2);
            }

            Console.WriteLine(score);
        }

        private static int ComputeScore(int move1, int move2)
        {
            int score = move2 + 1;

            if (move1 == move2)
            {
                score += 3;
            }
            else if (move2 == (move1 + 1) % 3)
            {
                score += 6;
            }

            return score;
        }
    }
}
