using System.IO;
using Xunit;
using AstarMaze.App.Infrastructure.Loggers;

namespace AstarMaze.Tests.Infrastructure.Loggers
{
    public class LogServiceTests
    {
        [Fact]
        public void LogService_ShouldCreateFile_WithCorrectHeaders()
        {
            string fileName = "testLog";
            string filePath = fileName;

            var logService = new LogService(fileName);

            Assert.True(File.Exists(filePath)); 
            var fileContents = File.ReadAllLines(filePath);
            Assert.Equal("Comando,SensorEsquerdo,SensorDireito,SensorFrontal,Carga", fileContents[0]);

            
            File.Delete(filePath); 
        }

        [Fact]
        public void RegisterLog_ShouldAppendToFile_WithCorrectData()
        {
            string fileName = "testLog";
            string filePath = fileName;
            var logService = new LogService(fileName);

            logService.RegisterLog("LIGAR", "VAZIO", "VAZIO", "PAREDE", "SEM CARGA");

            var fileContents = File.ReadAllLines(filePath);
            Assert.Equal("Comando,SensorEsquerdo,SensorDireito,SensorFrontal,Carga", fileContents[0]); 
            Assert.Equal("LIGAR,VAZIO,VAZIO,PAREDE,SEM CARGA", fileContents[1]); 

            File.Delete(filePath); 
        }
    }
}
