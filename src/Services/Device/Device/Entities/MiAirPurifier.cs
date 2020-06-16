using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Device.Entities
{
    public class MiAirPurifier
    {
        public bool isOn { get; set; }
        public int cleaningStrangth { get; set; }
        public MiAirPurifier()
        {
            isOn = false;
            cleaningStrangth = 10;
        }
    }
}
