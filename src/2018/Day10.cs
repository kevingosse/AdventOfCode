using System.Text.RegularExpressions;

namespace AdventOfCode._2018;

internal class Day10 : Problem
{
    public override void Solve()
    {
        var vectors = File.ReadLines(Input)
            .Select(line => Regex.Matches(line, @"(\-*\d+)"))
            .Select(matches => matches.Select(m => int.Parse(m.Value)))
            .Select(v =>
            {
                var (x, y, offsetX, offsetY) = v;
                return (Position: new Point(y, x), Velocity: new Point(offsetY, offsetX));
            })
            .ToArray();

        while (true)
        {
            if (vectors.MaxBy(v => v.Position.Line).Position.Line
                - vectors.MinBy(v => v.Position.Line).Position.Line < 10)
            {
                var lineOffset = vectors.MinBy(v => v.Position.Line).Position.Line;
                var columnOffset = vectors.MinBy(v => v.Position.Column).Position.Column;

                var offset = new Point(lineOffset, columnOffset);

                Display(vectors.Select(v => v.Position - offset));
                break;
            }

            for (int i = 0; i < vectors.Length; i++)
            {
                ref var vector = ref vectors[i];
                vector.Position += vector.Velocity;
            }

            _nbSeconds++;
        }

        void Display(IEnumerable<Point> points)
        {
            var initialPosition = Console.GetCursorPosition();

            foreach (var position in points)
            {
                Console.SetCursorPosition(position.Column, initialPosition.Top + position.Line);
                Console.Write('X');
            }

            Console.SetCursorPosition(0, initialPosition.Top + 12);
        }
    }

    private long _nbSeconds;

    public override void Solve2()
    {
        Console.WriteLine(_nbSeconds);
    }
}
