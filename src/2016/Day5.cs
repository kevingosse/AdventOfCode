using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode._2016
{
    internal class Day5 : Problem
    {
        public override void Solve()
        {
            var input = "ojvtpuvg";

            var solution = new StringBuilder();

            using var md5 = MD5.Create();

            int seed = 0;

            for (int i = 0; i < 8; i++)
            {
                while (true)
                {
                    var hash = md5.ComputeHash(Encoding.ASCII.GetBytes(input + seed));
                    var result = Convert.ToHexString(hash);

                    seed += 1;

                    if (result[..5] == "00000")
                    {
                        solution.Append(result[5]);
                        break;
                    }
                }

                var position = Console.GetCursorPosition();
                Console.SetCursorPosition(0, position.Top);
                Console.Write(solution);
            }

            Console.WriteLine();
        }

        public override void Solve2()
        {
            var input = "ojvtpuvg";

            var solution = new StringBuilder(8);
            solution.Append(new string('_', 8));

            using var md5 = MD5.Create();

            int seed = 0;

            Console.Write(solution);

            for (int i = 0; i < 8; i++)
            {
                while (true)
                {
                    var hash = md5.ComputeHash(Encoding.ASCII.GetBytes(input + seed));
                    var result = Convert.ToHexString(hash);

                    seed += 1;

                    if (result[..5] == "00000")
                    {
                        var index = result[5] - '0';

                        if (index is >= 0 and < 8 && solution[index] == '_')
                        {
                            solution[index] = result[6];
                            break;
                        }
                    }
                }

                var position = Console.GetCursorPosition();
                Console.SetCursorPosition(0, position.Top);
                Console.Write(solution);
            }

            Console.WriteLine();
        }
    }
}
