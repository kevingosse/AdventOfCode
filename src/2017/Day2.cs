namespace AdventOfCode._2017
{
    internal class Day2 : Problem
    {
        public override void Solve()
        {
            long sum = 0;

            foreach (var line in File.ReadLines(Input))
            {
                var (min, max) = line.Split('\t').Select(int.Parse).MinMax();
                sum += max - min;
            }

            Console.WriteLine(sum);
        }

        public override void Solve2()
        {
            long sum = 0;

            foreach (var line in File.ReadLines(Input))
            {
                var values = line.Split('\t').Select(int.Parse).ToArray();

                for (int i = 0; i < values.Length; i++)
                {
                    for (int j = i + 1; j < values.Length; j++)
                    {
                        var (min, max) = (values[i],  values[j]).MinMax();

                        if (max % min == 0)
                        {
                            sum += max / min;
                        }
                    }
                }
            }

            Console.WriteLine(sum);
        }
    }
}
