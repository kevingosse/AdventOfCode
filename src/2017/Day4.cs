namespace AdventOfCode._2017;

internal class Day4 : Problem
{
    public override void Solve()
    {
        int count = 0;

        foreach (var line in File.ReadLines(Input))
        {
            bool isValid = true;
            var words = new HashSet<string>();

            foreach (var word in line.Split(' '))
            {
                if (!words.Add(word))
                {
                    isValid = false;
                    break;
                }
            }

            if (isValid)
            {
                count++;
            }
        }

        Console.WriteLine(count);
    }

    public override void Solve2()
    {
        int count = 0;

        foreach (var line in File.ReadLines(Input))
        {
            bool isValid = true;
            var words = new HashSet<string>();

            foreach (var word in line.Split(' '))
            {
                if (!words.Add(string.Concat(word.Order())))
                {
                    isValid = false;
                    break;
                }
            }

            if (isValid)
            {
                count++;
            }
        }

        Console.WriteLine(count);
    }
}
