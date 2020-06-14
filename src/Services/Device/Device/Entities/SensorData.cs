using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Device.Entities
{
    public class SensorData
    {
        public int year;
        public int month;
        public int day;
        public int hour;
        public int PM25;
        public int PM10;
        public int SO2;
        public int NO2;
        public int C0;
        public int O3;
        public float temp;
        public float pres;


        public SensorData(){}

        public SensorData(int year,int month,int day, int hour, int pm25,int pm10,int so2, int no2,int c0,int o3,float temp,float pres)
        {
            this.year = year;
            this.month = month;
            this.day = day;
            this.hour = hour;
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
