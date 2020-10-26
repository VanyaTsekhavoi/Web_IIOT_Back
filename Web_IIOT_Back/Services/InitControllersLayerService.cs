using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Controllers_Layer;

namespace Web_IIOT_Back.Services
{
    public class InitControllersLayerService : IInitControllersLayerService
    {

        //One Element to rule them all...   
        public Logic StartLogic { get; set; }
        public string Fuck { get; set; }

        public InitControllersLayerService(Logic logic)
        {
            StartLogic = logic;
            Fuck = "This BullShit";
        }

        public string InitControllersLayer()
        {
            return StartLogic.StartProgect();
        }
    }
}
