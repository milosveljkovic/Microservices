using Analytics.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analytics.Data
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
                    Date = new DateTime(2013,2,28,22,0,0),
                    PM25 = 443,
                    PM10 = 679,
                    SO2 = 80,
                    NO2 = 148,
                    CO = 4500,
                    O3 = 107,
                    Temp = 14.5f,
                    Pres = 996.3f
                },
                new Sensor()
                {
                    Date = new DateTime(2013,2,28,23,0,0),
                    PM25 = 277,
                    PM10 = 461,
                    SO2 = 83,
                    NO2 = 86,
                    CO = 2899,
                    O3 = 107,
                    Temp = 13.2f,
                    Pres = 996.1f
                }
            };
        }
    }
}
