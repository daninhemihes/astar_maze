using AstarMaze.App.Domain.Entities;
using AstarMaze.App.Domain.Enums;
using AstarMaze.App.Domain.ValueObjects;

namespace AstarMaze.Tests;

public class MazeTests
{
    [Fact]
    public void Maze_ShouldInitializeCorrectly_WithValidParameters()
    {
        var positions = CreateSamplePositions(5, 5);
        var entryPosition = new Position(0, 1, PositionType.Entry);
        var humanPosition = new Position(3, 3, PositionType.Human);
        positions[0,1] = entryPosition;
        positions[3,3] = humanPosition;

        var maze = new Maze(positions, entryPosition, humanPosition);

        Assert.Equal(5, maze.Width);
        Assert.Equal(5, maze.Height);
        Assert.Equal(humanPosition, maze.HumanPosition);
        Assert.Equal(entryPosition, maze.EntryPosition);
        Assert.Equal(Direction.East, maze.EntryDirection);
    }

    [Fact]
    public void Maze_ShouldThrowArgumentException_ForInvalidPositionsArray()
    {
        var invalidPositions = CreateSamplePositions(2, 2);
        var entryPosition = new Position(0, 1, PositionType.Entry);
        var humanPosition = new Position(1, 1, PositionType.Human);

        var exception = Assert.Throws<ArgumentException>(() => new Maze(invalidPositions, entryPosition, humanPosition));

        Assert.Equal("Invalid maze positions. Should be at least 3x3.", exception.Message);
    }

    [Fact]
    public void Maze_ShouldThrowArgumentException_ForInvalidEntryPosition()
    {
        var positions = CreateSamplePositions(5, 5);
        var invalidEntryPosition = new Position(0, 1, PositionType.Empty);
        var humanPosition = new Position(3, 3, PositionType.Human);

        var exception = Assert.Throws<ArgumentException>(() => new Maze(positions, invalidEntryPosition, humanPosition));

        Assert.Equal("Invalid entry position.", exception.Message);
    }

    [Fact]
    public void Maze_ShouldThrowArgumentException_ForEntryPositionOutOfBoundary()
    {
        var positions = CreateSamplePositions(5, 5);
        var invalidEntryPosition = new Position(1, 1, PositionType.Entry);
        var humanPosition = new Position(3, 3, PositionType.Human);

        var exception = Assert.Throws<InvalidOperationException>(() => new Maze(positions, invalidEntryPosition, humanPosition));

        Assert.Equal("Invalid entry position. Entry must be adjacent to a wall.", exception.Message);
    }

    [Fact]
    public void Maze_ShouldThrowArgumentException_ForInvalidHumanPosition()
    {
        var positions = CreateSamplePositions(5, 5);
        var entryPosition = new Position(0, 1, PositionType.Entry);
        var invalidHumanPosition = new Position(3, 3, PositionType.Empty);

        var exception = Assert.Throws<ArgumentException>(() => new Maze(positions, entryPosition, invalidHumanPosition));

        Assert.Equal("Invalid human position.", exception.Message);
    }

    [Fact]
    public void GetEntryDirection_ShouldReturnCorrectDirection_ForDifferentBoundaries()
    {
        var positions = CreateSamplePositions(5, 5);
        var humanPosition = new Position(3, 3, PositionType.Human);
        var northEntryPosition = new Position(2, 0, PositionType.Entry);
        var southEntryPosition = new Position(2, 4, PositionType.Entry);
        var eastEntryPosition = new Position(0, 2, PositionType.Entry);
        var westEntryPosition = new Position(4, 2, PositionType.Entry);

        var northMaze = new Maze(positions, northEntryPosition, humanPosition);
        Assert.Equal(Direction.North, northMaze.EntryDirection);

        var southMaze = new Maze(positions, southEntryPosition, humanPosition);
        Assert.Equal(Direction.South, southMaze.EntryDirection);

        var eastMaze = new Maze(positions, eastEntryPosition, humanPosition);
        Assert.Equal(Direction.East, eastMaze.EntryDirection);

        var westMaze = new Maze(positions, westEntryPosition, humanPosition);
        Assert.Equal(Direction.West, westMaze.EntryDirection);
    }

    private Position[,] CreateSamplePositions(int width, int height)
    {
        var positions = new Position[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                positions[x,y] = new Position(x, y, PositionType.Empty);
            }
        }
        return positions;
    }
}