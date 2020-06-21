using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Device.Entities
{

    public class Settings
    {
        public int sendPeriod { get; set; }
        public int readPeriod { get; set; }
        public int treshold { get; set; }
        public int isOnSensor { get; set; }
        public int isOnMiAirPurfier { get; set; }
        public int cleaningStrengthMiAirPurfier { get; set; }

        public Settings()
        {

        }

    }


}