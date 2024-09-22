using AstarMaze.App.Domain.Enums;
using AstarMaze.App.Domain.ValueObjects;

namespace AstarMaze.App.Domain.Entities;

public class Route
{
    private readonly Maze _maze;
    public List<PositionNode> OpenList { get; private set; }
    public List<PositionNode> ClosedList { get; private set; }
    public PositionNode OriginPosition { get; private set; }
    public PositionNode DestinationPosition { get; private set; }
    public List<Position> Path { get; private set; }

    public Route (Maze maze, Position originPosition, Position destinationPosition)
    {
        _maze               = maze;
        OpenList            = new List<PositionNode>();
        ClosedList          = new List<PositionNode>();
        OriginPosition      = new PositionNode(originPosition, null, 0, GetHeuristicCost(originPosition, destinationPosition));
        DestinationPosition = new PositionNode(destinationPosition, null, 0, 0);
        Path                = new List<Position>();

        OpenList.Add(OriginPosition);
    }

    public static int GetHeuristicCost(Position a, Position b)
    {
        return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
    }

    private bool IsValidPosition(int x, int y)
    {
        return x >= 0 && y >= 0 && x < _maze.Width && y < _maze.Height && _maze.Positions[x,y].Type != PositionType.Wall;
    }

    public void AddToOpenList(Position position, PositionNode parent)
    {
        var hCost = GetHeuristicCost(position, DestinationPosition.Position);
        var positionNode = new PositionNode(position, parent, parent.GCost + 1, hCost);
        OpenList.Add(positionNode);
    }

    public void RemoveFromOpenList(PositionNode positionNode)
    {
        OpenList.Remove(positionNode);
    }

    public void AddToClosedList(PositionNode positionNode)
    {
        ClosedList.Add(positionNode);
        OpenList.Remove(positionNode);
    }

    public List<Position> GetNeighbors(Position position)
    {
        var neighbors = new List<Position>();

        var directions = new List<(int x, int y)>
        {
            (-1, 0),
            (1, 0),
            (0, -1),
            (0, 1)
        };

        foreach (var (dx, dy) in directions)
        {
            var targetX = position.X + dx;
            var targetY = position.Y + dy;

            if (IsValidPosition(targetX, targetY))
            {
                neighbors.Add(_maze.Positions[targetX, targetY]);
            }
        }

        return neighbors;
    }

    public List<Position> ReconstructPath(PositionNode positionNode)
    {
        var path = new List<Position>();
        var current = positionNode;

        while (current != null)
        {
            path.Add(current.Position);
            current = current.Parent;
        }

        path.Reverse();
        return path;
    }
}