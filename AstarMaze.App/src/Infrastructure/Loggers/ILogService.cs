using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstarMaze.App.src.Infrastructure.Loggers
{
    public interface ILogService
    {
        void RegistrarLog(string command, string LeftSensor, string RightSensor, string FrontSensor, string Load);
    }
}
