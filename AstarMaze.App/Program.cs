using AstarMaze.App.Domain.Services;
using AstarMaze.App.Domain.ValueObjects;
using AstarMaze.App.Infrastructure.Repositories;
using AstarMaze.App.Domain.Enums;
using AstarMaze.App.Application.Services;
using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            //precisa ajustar
            string mazeFilePath = "labirinto.txt";
            var mazeRepository = new MazeRepository();
            Maze maze = mazeRepository.LoadMaze(mazeFilePath);

            var routeService = new RouteService();
            var path = routeService.FindPath(maze, maze.EntryPosition, maze.HumanPosition);

            if (path == null)
            {
                Console.WriteLine("Nenhuma rota encontrada.");
                return;
            }

            var robotService = new RobotService();

            bool success = robotService.FetchHumanInMaze(maze, path);

            if (success)
            {
                Console.WriteLine("O robô encontrou o humano e o trouxe de volta à saída.");
            }

            PrintMazeWithPath(maze, path);

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }
    }

    static void PrintMazeWithPath(Maze maze, List<Position> path)
    {
        char[,] mazeDisplay = new char[maze.Width, maze.Height];

        for (int i = 0; i < maze.Height; i++)
        {
            for (int j = 0; j < maze.Width; j++)
            {
                var position = maze.Positions[j, i];
                mazeDisplay[j, i] = position.Type switch
                {
                    PositionType.Wall => '*',
                    PositionType.Human => 'H',
                    PositionType.Entry => 'E',
                    _ => ' '
                };
            }
        }

        // Marcar o caminho do robô
        foreach (var position in path)
        {
            mazeDisplay[position.X, position.Y] = '.';
        }

        // Imprimir o labirinto
        for (int i = 0; i < maze.Height; i++)
        {
            for (int j = 0; j < maze.Width; j++)
            {
                Console.Write(mazeDisplay[j, i]);
            }
            Console.WriteLine();
        }
    }
}
