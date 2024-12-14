namespace AdventOfCode._2024;

internal class Day14 : Problem
{
    private const int Width = 101;
    private const int Height = 103;

    public override void Solve()
    {
        var robots = new List<Robot>();

        foreach (var line in File.ReadLines(Input))
        {
            var parts = line.Split(' ');
            var values1 = parts[0].Split(',');
            var values2 = parts[1].Split(',');

            var position = new Point(int.Parse(values1[1]), int.Parse(values1[0][2..]));
            var velocity = new Point(int.Parse(values2[1]), int.Parse(values2[0][2..]));

            robots.Add(new(position, velocity));
        }

        for (int i = 0; i < 100; i++)
        {
            foreach (var robot in robots)
            {
                robot.Position += robot.Velocity;
                robot.Position.Line = Wrap(robot.Position.Line, Height);
                robot.Position.Column = Wrap(robot.Position.Column, Width);
            }
        }

        var quadrants = new int[4];

        foreach (var robot in robots)
        {
            if (robot.Position.Line == Height / 2 || robot.Position.Column == Width / 2)
            {
                continue;
            }

            var horizontalHalf = robot.Position.Column < Width / 2 ? 0 : 1;
            var verticalHalf = robot.Position.Line < Height / 2 ? 0 : 2;

            quadrants[horizontalHalf + verticalHalf]++;
        }

        Console.WriteLine(quadrants.Aggregate(1, (x, y) => x * y));
    }

    public override void Solve2()
    {
        var map = new int[Height][];

        for (int i = 0; i < Height; i++)
        {
            map[i] = new int[Width];
        }

        var robots = new List<Robot>();

        foreach (var line in File.ReadLines(Input))
        {
            var parts = line.Split(' ');
            var values1 = parts[0].Split(',');
            var values2 = parts[1].Split(',');

            var position = new Point(int.Parse(values1[1]), int.Parse(values1[0][2..]));
            var velocity = new Point(int.Parse(values2[1]), int.Parse(values2[0][2..]));

            robots.Add(new(position, velocity));

            map.At(position)++;
        }

        void DisplayMap()
        {
            for (int line = 0; line < Height; line++)
            {
                for (int column = 0; column < Width; column++)
                {
                    var count = robots.Count(r => r.Position == new Point(line, column));
                    Console.Write(count == 0 ? "." : count.ToString());
                }

                Console.WriteLine();
            }
        }

        int iterations = 0;

        while (true)
        {
            foreach (var robot in robots)
            {
                var oldPosition = robot.Position;

                robot.Position += robot.Velocity;
                robot.Position.Line = Wrap(robot.Position.Line, Height);
                robot.Position.Column = Wrap(robot.Position.Column, Width);

                map.At(oldPosition)--;
                map.At(robot.Position)++;
            }

            iterations++;

            if (FindHorizontalAlignment(map, 5) && FindVerticalAlignment(map, 5))
            {
                Console.WriteLine("Iteration " + iterations);
                DisplayMap();

                Console.WriteLine();
                Console.WriteLine();

                if (Console.ReadLine() == "q")
                {
                    return;
                }
            }
        }
    }

    private bool FindHorizontalAlignment(int[][] map, int size)
    {
        for (int line = 0; line < Height; line++)
        {
            int count = 0;

            for (int column = 0; column < Width; column++)
            {
                if (map[line][column] > 0)
                {
                    count++;

                    if (count >= size)
                    {
                        return true;
                    }
                }
                else
                {
                    count = 0;
                }
            }
        }

        return false;
    }

    private bool FindVerticalAlignment(int[][] map, int size)
    {
        for (int column = 0; column < Width; column++)
        {
            int count = 0;

            for (int line = 0; line < Height; line++)
            {
                if (map[line][column] > 0)
                {
                    count++;

                    if (count >= size)
                    {
                        return true;
                    }
                }
                else
                {
                    count = 0;
                }
            }
        }

        return false;
    }


    private int Wrap(int value, int boundary)
    {
        if (value < 0)
        {
            while (value < 0)
            {
                value += boundary;
            }

            return value;
        }

        return value % boundary;
    }

    private record Robot
    {
        public Robot(Point position, Point velocity)
        {
            Position = position;
            Velocity = velocity;
        }

        public Point Position;
        public Point Velocity;
    }
}
