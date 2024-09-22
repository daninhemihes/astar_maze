using AstarMaze.App.Domain.Enums;
using AstarMaze.App.Domain.ValueObjects;

namespace AstarMaze.App.Domain.Entities;

public class Robot
{
    public bool IsCarryingHuman { get; private set; }
    public Direction FacingDirection { get; private set; }
    public Position CurrentPosition { get; private set; }

    public Robot(Position currentPosition, Direction facingDirection){
        IsCarryingHuman = false;
        CurrentPosition = currentPosition;
        FacingDirection = facingDirection;
    }

    public void MoveForward() {
        CurrentPosition = FacingDirection switch
        {
            Direction.North => new Position(CurrentPosition.X, CurrentPosition.Y + 1, PositionType.Empty),
            Direction.South => new Position(CurrentPosition.X, CurrentPosition.Y - 1, PositionType.Empty),
            Direction.East  => new Position(CurrentPosition.X + 1, CurrentPosition.Y, PositionType.Empty),
            Direction.West  => new Position(CurrentPosition.X - 1, CurrentPosition.Y, PositionType.Empty),
            _ => CurrentPosition
        };
    }

    public void TurnRight() {
        FacingDirection = (Direction) (((int)FacingDirection + 1) % 4);
    }

    public void PickHuman() {
        IsCarryingHuman = true;
    }

    public void EjectHuman() {
        IsCarryingHuman = false;
    }

    public Direction GetNextPositionDirection(Position targetPosition)
    {
        int xDiff = targetPosition.X - CurrentPosition.X;
        int yDiff = targetPosition.Y - CurrentPosition.Y;

        if (xDiff == 0  && yDiff == 1 ) return Direction.North;
        if (xDiff == 0  && yDiff == -1) return Direction.South;
        if (xDiff == 1  && yDiff == 0 ) return Direction.East;
        if (xDiff == -1 && yDiff == 0 ) return Direction.West;
        
        throw new InvalidOperationException("Target position is outside of robot bounds.");
    }
}