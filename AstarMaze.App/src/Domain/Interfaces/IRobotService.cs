using AstarMaze.App.Domain.ValueObjects;

namespace AstarMaze.App.Domain.Interfaces;

public interface IRobotService
{
    bool FetchHumanInMaze(Maze maze, List<Position> path);
}