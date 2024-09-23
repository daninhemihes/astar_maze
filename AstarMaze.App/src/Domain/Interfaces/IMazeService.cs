using AstarMaze.App.Domain.Entities;
using AstarMaze.App.Domain.ValueObjects;

namespace AstarMaze.App.Domain.Interfaces;

public interface IMazeService
{
    Maze MapMaze(string filePath);
}