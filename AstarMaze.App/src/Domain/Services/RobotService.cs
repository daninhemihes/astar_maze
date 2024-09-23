using AstarMaze.App.Domain.Entities;
using AstarMaze.App.Domain.Enums;
using AstarMaze.App.Domain.Interfaces;
using AstarMaze.App.Domain.ValueObjects;

namespace AstarMaze.App.Domain.Services;

public class RobotService : IRobotService
{
    public bool FetchHumanInMaze(Maze maze, List<Position> path, string loggerPath = "log.csv")
    {
        var robot = new Robot(maze.EntryPosition, maze.EntryDirection);
        var robotSensor = new RobotSensor(maze, robot, loggerPath);
        robot.Attach(robotSensor);

        //Rota em direção ao humano
        var pathToHuman = FollowPath(maze, robot, path);

        if (pathToHuman == false || robot.IsCarryingHuman == false) 
            throw new InvalidOperationException("The robot was unable to catch the human.");

        //Rota de volta a saída do labirinto
        path.Reverse();
        path.RemoveAt(0);
        var pathToExit = FollowPath(maze, robot, path);

        if (pathToExit == false || robot.IsCarryingHuman == true)
            throw new InvalidOperationException("The robot was unable to retrieve the human.");
    
        return true;
    }

    private bool FollowPath(Maze maze, Robot robot, List<Position> path)
    {
        foreach (Position nextPosition in path)
        {
            if (robot.CurrentPosition.Equals(nextPosition)) continue;

            var nextDirection = robot.GetNextPositionDirection(nextPosition);

            while (robot.FacingDirection != nextDirection)
            {
                robot.TurnRight();
            }

            if (nextPosition.Type == PositionType.Human)
            {
                maze.Positions[nextPosition.X, nextPosition.Y].CollectHuman();
                robot.PickHuman();
                return true;
            }

            if (nextPosition.Type == PositionType.Entry)
            {
                maze.Positions[nextPosition.X, nextPosition.Y].PlaceHuman();
                robot.EjectHuman();
                return true;
            }

            robot.MoveForward();
            if (robot.CurrentPosition.Equals(nextPosition) == false) 
                throw new InvalidOperationException("The robot has lost its way.");
        }
        
        return false;
    }
}