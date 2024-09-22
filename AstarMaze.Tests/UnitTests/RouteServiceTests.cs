using AstarMaze.App.Domain.Enums;
using AstarMaze.App.Domain.Interfaces;
using AstarMaze.App.Domain.Services;
using AstarMaze.App.Domain.ValueObjects;

namespace AstarMaze.Tests;

public class RouteServiceTests
{
    private readonly IRouteService _routeService;
    public RouteServiceTests()
    {
        _routeService = new RouteService();
    }

    [Fact]
    public void FindPath_ShouldReturnPathCorrectly()
    {
        var (maze, entryPosition, humanPosition, expectedPath) = CreateSampleMaze();
        
        Assert.NotNull(expectedPath);

        var response = _routeService.FindPath(maze, entryPosition, humanPosition);

        Assert.NotNull(response);
        Assert.Equal(expectedPath.Count, response.Count);
        Assert.Equal(expectedPath, response);
    }

    [Fact]
    public void FindPath_ShouldReturnNull_WhenNoPathExists()
    {
        var (maze, entryPosition, humanPosition, _) = CreateUnsolvableMaze();

        var response = _routeService.FindPath(maze, entryPosition, humanPosition);

        Assert.Null(response);
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

    private (Maze, Position, Position, List<Position>?) CreateUnsolvableMaze()
    {
        var entryPosition = new Position(0, 2, PositionType.Entry);
        var humanPosition = new Position(4, 4, PositionType.Human);

        var positions = new Position[5, 5];
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                positions[x, y] = new Position(x, y, PositionType.Wall);
            }
        }
        positions[0, 2] = entryPosition;
        positions[4, 4] = humanPosition;

        var maze = new Maze(positions, entryPosition, humanPosition);
        return (maze, entryPosition, humanPosition, null);
    }
}