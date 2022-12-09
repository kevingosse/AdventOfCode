namespace AdventOfCode._2022;

internal class Day9 : Problem
{
    public override void Solve()
    {
        (int x, int y) head = (0, 0);
        (int x, int y) tail = (0, 0);

        var knownPositions = new HashSet<(int x, int y)> { tail };

        foreach (var line in File.ReadLines(Input))
        {
            var (direction, steps) = line.Split(' ').As<string, int>();

            while (steps-- > 0)
            {
                if (direction == "L")
                {
                    head.x -= 1;
                }
                else if (direction == "R")
                {
                    head.x += 1;
                }
                else if (direction == "D")
                {
                    head.y -= 1;
                }
                else if (direction == "U")
                {
                    head.y += 1;
                }

                if (Math.Abs(head.x - tail.x) > 1 || Math.Abs(head.y - tail.y) > 1)
                {
                    if (head.x == tail.x)
                    {
                        tail.y += head.y > tail.y ? 1 : -1;
                    }
                    else if (head.y == tail.y)
                    {
                        tail.x += head.x > tail.x ? 1 : -1;
                    }
                    else
                    {
                        tail.x += (head.x - tail.x) / Math.Abs(head.x - tail.x);
                        tail.y += (head.y - tail.y) / Math.Abs(head.y - tail.y);
                    }
                }

                knownPositions.Add(tail);
            }
        }

        Console.WriteLine(knownPositions.Count);
    }

    public override void Solve2()
    {
        var rope = new (int x, int y)[10];

        ref var head = ref rope[0];
        ref var tail = ref rope[9];

        var knownPositions = new HashSet<(int x, int y)> { tail };

        foreach (var line in File.ReadLines(Input))
        {
            var (direction, steps) = line.Split(' ').As<string, int>();

            while (steps-- > 0)
            {
                if (direction == "L")
                {
                    head.x -= 1;
                }
                else if (direction == "R")
                {
                    head.x += 1;
                }
                else if (direction == "D")
                {
                    head.y -= 1;
                }
                else if (direction == "U")
                {
                    head.y += 1;
                }

                for (int i = 1; i < rope.Length; i++)
                {
                    ref var knot = ref rope[i];
                    ref var previousKnot = ref rope[i - 1];

                    if (Math.Abs(previousKnot.x - knot.x) > 1 || Math.Abs(previousKnot.y - knot.y) > 1)
                    {
                        if (previousKnot.x == knot.x)
                        {
                            knot.y += previousKnot.y > knot.y ? 1 : -1;
                        }
                        else if (previousKnot.y == knot.y)
                        {
                            knot.x += previousKnot.x > knot.x ? 1 : -1;
                        }
                        else
                        {
                            knot.x += (previousKnot.x - knot.x) / Math.Abs(previousKnot.x - knot.x);
                            knot.y += (previousKnot.y - knot.y) / Math.Abs(previousKnot.y - knot.y);
                        }
                    }
                }

                knownPositions.Add(tail);
            }
        }

        Console.WriteLine(knownPositions.Count);

    }
}