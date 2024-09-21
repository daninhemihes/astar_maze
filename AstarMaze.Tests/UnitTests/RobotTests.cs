using AstarMaze.App.Domain.Entities;
using AstarMaze.App.Domain.Enums;
using AstarMaze.App.Domain.ValueObjects;

namespace AstarMaze.Tests;

public class RobotTests
{
    [Fact]
    public void MoveForward_ShouldMoveInSquareCorrectly()
    {
        var initialPosition = new Position(0, 0, PositionType.Entry);
        var robot = new Robot(initialPosition, Direction.North);

        var expectedPositions = new[]
        {
            new Position(0, 1, PositionType.Empty),
            new Position(1, 1, PositionType.Empty),
            new Position(1, 0, PositionType.Empty),
            new Position(0, 0, PositionType.Empty),
        };

        Assert.Equal(initialPosition, robot.CurrentPosition);

        foreach (var expectedPosition in expectedPositions)
        {
            robot.MoveForward();
            Assert.Equal(expectedPosition, robot.CurrentPosition);
            robot.TurnRight();
        }
    }

    [Fact]
    public void TurnRight_ShouldChangeDirectionsCorrectly()
    {
        var robotPosition = new Position(0, 0, PositionType.Entry);
        var robot = new Robot(robotPosition, Direction.North);

        Assert.Equal(Direction.North, robot.FacingDirection);

        var expectedDirections = new[]
        {
            Direction.East,
            Direction.South,
            Direction.West,
            Direction.North,
            Direction.East
        };

        foreach (var expectedDirection in expectedDirections)
        {
            robot.TurnRight();
            Assert.Equal(expectedDirection, robot.FacingDirection);
        }
    }
}