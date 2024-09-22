using AstarMaze.App.Domain.Enums;

namespace AstarMaze.App.Domain.ValueObjects;

public class PositionNode
{
    public Position Position { get; }
    public PositionNode? Parent { get; }
    public int GCost { get; }
    public int HCost { get; }
    public int FCost => GCost + HCost;

    public PositionNode(Position position, PositionNode? parent, int gCost, int hCost)
    {
        Position = position;
        Parent = parent;
        GCost = gCost;
        HCost = hCost;
    }
}