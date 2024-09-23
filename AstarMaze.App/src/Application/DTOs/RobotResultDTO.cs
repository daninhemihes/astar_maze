namespace AstarMaze.App.Application.DTOs;

public class RobotResultDTO
{
    public string Status { get; }
    public string Message { get; }
    public RobotResultDTO(string status, string message)
    {
        Status = status;
        Message = message;
    }

    public override string ToString()
    {
        return $"Status: {Status}\nMessage: {Message}";
    }
}