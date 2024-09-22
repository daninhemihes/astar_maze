using AstarMaze.App.Domain.Entities;
using AstarMaze.App.Domain.ValueObjects;

namespace AstarMaze.App.Domain.Interfaces;

public interface IRouteService
{
    Maze MapMaze(string filePath);
    List<Position>? FindPath(Maze maze, Position originPosition, Position destinationPosition);
}