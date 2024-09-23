using AstarMaze.App.Domain.Interfaces;
using AstarMaze.App.Domain.ValueObjects;
using AstarMaze.App.Infrastructure.Repositories;

namespace AstarMaze.App.Domain.Services;

public class MazeService : IMazeService
{
    private readonly IMazeRepository _mazeRepository;
    public MazeService()
    {
        _mazeRepository = new MazeRepository();
    }
    public Maze MapMaze(string filePath)
    {
        var maze = _mazeRepository.LoadMaze(filePath);

        return maze;
    }
}