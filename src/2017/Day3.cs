namespace AdventOfCode._2017;

internal class Day3 : Problem
{
    public override void Solve()
    {
        var input = 289326;

        var directions = Point.GetOffsets().Reverse().ToArray();

        int index = 1;
        int armLength = 1;
        var position = new Point(0, 0);
        int remainingArmLength = armLength;
        var direction = 3;

        while (index < input)
        {
            index++;

            if (index == 5)
            {

            }

            position += directions[direction];

            remainingArmLength--;

            if (remainingArmLength == 0)
            {
                if (direction % 2 == 0)
                {
                    armLength++;
                }

                remainingArmLength = armLength;
                direction = (direction + 1) % directions.Length;
            }
        }

        Console.WriteLine(Math.Abs(position.Line) + Math.Abs(position.Column));
    }

    public override void Solve2()
    {
        const int length = 100;

        var input = 289326;

        var map = new int[length][];

        for (int i = 0; i < map.Length; i++)
        {
            map[i] = new int[length];
        }

        var directions = Point.GetOffsets().Reverse().ToArray();

        int index = 1;
        int armLength = 1;
        var position = new Point(0, 0);
        int remainingArmLength = armLength;
        var direction = 3;

        var mapOffset = new Point(length / 2, length / 2);

        map.At(position + mapOffset) = 1;

        while (true)
        {
            index++;

            if (index == 5)
            {

            }

            position += directions[direction];

            remainingArmLength--;

            if (remainingArmLength == 0)
            {
                if (direction % 2 == 0)
                {
                    armLength++;
                }

                remainingArmLength = armLength;
                direction = (direction + 1) % directions.Length;
            }

            int result = 0;

            foreach (var offset in Point.GetOffsetsWithDiagonals())
            {
                result += map.At(position + offset + mapOffset);
            }

            map.At(position + mapOffset) = result;

            if (result > input)
            {
                Console.WriteLine(result);
                break;
            }
        }
    }
}
