using AstarMaze.App.Application.Services;
using AstarMaze.App.Application.Interfaces;

class Program
{

    static void Main(string[] args)
    {
        IRobotAppService _robotAppService = new RobotAppService();
        string responseStatus = "";

        do
        {
            Console.WriteLine("Type the full path to the maze map .txt file.");
            
            string? mazeFilePath = Console.ReadLine();

            if (mazeFilePath ==  null || mazeFilePath == "") continue;
            
            var response = _robotAppService.FindHumanInMaze(mazeFilePath);
            responseStatus = response.Status;

            Console.WriteLine(response.Status);
            Console.WriteLine(response.Message);
            Console.WriteLine("\n");
        } while (responseStatus != "Success");
    }
}
