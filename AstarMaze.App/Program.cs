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
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }
    }
}
