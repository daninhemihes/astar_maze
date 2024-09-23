using AstarMaze.App.Domain.Enums;

namespace AstarMaze.App.Domain.ValueObjects;

public class Position
{
    public int X { get; }
    public int Y { get; }
    public PositionType Type { get; private set; }

    public Position(int x, int y, PositionType type)
    {
        X = x;
        Y = y;
        Type = type;
    }

    public void CollectHuman()
    {
        if (Type == PositionType.Human){
            Type = PositionType.Empty;
            return;
        }

        throw new InvalidOperationException("Can't collect human: there is no human in the position.");
    }

    public void PlaceHuman()
    {
        if (Type == PositionType.Entry){
            Type = PositionType.Human;
            return;
        }

        throw new InvalidOperationException("Can't place human: needs to be placed at the entry of the maze.");  
    }

    public override bool Equals(object? obj)
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