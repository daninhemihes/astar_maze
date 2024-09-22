using AstarMaze.App.Domain.Enums;
using AstarMaze.App.Domain.Entities;
using AstarMaze.App.Domain.ValueObjects;

namespace AstarMaze.Tests;

public class RouteTests
{
    [Fact]
    public void GetNeighbors_ShouldReturnValidNeighbors()
    {
        var maze = CreateTestMaze();
        var route = new Route(maze, maze.EntryPosition, maze.HumanPosition);
        var position = new Position(0, 1, PositionType.Empty);

        var neighbors = route.GetNeighbors(position);

        Assert.Equal(3, neighbors.Count);
        Assert.Contains(neighbors, p => p.X == 0 && p.Y == 0);
        Assert.Contains(neighbors, p => p.X == 1 && p.Y == 1);
        Assert.Contains(neighbors, p => p.X == 0 && p.Y == 2);
    }

    [Fact]
    public void AddToOpenList_ShouldAddNewPositionNode()
    {
        var maze = CreateTestMaze();
        var route = new Route(maze, maze.EntryPosition, maze.HumanPosition);
        var position = new Position(1, 2, PositionType.Empty);
        var parent = new PositionNode(maze.EntryPosition, null, 0, 0);

        route.AddToOpenList(position, parent);

        Assert.Equal(2, route.OpenList.Count);
        Assert.Equal(position, route.OpenList[1].Position);
    }

    [Fact]
    public void ReconstructPath_ShouldReturnCorrectPath()
    {
        var maze = CreateTestMaze();
        var route = new Route(maze, maze.EntryPosition, maze.HumanPosition);
        var humanNode = new PositionNode(maze.HumanPosition, route.OriginPosition, 1, 0);

        var path = route.ReconstructPath(humanNode);

        Assert.Equal(2, path.Count);
        Assert.Equal(maze.EntryPosition, path[0]);
        Assert.Equal(maze.HumanPosition, path[1]);
    }

    [Fact]
    public void GetHeuristicCost_ShouldReturnManhattanDistance()
    {
        var position1 = new Position(0, 0, PositionType.Empty);
        var position2 = new Position(3, 4, PositionType.Empty);

        var hCost = Route.GetHeuristicCost(position1, position2);

        Assert.Equal(7, hCost); // |3-0| + |4-0| = 7
    }

    private Maze CreateTestMaze()
    {
        var entryPosition = new Position(0, 2, PositionType.Entry);
        var humanPosition = new Position(1, 2, PositionType.Human);
        
        var positions = new Position[5, 5];
        positions[0, 2] = entryPosition;
        positions[1, 2] = humanPosition;

        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                if (positions[x, y] == null)
                    positions[x, y] = new Position(x, y, PositionType.Empty);
            }
        }

        return new Maze(positions, entryPosition, humanPosition);
    }
}
