using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Device.Model
{
    public class SensorData
    {
        private int temperature;
        private string name;

        public SensorData()
        {
            this.temperature = 100;
            this.name = "neko ime";
        }

        public int Temperature { get => temperature; set => temperature = value; }
        public string Name { get => name; set => name = value; }
    }
}
