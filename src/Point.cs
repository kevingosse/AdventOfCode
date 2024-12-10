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

    public static IEnumerable<Point> GetOffsets()
    {
        yield return new(0, 1);
        yield return new(-1, 0);
        yield return new(0, -1);
        yield return new(1, 0);
    }
}
