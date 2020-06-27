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
                   Date = new DateTime(2013,3,1,0,0,0),
                   PM25 = 3,
                   PM10 = 6,
                   SO2 = 13,
                   NO2 = 7,
                   CO = 300,
                   O3 = 85,
                   Temp = -2.5f,
                   Pres = 1021.3f
               },
               new Sensor()
               {
                   Date = new DateTime(2013,3,1,1,0,0),
                   PM25 = 4,
                   PM10 = 2,
                   SO2 = 6,
                   NO2 = 6,
                   CO = 300,
                   O3 = 85,
                   Temp = -2.5f,
                   Pres = 1021.3f
               },
               new Sensor()
               {
                   Date = new DateTime(2013,3,1,2,0,0),
                   PM25 = 4,
                   PM10 = 3,
                   SO2 = 22,
                   NO2 = 13,
                   CO = 400,
                   O3 = 74,
                   Temp = -2.5f,
                   Pres = 1021.3f
               }

            };
        }
    }
}
