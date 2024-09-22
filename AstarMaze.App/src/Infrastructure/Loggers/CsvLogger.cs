using System.IO;


    public interface ILogService
    {
        void RegistrarLog(string command, string LeftSensor, string RightSensor, string FrontSensor, string Load);
    }
public class LogService : ILogService
{
    private readonly string logFilePath;


    public LogService(string fileName)
    {
        logFilePath = $"{fileName}.csv";
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
        throw new NotImplementedException();
    }
}

  

