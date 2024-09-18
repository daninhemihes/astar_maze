using AstarMaze.App.Domain.Entities;
using AstarMaze.App.Domain.Enums;

namespace AstarMaze.Tests;

public class RobotTests
{
    [Fact]
    public void TurnRight_ShouldChangeDirectionsCorrectly()
    {
        var robot = new Robot(Direction.North);

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