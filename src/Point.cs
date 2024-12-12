namespace AdventOfCode;

internal record struct Point(int Line, int Column)
{
    public static Point operator +(Point a, Point b)
    {
        return new Point { Line = a.Line + b.Line, Column = a.Column + b.Column };
    }

    public static Point operator -(Point a, Point b)
    {
        return new Point { Line = a.Line - b.Line, Column = a.Column - b.Column };
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
}
