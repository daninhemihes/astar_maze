using AstarMaze.App.Domain.Enums;

namespace AstarMaze.App.Domain.ValueObjects;

public class Maze
{
    public Position[,] Positions { get; }
    public Position HumanPosition { get; }
    public Position EntryPosition { get; }
    public Direction EntryDirection { get; }
    public int Height { get; }
    public int Width { get ; }

    public Maze(Position[,] positions, Position entryPosition, Position humanPosition)
    {
        if (positions == null || positions.GetLength(0) < 3 || positions.GetLength(1) < 3) throw new ArgumentException("Invalid maze positions. Should be at least 3x3.");
        if (entryPosition == null || entryPosition.Type != PositionType.Entry) throw new ArgumentException("Invalid entry position.");
        if (humanPosition == null || humanPosition.Type != PositionType.Human) throw new ArgumentException("Invalid human position.");

        Positions       = positions;
        EntryPosition   = entryPosition;
        HumanPosition   = humanPosition;
        Height          = positions.GetLength(0);
        Width           = positions.GetLength(1);
        EntryDirection = GetEntryDirection(positions);
    }
    public Direction GetEntryDirection(Position[,] positions)
    {
        int entryX = EntryPosition.X;
        int entryY = EntryPosition.Y;

        if (entryY > 0 && positions[entryX, entryY - 1].Type == PositionType.Wall)
            return Direction.North; 
        if (entryY < Height - 1 && positions[entryX, entryY + 1].Type == PositionType.Wall)
            return Direction.South; 
        if (entryX > 0 && positions[entryX - 1, entryY].Type == PositionType.Wall)
            return Direction.West;  
        if (entryX < Width - 1 && positions[entryX + 1, entryY].Type == PositionType.Wall)
            return Direction.East;  

        throw new InvalidOperationException("Invalid entry position. Entry must be adjacent to a wall.");
    }


}