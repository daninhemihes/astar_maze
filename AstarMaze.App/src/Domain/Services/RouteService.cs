using AstarMaze.App.Domain.Entities;
using AstarMaze.App.Domain.Interfaces;
using AstarMaze.App.Domain.ValueObjects;

namespace AstarMaze.App.Domain.Services;

public class RouteService : IRouteService
{
    public Maze MapMaze(string filePath)
    {
        throw new NotImplementedException();
    }
    public List<Position>? FindPath(Maze maze, Position originPosition, Position destinationPosition)
    {
        var route = new Route(maze, originPosition, destinationPosition);

        while (route.OpenList.Count > 0)
        {
            var currentPosition = route.OpenList.OrderBy(n => n.FCost).First();

            if(currentPosition.Position.Equals(destinationPosition))
            {
                return route.ReconstructPath(currentPosition);
            }

            route.AddToClosedList(currentPosition);

            var neighbors = route.GetNeighbors(currentPosition.Position);

            foreach (var neighbor in neighbors)
            {
                if (route.ClosedList.Any(n => n.Position.Equals(neighbor)))
                    continue;

                var gCost = currentPosition.GCost + 1;

                var existingPosition = route.OpenList.FirstOrDefault(n => n.Position.Equals(neighbor));
                
                if (existingPosition == null)
                {
                    route.AddToOpenList(neighbor, currentPosition);
                }
                else if (gCost < existingPosition.GCost)
                {
                    route.RemoveFromOpenList(existingPosition);
                    route.AddToOpenList(neighbor, currentPosition);
                }
            }
        }

        return null;
    }
}