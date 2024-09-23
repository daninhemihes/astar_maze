using AstarMaze.App.Domain.Enums;
using AstarMaze.App.Domain.Interfaces;
using AstarMaze.App.Domain.ValueObjects;

namespace AstarMaze.App.Domain.Entities;

public class Robot : ISubject
{
    public bool IsCarryingHuman { get; private set; }
    public Direction FacingDirection { get; private set; }
    public Position CurrentPosition { get; private set; }
    public Command LastCommand { get; private set; }
    private List<IObserver> _observers;

    public Robot(Position currentPosition, Direction facingDirection){
        IsCarryingHuman = false;
        CurrentPosition = currentPosition;
        FacingDirection = facingDirection;
        LastCommand = Command.Start;
        _observers = new List<IObserver>();
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
        LastCommand = Command.Move;
        Notify();
    }

    public void TurnRight() {
        FacingDirection = (Direction) (((int)FacingDirection + 1) % 4);
        LastCommand = Command.Turn;
        Notify();
    }

    public void PickHuman() {
        IsCarryingHuman = true;
        LastCommand = Command.Pick;
        Notify();
    }

    public void EjectHuman() {
        IsCarryingHuman = false;
        LastCommand = Command.Eject;
        Notify();
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

    public void Attach(IObserver observer)
    {
        _observers.Add(observer);
        Notify();
    }

    public void Detach(IObserver observer)
    {
        _observers.Remove(observer);
    }

    public void Notify()
    {
        foreach (var observer in _observers)
        {
            observer.Update(this);
        }
    }
}