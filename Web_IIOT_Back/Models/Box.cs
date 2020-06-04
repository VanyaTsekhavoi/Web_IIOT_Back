using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_IIOT_Back.Models
{
    public class Box
    {
        public int Id { get; set; }

        public int Temperature { get; set; }

        public int Humidity { get; set; }

        public int Pressure { get; set; }

        public bool Relay { get; set; }

        public bool VoltageSupply { get; set; }

    }
}
