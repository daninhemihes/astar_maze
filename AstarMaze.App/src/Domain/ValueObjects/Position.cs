using AstarMaze.App.Domain.Enums;

namespace AstarMaze.App.Domain.ValueObjects;

public class Position
{
    public int X { get; }
    public int Y { get; }
    public PositionType Type { get; }

    public Position(int x, int y, PositionType type)
    {
        X = x;
        Y = y;
        Type = type;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || obj.GetType() != typeof(Position))
        {
            return false;
        }

        var other = (Position)obj;

        return X == other.X && Y == other.Y && Type == other.Type;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Type);
    }

}