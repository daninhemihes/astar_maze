using AstarMaze.App.Application.Interfaces;
using AstarMaze.App.Application.Services;

namespace AstarMaze.Tests.Application;

public class RobotAppServiceTests
{
    private readonly IRobotAppService _robotAppService;
    public RobotAppServiceTests()
    {
        _robotAppService = new RobotAppService();
    }

    [Fact]
    public void FindHumanInMaze_ShouldReturnSuccess_WhenHumanIsFoundAndReturned()
    {
        string pathToMazeFile = "valid_maze.txt";

        var result = _robotAppService.FindHumanInMaze(pathToMazeFile);

        Assert.NotNull(result);
        Assert.Equal("Success", result.Status);
    }
}