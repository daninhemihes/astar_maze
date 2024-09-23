using AstarMaze.App.Application.DTOs;

namespace AstarMaze.App.Application.Interfaces
{
    public interface IRobotAppService
    {
        RobotResultDTO FindHumanInMaze(string pathToMazeFile);
    }
}