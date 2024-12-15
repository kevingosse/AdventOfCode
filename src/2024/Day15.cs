using System.Text;

namespace AdventOfCode._2024;

internal class Day15 : Problem
{
    private Dictionary<char, Point> _instructionMap = new()
    {
        ['^'] = new Point(-1, 0),
        ['v'] = new Point(1, 0),
        ['>'] = new Point(0, 1),
        ['<'] = new Point(0, -1)
    };

    public override void Solve()
    {
        var lines = File.ReadAllLines(Input);

        var tempMap = new List<char[]>();

        int index = 0;

        while (lines[index] != "")
        {
            tempMap.Add(lines[index].ToCharArray());
            index++;
        }

        var map = tempMap.ToArray();

        var instructions = lines.Skip(index + 1)
            .SelectMany(l => l)
            .Select(p => _instructionMap[p])
            .ToArray();

        Point FindInitialPosition()
        {
            for (int line = 0; line < map.Length; line++)
            {
                for (int column = 0; column < map[0].Length; column++)
                {
                    if (map[line][column] == '@')
                    {
                        return new Point(line, column);
                    }
                }
            }

            throw new InvalidOperationException();
        }

        var position = FindInitialPosition();

        foreach (var instruction in instructions)
        {
            var currentObject = position;
            int chain = 0;

            while (true)
            {
                currentObject += instruction;
                chain++;

                if (!currentObject.IsWithinBounds(map) || map.At(currentObject) == '#')
                {
                    chain = -1;
                    break;
                }

                if (map.At(currentObject) == '.')
                {
                    break;
                }
            }

            if (chain == -1)
            {
                // Impossible move
                continue;
            }

            // Move the chain
            for (int i = chain; i > 0; i--)
            {
                ref var newSpot = ref map.At(position + instruction * i);
                ref var oldSpot = ref map.At(position + instruction * (i - 1));

                newSpot = oldSpot;
                oldSpot = '.';
            }

            position += instruction;
        }

        long result = 0;

        for (int line = 0; line < map.Length; line++)
        {
            for (int column = 0; column < map[0].Length; column++)
            {
                if (map[line][column] == 'O')
                {
                    result += line * 100 + column;
                }
            }
        }

        Console.WriteLine(result);
    }

    public override void Solve2()
    {
        var lines = File.ReadAllLines(Input);

        var tempMap = new List<char[]>();

        int index = 0;

        while (lines[index] != "")
        {
            var lineBuilder = new StringBuilder();

            foreach (var c in lines[index])
            {
                if (c == '#')
                {
                    lineBuilder.Append("##");
                }
                else if (c == '@')
                {
                    lineBuilder.Append("@.");
                }
                else if (c == 'O')
                {
                    lineBuilder.Append("[]");
                }
                else if (c == '.')
                {
                    lineBuilder.Append("..");
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }

            tempMap.Add(lineBuilder.ToString().ToCharArray());
            index++;
        }

        var map = tempMap.ToArray();

        var instructions = lines.Skip(index + 1)
            .SelectMany(l => l)
            .Select(p => _instructionMap[p])
            .ToArray();

        Point FindInitialPosition()
        {
            for (int line = 0; line < map.Length; line++)
            {
                for (int column = 0; column < map[0].Length; column++)
                {
                    if (map[line][column] == '@')
                    {
                        return new Point(line, column);
                    }
                }
            }

            throw new InvalidOperationException();
        }

        var position = FindInitialPosition();

        foreach (var instruction in instructions)
        {
            if (CanMove(map, position, instruction))
            {
                Move(map, position, instruction);
                position += instruction;
            }
        }

        long result = 0;

        for (int line = 0; line < map.Length; line++)
        {
            for (int column = 0; column < map[0].Length; column++)
            {
                if (map[line][column] == '[')
                {
                    result += line * 100 + column;
                }
            }
        }

        Console.WriteLine(result);
    }

    private void Move(char[][] map, Point position, Point vector)
    {
        ref var current = ref map.At(position);

        if (current == '.')
        {
            return;
        }

        if (current == '@')
        {
            Move(map, position + vector, vector);
            map.At(position + vector) = '@';
            current = '.';
            return;
        }

        if (current == ']')
        {
            position += new Point(0, -1);
        }

        if (vector == new Point(0, -1))
        {
            // Moving left
            Move(map, position + vector, vector);
            map.At(position + vector) = '[';
            map.At(position) = ']';
            map.At(position - vector) = '.';
        }
        else if (vector == new Point(0, 1))
        {
            // Moving right
            Move(map, position + vector * 2, vector);
            map.At(position + vector) = '[';
            map.At(position + vector * 2) = ']';
            map.At(position) = '.';
        }
        else
        {
            // Moving vertically
            Move(map, position + vector, vector);
            Move(map, position + vector + new Point(0, 1), vector);
            map.At(position) = '.';
            map.At(position + new Point(0, 1)) = '.';
            map.At(position + vector) = '[';
            map.At(position + vector + new Point(0, 1)) = ']';
        }
    }

    private bool CanMove(char[][] map, Point position, Point vector)
    {
        if (!position.IsWithinBounds(map))
        {
            return false;
        }

        var current = map.At(position);

        if (current == '#')
        {
            return false;
        }

        if (current == '.')
        {
            return true;
        }

        if (current == '@')
        {
            return CanMove(map, position + vector, vector);
        }

        if (current == ']')
        {
            position += new Point(0, -1);
        }

        if (vector == new Point(0, -1))
        {
            // Moving left
            return CanMove(map, position + vector, vector);
        }
        else if (vector == new Point(0, 1))
        {
            // Moving right
            return CanMove(map, position + vector * 2, vector);
        }
        else
        {
            // Moving vertically
            return CanMove(map, position + vector, vector)
                && CanMove(map, position + new Point(0, 1) + vector, vector);
        }
    }
}
