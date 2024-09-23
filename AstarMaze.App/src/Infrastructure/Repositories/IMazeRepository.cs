using AstarMaze.App.Domain.Entities;
using AstarMaze.App.Domain.ValueObjects;

namespace AstarMaze.App.Infrastructure.Repositories;

public interface IMazeRepository
{
    Maze LoadMaze(string fileName);
    Maze CreateMaze(string[] lines);
}