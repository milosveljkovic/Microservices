using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Device.Entities
{
    public class SensorData
    {
        private DateTime readTime;
        private string val1;
        private float val2;
        private float val3;
        private float val4;
        private float val5;
        private float val6;
        private float val7;
        private float val8;

        public SensorData(){}

        public SensorData(DateTime time, string val1, float val2, float val3, float val4, float val5, float val6, float val7, float val8)
        {
            this.readTime = time;
            this.val1 = val1;
            this.val2 = val2;
            this.val3 = val3;
            this.val4 = val4;
            this.val5 = val5;
            this.val6 = val6;
            this.val7 = val7;
            this.val8 = val8;
        }
    }
}
