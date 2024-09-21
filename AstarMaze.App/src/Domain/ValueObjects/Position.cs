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

    
}