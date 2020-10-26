using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_IIOT_Back.Models
{
    public class Box
    {
        public int Id { get; set; }

        public double Temperature { get; set; }

        public double Humidity { get; set; }

        public double Pressure { get; set; }

        public bool Relay { get; set; }

        public bool VoltageSupply { get; set; }

    }
}
