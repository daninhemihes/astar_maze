using AstarMaze.App.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using AstarMaze.App.Domain.Enums;

namespace AstarMaze.App.src.Application.Services
{
    internal class RescueRecord
    {
        public class ResgateService
        {

            private readonly Robot _robo;
            private readonly ILogService _logService;

            public ResgateService(Robot robo, ILogService logService)
            {
                _robo = robo;
                _logService = logService;
            }

            public void ExecutarResgate()
            {
                _logService.RegistrarLog(ComandoRobo.Ligar.ToString(), _robo.LeftSensor.State.ToString(), _robo.RightSensor.State.ToString(), _robo.FrontSensor.State.ToString(), _robo.CurrentLoad.ToString());

                _robo.MoveForward();
                _logService.RegistrarLog(ComandoRobo.Avancar.ToString(), _robo.LeftSensor.State.ToString(), _robo.RightSensor.State.ToString(), _robo.FrontSensor.State.ToString(), _robo.CurrentLoad.ToString());

                _robo.TurnRight();
                _logService.RegistrarLog(ComandoRobo.Girar.ToString(), _robo.LeftSensor.State.ToString(), _robo.RightSensor.State.ToString(), _robo.FrontSensor.State.ToString(), _robo.CurrentLoad.ToString());

                _robo.MoveForward();
                _logService.RegistrarLog(ComandoRobo.Avancar.ToString(), _robo.LeftSensor.State.ToString(), _robo.RightSensor.State.ToString(), _robo.FrontSensor.State.ToString(), _robo.CurrentLoad.ToString());

                _robo.FrontSensor.AtualizarEstado(StateSensor.Humano);
                _robo.PickHuman();
                _logService.RegistrarLog(ComandoRobo.PegarHumano.ToString(), _robo.LeftSensor.State.ToString(), _robo.RightSensor.State.ToString(), _robo.FrontSensor.State.ToString(), _robo.CurrentLoad.ToString());

                _robo.TurnRight();
                _logService.RegistrarLog(ComandoRobo.Girar.ToString(), _robo.LeftSensor.State.ToString(), _robo.RightSensor.State.ToString(), _robo.FrontSensor.State.ToString(), _robo.CurrentLoad.ToString());

                _robo.EjectHuman();
                _logService.RegistrarLog(ComandoRobo.EjetarHumano.ToString(), _robo.LeftSensor.State.ToString(), _robo.RightSensor.State.ToString(), _robo.FrontSensor.State.ToString(), _robo.CurrentLoad.ToString());

            }
        }
    }
}

    

