using AstarMaze.App.Application.DTOs;
using AstarMaze.App.Application.Interfaces;
using AstarMaze.App.Domain.Interfaces;
using AstarMaze.App.Domain.Services;

namespace AstarMaze.App.Application.Services
{
    public class RobotAppService : IRobotAppService
    {
        private readonly IMazeService _mazeService;
        private readonly IRobotService _robotService;
        private readonly IRouteService _routeService;
        public RobotAppService()
        {
            _mazeService = new MazeService();
            _robotService = new RobotService();
            _routeService  = new RouteService();
        }
        public RobotResultDTO FindHumanInMaze(string pathToMazeFile)
        {
            try
            {
                var maze = _mazeService.MapMaze(pathToMazeFile);

                var route = _routeService.FindPath(maze, maze.EntryPosition, maze.HumanPosition) ?? throw new InvalidOperationException("Unable to find a path from robot to human.");

                string pathToLogFile = Path.ChangeExtension(pathToMazeFile, ".csv");
                var result = _robotService.FetchHumanInMaze(maze, route, pathToLogFile);

                if (result)
                {
                    return new RobotResultDTO(
                        "Success",
                        $"Human rescued succesfully! Check the robot log files at {Path.GetFullPath(pathToLogFile)}"
                    );
                }

                return new RobotResultDTO(
                    "Error",
                    "Unable to recover human in maze."
                );
            } 
            catch (Exception ex)
            {
                return new RobotResultDTO(
                    "Error",
                    ex.Message
                );
            }
        }
    }
}