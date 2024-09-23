using AstarMaze.App.Domain.Services;
using AstarMaze.App.Domain.ValueObjects;
using AstarMaze.App.Infrastructure.Repositories;
using AstarMaze.App.Domain.Enums;
using AstarMaze.App.Application.Services;
using System;
using System.Collections.Generic;
using System.IO;
using AstarMaze.App.Application.Interfaces;

class Program
{

    static void Main(string[] args)
    {
        try
        {
            //precisa ajustar
            string mazeFilePath = "labirinto.txt";
            IRobotAppService _robotAppService = new RobotAppService();

            var response = _robotAppService.FindHumanInMaze(mazeFilePath);
            Console.WriteLine(response.Status);
            Console.WriteLine(response.Message);

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

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }
    }
}
