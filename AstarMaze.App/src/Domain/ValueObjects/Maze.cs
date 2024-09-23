using AstarMaze.App.Domain.Enums;

namespace AstarMaze.App.Domain.ValueObjects;

public class Maze
{
    public Position[,] Positions { get; }
    public Position HumanPosition { get; }
    public Position EntryPosition { get; }
    public Direction EntryDirection { get; }
    public int Width { get ; }
    public int Height { get; }

    public Maze(Position[,] positions, Position entryPosition, Position humanPosition)
    {
        if (positions == null || positions.GetLength(0) < 3 || positions.GetLength(1) < 3) throw new ArgumentException("Invalid maze positions. Should be at least 3x3.");
        if (entryPosition == null || entryPosition.Type != PositionType.Entry) throw new ArgumentException("Invalid entry position.");
        if (humanPosition == null || humanPosition.Type != PositionType.Human) throw new ArgumentException("Invalid human position.");

        Positions       = positions;
        EntryPosition   = entryPosition;
        HumanPosition   = humanPosition;
        Width           = positions.GetLength(0);
        Height          = positions.GetLength(1);
        EntryDirection = GetEntryDirection(positions);
    }
    public Direction GetEntryDirection(Position[,] positions)
    {
        if (EntryPosition.Y == 0) return Direction.North;
        if (EntryPosition.Y == Height - 1) return Direction.South;
        if (EntryPosition.X == 0) return Direction.East;
        if (EntryPosition.X == Width - 1) return Direction.West;

        throw new InvalidOperationException($"Invalid entry position. Entry must be adjacent to a wall.");
    }

    public Position? GetPosition(int x, int y)
    {
        if (x >= 0 && x < Width && y >= 0 && y < Height)
        {
            return Positions[x,y];
        }
        return null;
    }
}