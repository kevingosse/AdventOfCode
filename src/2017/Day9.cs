namespace AdventOfCode._2017;

internal class Day9 : Problem
{
    public override void Solve()
    {
        var input = File.ReadAllText(Input);

        int nestedLevel = 0;
        bool garbage = false;
        int result = 0;

        for (int i = 0; i < input.Length; i++)
        {
            var c = input[i];

            if (garbage)
            {
                if (c == '>')
                {
                    garbage = false;
                }
                else if (c == '!')
                {
                    i++;
                }
            }
            else
            {
                if (c == '<')
                {
                    garbage = true;
                }
                else if (c == '{')
                {
                    nestedLevel++;
                }
                else if (c == '}')
                {
                    result += nestedLevel;
                    nestedLevel--;
                }
            }
        }

        Console.WriteLine(result);
    }

    public override void Solve2()
    {
        var input = File.ReadAllText(Input);

        bool garbage = false;
        int result = 0;

        for (int i = 0; i < input.Length; i++)
        {
            var c = input[i];

            if (garbage)
            {
                if (c == '>')
                {
                    garbage = false;
                }
                else if (c == '!')
                {
                    i++;
                }
                else
                {
                    result++;
                }
            }
            else
            {
                if (c == '<')
                {
                    garbage = true;
                }
            }
        }

        Console.WriteLine(result);
    }
}
