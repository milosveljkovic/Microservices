using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Device.Entities
{
    public class SensorData
    {
        public DateTime date;
        public int PM25;
        public int PM10;
        public int SO2;
        public int NO2;
        public int C0;
        public int O3;
        public float temp;
        public float pres;


        public SensorData(){
            this.date = new DateTime(0);
            this.PM25 = 0;
            this.PM10 = 0;
            this.SO2 = 0;
            this.NO2 = 0;
            this.C0 = 0;
            this.O3 = 0;
            this.temp = 0;
            this.pres = 0;
        }

        public SensorData(DateTime date, int pm25,int pm10,int so2, int no2,int c0,int o3,float temp,float pres)
        {
            this.date = date;
            this.PM25 = pm25;
            this.PM10 = pm10;
            this.SO2 = so2;
            this.NO2 = no2;
            this.C0 = c0;
            this.O3 = o3;
            this.temp = temp;
            this.pres = pres;
        }
    }
}
