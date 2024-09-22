using AstarMaze.App.Domain.Enums;
using AstarMaze.App.Domain.Interfaces;
using AstarMaze.App.Domain.Services;
using AstarMaze.App.Domain.ValueObjects;

namespace AstarMaze.Tests;

public class RobotServiceTests
{
    private readonly IRouteService _routeService;
    private readonly IRobotService _robotService;
    public RobotServiceTests()
    {
        _routeService = new RouteService();
        _robotService = new RobotService();
    }

    [Fact]
    public void FetchHumanInMaze_ShouldReturnTrue_WhenHumanIsFoundAndReturned()
    {
        var (maze, entryPosition, humanPosition, expectedPath) = CreateSampleMaze();

        var path = _routeService.FindPath(maze, entryPosition, humanPosition);
        Assert.NotNull(path);

        var response = _robotService.FetchHumanInMaze(maze, path);
        Assert.True(response);
    }

    [Fact]
    public void FetchHumanInMaze_ShouldThrowException_WhenRobotLosesWay()
    {
        var (maze, entryPosition, humanPosition, _) = CreateSampleMaze();

        var path = _routeService.FindPath(maze, entryPosition, humanPosition);
        Assert.NotNull(path);

        path[2] = new Position(1, 3, PositionType.Wall);
        var exception = Assert.Throws<InvalidOperationException>(() => _robotService.FetchHumanInMaze(maze, path));

        Assert.Equal("The robot has lost its way.", exception.Message);
    }

    private (Maze, Position, Position, List<Position>?) CreateSampleMaze()
    {
        int width = 5;
        int height = 6;

        var entryPosition = new Position(0, 2, PositionType.Entry);
        var humanPosition = new Position(1, 4, PositionType.Human);

        var positions = new Position[width, height];
        positions[0,0] = new Position(0, 0, PositionType.Wall);
        positions[1,0] = new Position(1, 0, PositionType.Wall);
        positions[2,0] = new Position(2, 0, PositionType.Wall);
        positions[3,0] = new Position(3, 0, PositionType.Wall);
        positions[4,0] = new Position(4, 0, PositionType.Wall);
        positions[0,1] = new Position(0, 1, PositionType.Wall);
        positions[1,1] = new Position(1, 1, PositionType.Empty);
        positions[2,1] = new Position(2, 1, PositionType.Empty);
        positions[3,1] = new Position(3, 1, PositionType.Empty);
        positions[4,1] = new Position(4, 1, PositionType.Wall);
        positions[0,2] = entryPosition;
        positions[1,2] = new Position(1, 2, PositionType.Empty);
        positions[2,2] = new Position(2, 2, PositionType.Wall);
        positions[3,2] = new Position(3, 2, PositionType.Empty);
        positions[4,2] = new Position(4, 2, PositionType.Wall);
        positions[0,3] = new Position(0, 3, PositionType.Wall);
        positions[1,3] = new Position(1, 3, PositionType.Wall);
        positions[2,3] = new Position(2, 3, PositionType.Wall);
        positions[3,3] = new Position(3, 3, PositionType.Empty);
        positions[4,3] = new Position(4, 3, PositionType.Wall);
        positions[0,4] = new Position(0, 4, PositionType.Wall);
        positions[1,4] = humanPosition;
        positions[2,4] = new Position(2, 4, PositionType.Empty);
        positions[3,4] = new Position(3, 4, PositionType.Empty);
        positions[4,4] = new Position(4, 4, PositionType.Wall);
        positions[0,5] = new Position(0, 5, PositionType.Wall);
        positions[1,5] = new Position(1, 5, PositionType.Wall);
        positions[2,5] = new Position(2, 5, PositionType.Wall);
        positions[3,5] = new Position(3, 5, PositionType.Wall);
        positions[4,5] = new Position(4, 5, PositionType.Wall);

        var expectedPath = new List<Position>();
        expectedPath.Add(entryPosition);
        expectedPath.Add(new Position(1, 2, PositionType.Empty));
        expectedPath.Add(new Position(1, 1, PositionType.Empty));
        expectedPath.Add(new Position(2, 1, PositionType.Empty));
        expectedPath.Add(new Position(3, 1, PositionType.Empty));
        expectedPath.Add(new Position(3, 2, PositionType.Empty));
        expectedPath.Add(new Position(3, 3, PositionType.Empty));
        expectedPath.Add(new Position(3, 4, PositionType.Empty));
        expectedPath.Add(new Position(2, 4, PositionType.Empty));
        expectedPath.Add(humanPosition);

        var maze = new Maze(positions, entryPosition, humanPosition);

        return (maze, entryPosition, humanPosition, expectedPath);
    }
}