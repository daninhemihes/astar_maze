using AstarMaze.App.Domain.Enums;

namespace AstarMaze.App.Domain.ValueObjects;

public class Maze
{
    public Position[][] Positions { get; }
    public Position HumanPosition { get; }
    public Position EntryPosition { get; }
    public Direction EntryDirection { get; }
    public int Height { get; }
    public int Width { get ; }

    public Maze(Position[][] positions, Position entryPosition, Position humanPosition)
    {
        if (positions == null || positions.Length < 3 || positions[0].Length < 3) throw new ArgumentException("Invalid maze positions.");
        if (entryPosition == null || entryPosition.Type != PositionType.Entry) throw new ArgumentException("Invalid entry position.");
        if (humanPosition == null || humanPosition.Type != PositionType.Human) throw new ArgumentException("Invalid human position.");

        Positions       = positions;
        EntryPosition   = entryPosition;
        HumanPosition   = humanPosition;
        Height          = positions.Length;
        Width           = positions[0].Length;
        EntryDirection  = GetEntryDirection();
    }

    public Direction GetEntryDirection()
    {
        if (EntryPosition.Y == 0) return Direction.North;
        if (EntryPosition.Y == Height - 1) return Direction.South;
        if (EntryPosition.X == 0) return Direction.East;
        if (EntryPosition.X == Width - 1) return Direction.West;

        throw new InvalidOperationException("Invalid entry position. Entry must be on the maze boundary at a border.");
    }
}