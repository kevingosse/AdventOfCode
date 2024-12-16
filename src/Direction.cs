namespace AdventOfCode;

internal readonly struct Direction(int value) : IEquatable<Direction>
{
    public readonly int Value = value;

    public Point Offset => Point.Offsets[Value];

    public static implicit operator Direction(int value)
    {
        return new(value);
    }

    public Direction Left()
    {
        var direction = Value - 1;

        if (direction < 0)
        {
            direction += Point.Offsets.Length;
        }

        return direction;
    }

    public Direction Right() => (Value + 1) % Point.Offsets.Length;

    public Direction Opposite() => (Value + 2) % Point.Offsets.Length;

    public bool Equals(Direction other) => Value == other.Value;

    public override bool Equals(object? obj)
    {
        if (obj is not Direction direction)
        {
            return false;
        }

        return direction.Equals(this);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}
