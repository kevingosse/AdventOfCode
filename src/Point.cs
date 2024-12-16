namespace AdventOfCode;

internal record struct Point(int Line, int Column)
{
    public static readonly Point[] Offsets = GetOffsets().ToArray();

    public static Point operator +(Point a, Point b)
    {
        return new Point { Line = a.Line + b.Line, Column = a.Column + b.Column };
    }

    public static Point operator +(Point point, Direction direction)
    {
        return point + direction.Offset;
    }

    public static Point operator -(Point a, Point b)
    {
        return new Point { Line = a.Line - b.Line, Column = a.Column - b.Column };
    }

    public static Point operator *(Point a, int factor)
    {
        return new Point { Line = a.Line * factor, Column = a.Column * factor };
    }
    
    public static implicit operator Point(ValueTuple<int, int> coordinates)
    {
        return new(coordinates.Item1, coordinates.Item2);
    }

    public readonly bool IsWithinBounds<T>(T[][] array)
    {
        return Line >= 0 && Line < array.Length
            && Column >= 0 && Column < array[0].Length;
    }

    public static IEnumerable<Point> GetOffsets()
    {
        yield return new(0, 1);
        yield return new(1, 0);
        yield return new(0, -1);
        yield return new(-1, 0);
    }

    public static IEnumerable<Point> GetOffsetsWithDiagonals()
    {
        yield return new(0, 1);
        yield return new(-1, 0);
        yield return new(0, -1);
        yield return new(1, 0);
        yield return new(-1, -1);
        yield return new(-1, 1);
        yield return new(1, -1);
        yield return new(1, 1);
    }

    public override string ToString() => $"{Line},{Column}";
}
