using AstarMaze.App.Domain.Enums;

namespace AstarMaze.App.Domain.Entities;

public class Robot
{
    public bool IsCarryingHuman { get; private set; }
    public Direction FacingDirection { get; private set; }

    public Robot(Direction facingDirection = 0){
        IsCarryingHuman = false;
        FacingDirection = facingDirection;
    }

    public void MoveForward() {

    }

    public void TurnRight() {
        FacingDirection = (Direction) (((int)FacingDirection + 1) % 4);
    }

    public void PickHuman() {

    }

    public void EjectHuman() {
        
    }
}