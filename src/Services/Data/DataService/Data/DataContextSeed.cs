using DataService.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataService.Data
{
    public class DataContextSeed
    {
        public static void SeedData(IMongoCollection<Sensor> sensorCollection)
        {
            bool existSensor = sensorCollection.Find(p => true).Any();
            if (!existSensor)
            {
                sensorCollection.InsertManyAsync(GetPreconfiguredProducts());
            }
        }

        private static IEnumerable<Sensor> GetPreconfiguredProducts()
        {
            return new List<Sensor>()
            {
               new Sensor()
               {
                   Date = new DateTime(2013,12,12,0,0,0),
                   PM25 = 3,
                   PM10 = 3,
                   SO2 = 13,
                   NO2 = 300,
                   CO = 10,
                   O3 = 100,
                   Temp = -2.5f,
                   Pres = 1021.3f
               },
               new Sensor()
               {
                   Date = new DateTime(2013,12,18,1,0,0),
                   PM25 = 3,
                   PM10 = 6,
                   SO2 = 24,
                   NO2 = 350,
                   CO = 10,
                   O3 = 100,
                   Temp = -2.5f,
                   Pres = 1021.3f
               },
               new Sensor()
               {
                   Date = new DateTime(2014,1,11,2,0,0),
                   PM25 = 3,
                   PM10 = 6,
                   SO2 = 13,
                   NO2 = 400,
                   CO = 10,
                   O3 = 100,
                   Temp = -2.5f,
                   Pres = 1021.3f
               }

            };
        }
    }
}
