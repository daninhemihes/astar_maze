using AstarMaze.App.Infrastructure.Loggers;
using System.IO;

namespace AstarMaze.App.Infrastructure.Loggers;
public class LogService : ILogService
{
    private readonly string logFilePath;


    public LogService(string fileName)
    {
        logFilePath = fileName;
        using (StreamWriter writer = new StreamWriter(logFilePath, false))
        {
            writer.WriteLine("Comando,SensorEsquerdo,SensorDireito,SensorFrontal,Carga");
        }
    }

    public void RegisterLog(string command, string LeftSensor, string RightSensor, string FrontSensor, string Load)
    {
        using (StreamWriter writer = new StreamWriter(logFilePath, true))
        {
            writer.WriteLine($"{command},{LeftSensor},{RightSensor},{FrontSensor},{Load}");
        }
    }

    void ILogService.RegistrarLog(string command, string LeftSensor, string RightSensor, string FrontSensor, string Load)
    {
        RegisterLog(command, LeftSensor, RightSensor, FrontSensor, Load);
    }
}

  

