using Controllers_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Web_IIOT_Back.Services
{
    public interface IInitControllersLayerService
    {
        public Logic StartLogic { get; set; }
        string Fuck { get; set; }

        string InitControllersLayer();
    }
}
