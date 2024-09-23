using AstarMaze.App.Domain.Enums;
using AstarMaze.App.Domain.Interfaces;
using AstarMaze.App.Domain.ValueObjects;
using AstarMaze.App.Infrastructure.Loggers;

namespace AstarMaze.App.Domain.Entities;

public class RobotSensor : IObserver
{

    private readonly Maze _maze;
    private readonly Robot _robot;
    private readonly ILogService _csvLogger;
    public string? LeftSensor { get; private set; }
    public string? RightSensor { get; private set; }
    public string? FrontSensor { get; private set; }
    public string? CommandReader { get; private set; }
    public string? CompartmentCondition { get; private set; }
    public RobotSensor(Maze maze, Robot robot, string loggerPath)
    {
        _maze = maze;
        _robot = robot;
        _csvLogger = new LogService(loggerPath);
        LeftSensor = null;
        RightSensor = null;
        FrontSensor = null;
        CommandReader = null;
        CompartmentCondition = null;
    }

    public void ReadSensors()
    {
        string[] commandNames = new string[Enum.GetNames(typeof(Command)).Length];
        commandNames[(int)Command.Start] = "LIGAR";
        commandNames[(int)Command.Move] = "A";
        commandNames[(int)Command.Turn] = "G";
        commandNames[(int)Command.Pick] = "P";
        commandNames[(int)Command.Eject] = "E";

        var (leftDirection, rightDirection) = GetDirectionsAtSides(_robot.FacingDirection);
        var frontPosition = GetPositionInDirection(_robot.CurrentPosition, _robot.FacingDirection);
        var leftPosition = GetPositionInDirection(_robot.CurrentPosition, leftDirection);
        var rightPosition = GetPositionInDirection(_robot.CurrentPosition, rightDirection);

        LeftSensor = GetPositionTypeString(leftPosition.Type);
        RightSensor = GetPositionTypeString(rightPosition.Type);
        FrontSensor = GetPositionTypeString(frontPosition.Type);
        CommandReader = commandNames[(int)_robot.LastCommand];
        CompartmentCondition = _robot.IsCarryingHuman ? "COM HUMANO" : "SEM CARGA";

        _csvLogger.RegistrarLog(CommandReader, LeftSensor, RightSensor, FrontSensor, CompartmentCondition);
    }

    private (Direction, Direction) GetDirectionsAtSides(Direction baseDirection)
    {
        return baseDirection switch
        {
            Direction.North => (Direction.West, Direction.East),
            Direction.South => (Direction.East, Direction.West),
            Direction.East  => (Direction.North, Direction.South),
            Direction.West  => (Direction.South, Direction.North),
            _ => throw new InvalidOperationException("Invalid robot direction.")
        };
    }

    private Position GetPositionInDirection(Position basePosition, Direction direction)
    {
        return direction switch
        {
            Direction.North => _maze.GetPosition(basePosition.X, basePosition.Y + 1) ?? new Position(basePosition.X, basePosition.Y + 1, PositionType.Empty),
            Direction.South => _maze.GetPosition(basePosition.X, basePosition.Y - 1) ?? new Position(basePosition.X, basePosition.Y - 1, PositionType.Empty),
            Direction.East  => _maze.GetPosition(basePosition.X + 1, basePosition.Y) ?? new Position(basePosition.X + 1, basePosition.Y, PositionType.Empty),
            Direction.West  => _maze.GetPosition(basePosition.X - 1, basePosition.Y) ?? new Position(basePosition.X - 1, basePosition.Y, PositionType.Empty),
            _ => basePosition
        };
    }

    private string GetPositionTypeString(PositionType positionType)
    {
        return positionType switch
        {
            PositionType.Empty => "VAZIO",
            PositionType.Wall => "PAREDE",
            PositionType.Human => "HUMANO",
            _ => "VAZIO"
        };
    }

    public void Update(ISubject subject)
    {
        ReadSensors();
    }
}