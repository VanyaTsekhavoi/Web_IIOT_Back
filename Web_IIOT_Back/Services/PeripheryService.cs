using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_IIOT_Back.Models;

namespace Web_IIOT_Back.Services
{
    public class PeripheryService : IPeripheryService
    {
        private readonly Periphery Periphery;
        private IInitControllersLayerService initControllersLayerService;

        public PeripheryService(IInitControllersLayerService initControllersLayerService)
        {
            this.initControllersLayerService = initControllersLayerService;

            Periphery = new Periphery();
            Periphery.FirstRelay = false;
            Periphery.SecondRelay = false;
            Periphery.VoltageSupply = false;
        }

        public List<Periphery> GetAllPeripheries()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Change state of Relays and Semistor
        /// </summary>
        /// <param name="peripheryId"></param>
        /// <returns></returns>
        public Periphery GetPeriphery(int peripheryId)
        {
            switch (peripheryId)
            {
                case 1:
                    Periphery.FirstRelay = !Periphery.FirstRelay;
                    initControllersLayerService.StartLogic.logicFirstrelay();

                    break;
                case 2:
                    Periphery.SecondRelay = !Periphery.SecondRelay;
                    initControllersLayerService.StartLogic.logicSecondRelay();
                    break;
                case 3:
                    Periphery.VoltageSupply = !Periphery.VoltageSupply;
                    initControllersLayerService.StartLogic.logicArduinoVolt();
                    break;
            }

            return Periphery;
        }

        public Periphery CreatePeriphery()
        {
            throw new NotImplementedException();
        }

        public Periphery UpdatePeriphery(int peripheryId)
        {
            throw new NotImplementedException();
        }

        public Periphery DeletePeriphery(int peripheryId)
        {
            throw new NotImplementedException();
        }
    }
}
