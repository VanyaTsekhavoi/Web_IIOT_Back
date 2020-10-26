using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_IIOT_Back.Models
{
    public class Periphery
    {
        public int Id { get; set; }

        public bool FirstRelay { get; set; }

        public bool SecondRelay { get; set; }

        public bool VoltageSupply { get; set; }

    }
}
